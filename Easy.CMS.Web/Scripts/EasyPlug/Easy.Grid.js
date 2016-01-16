/// <reference path="Easy.js" />

var Easy = Easy || {};
Easy.GridLan = {
    PageSize: "分页大小",
    CurrentPage: "当前",
    PageGo: "刷新",
    FirstPage: "首页",
    PrePage: "上一页",
    NextPage: "下一页",
    LastPage: "尾页",
    NoData: "无相关数据...",
    PageNumberError: "您输入的页码不为纯数字，请输入正确的页码数!",
    AllRecord: "{0} 条记录",
    DeleteTitle: "提示",
    DeleteConfirm: "确定要删除选中项吗？",
    SelectWorm: "请至少选择一项进行操作！",
    Delete: "删除选中项"
}
Easy.Grid = (function (json) {
    var DataUrl = "";
    var ele;
    var chVale;
    var pageIndex = 0;
    var pageSize = 20;
    var allPage = 0;
    var templetes = new Array();
    var templeteValues = new Array();
    var model;
    var canSearch = false;
    var rowDataBindEventHandler;
    var checkBoxEventHandler;
    var dataSuccessEventHandler;
    var CheckBox = false;
    var orderArray = [];
    var OrderCol = "";
    var OrderType = 1;
    var gridHtml = [];
    var scrollLeft = 0;
    var constHeight = false;
    var isLoading = false;
    var QueryString = "";
    var deleteUrl = "";
    var heightFix = 15;
    var jsonData;
    gridHtml.push("<div class='EasyGrid panel panel-default'>");
    gridHtml.push("<div class='GridContent'><div class='GridToolBar panel-heading'></div><div class='GridHeader'></div><div class='GridSearch'></div><div class='GridBody'></div></div>");
    gridHtml.push("<div class='Gridfoot'>");
    gridHtml.push("<div class='GridDelete' title='" + Easy.GridLan.Delete + "'>&nbsp;</div>");
    gridHtml.push("<label>");
    gridHtml.push(Easy.GridLan.PageSize);
    gridHtml.push("</label>");
    gridHtml.push("<select id='GridPageSize' easy='easy'>");
    gridHtml.push("<option value='10'>10</option>");
    gridHtml.push("<option value='20' selected='selected'>20</option>");
    gridHtml.push("<option value='30'>30</option>");
    gridHtml.push("<option value='50'>50</option>");
    gridHtml.push("<option value='100'>100</option>");
    gridHtml.push("</select>");
    gridHtml.push("<label for='GridPageIdex'>")
    gridHtml.push(Easy.GridLan.CurrentPage);
    gridHtml.push("</label><input id='GridPageIdex' type='text' value='0' ValueType='Num' />");
    gridHtml.push("<label id='PageInfo'></label>");
    gridHtml.push("<label id='Count'></label>");
    gridHtml.push("<a id='PageGo' class='glyphicon glyphicon-repeat' title='" + Easy.GridLan.PageGo + "'>");
    gridHtml.push("</a>");
    gridHtml.push("<a id='PageFirst' class='glyphicon glyphicon-fast-backward' title='" + Easy.GridLan.FirstPage + "'>");
    gridHtml.push("</a>");
    gridHtml.push("<a id='PagePre' class='glyphicon glyphicon-step-backward' title='" + Easy.GridLan.PrePage + "'>");
    gridHtml.push("</a>");
    gridHtml.push("<a id='PageNext' class='glyphicon glyphicon-step-forward' title='" + Easy.GridLan.NextPage + "'>");
    gridHtml.push("</a>");
    gridHtml.push("<a id='PageLast' class='glyphicon glyphicon-fast-forward' title='" + Easy.GridLan.LastPage + "'>");
    gridHtml.push("</a>");
    gridHtml.push("</div></div>");
    var Grid = $(gridHtml.join(""));
    var gridHeader = Grid.find(".GridHeader");
    var gridBody = Grid.find(".GridBody");
    var gridSearch = Grid.find(".GridSearch");
    gridBody.scroll(function () {
        gridHeader.scrollLeft($(this).scrollLeft());
        gridSearch.scrollLeft($(this).scrollLeft());
    });
    Grid.find("#GridPageIdex").BeNumber(Easy.GridLan.PageNumberError);
    Grid.find("#GridPageIdex").blur(function () {
        if ($(this).val() == "") {
            $(this).val(pageIndex + 1);
        }
    });
    Grid.find("#GridPageIdex").keyup(function (e) {
        if (e.keyCode == 13) {
            Grid.find("#PageGo").click();
        }
    });
    Grid.find("#GridPageIdex")
    Grid.find("#GridPageSize").change(function () {
        pageIndex = 0;
        pageSize = $(this).val();
        PlaceGrid();
    });
    Grid.find("#PageGo").click(function () {
        pageIndex = parseInt(Grid.find("#GridPageIdex").val()) - 1;
        if (pageIndex < 0 || pageIndex >= allPage) {
            pageIndex = 0;
        }
        PlaceGrid();
    });
    Grid.find("#PageFirst").click(function () {
        if (pageIndex == 0)
            return;
        pageIndex = 0;
        PlaceGrid();
    });
    Grid.find("#PagePre").click(function () {
        pageIndex -= 1;
        if (pageIndex < 0) {
            pageIndex = 0;
            return;
        }
        PlaceGrid();
    });
    Grid.find("#PageNext").click(function () {
        pageIndex += 1;
        if (pageIndex >= allPage) {
            pageIndex = allPage - 1;
            return;
        }
        PlaceGrid();
    });
    Grid.find("#PageLast").click(function () {
        if (pageIndex == allPage - 1) {
            return;
        }
        pageIndex = allPage - 1;
        PlaceGrid();
    });
    Grid.find(".GridDelete").click(function () { Delete(); });
    if (json) {
        if (json.url) { DataUrl = json.url; }
        if (json.id) {
            ele = "#" + json.id;
            $(ele).append(Grid);
        }
    }

    function GetDataSucess(data) {
        isLoading = false;
        try {
            if (typeof data === "string") {
                jsonData = eval("(" + data + ")");
            } else {
                jsonData = data;
            }
        }
        catch (ex) {
            alert(ex.message);
            return false;
        }
        if (!model) {
            InitGrid(jsonData);
        }
        InitData(jsonData);
        Grid.find("#PageGo").removeClass("InnerLonding");
    }
    function InitGrid(gdata) {
        var width = 0;
        var tableHeader = $("<table cellpadding='0' cellspacing='0'></table>");
        var trH = "<tr class='trTitle'>";
        if (CheckBox && chVale) {
            trH += "<th style='width:20px' align='center'><input type='checkbox' class='CheckBoxAll' /></th>";
            width += 20;
        }
        if (!model) {
            model = gdata.Columns;
        }
        for (var item in model) {
            if (model[item].Hidden)
                continue;
            var colWidth = 150;
            if (model[item].Width)
                colWidth = model[item].Width;
            if (OrderCol == "")
                OrderCol = model[item].Name;
            trH += "<th style='width:" + colWidth + "px' col='" + model[item].Name + "'><div class='coData' style='width:" + (colWidth - 2) + "px'>" + model[item].DisplayName + "</div></th>";
            width += colWidth;
        }
        trH += "<th style='width:20px'></th>";
        tableHeader.html(trH);
        gridHeader.html(tableHeader);
        if (OrderType == 1) {
            gridHeader.find("th[col='" + OrderCol + "']").addClass("OrderUp");
        }
        else {
            gridHeader.find("th[col='" + OrderCol + "']").addClass("OrderDown");
        }
        var search = $("<table cellpadding='0' cellspacing='0'></table>");
        trH = "";
        if (canSearch) {
            trH = "<tr>";
            if (CheckBox && chVale) {
                trH += "<th style='width:20px' align='center'><input type='button' class='ClearSearch' /></th>";
            }
            for (var itemName in model) {
                var input = "";
                var item = model[itemName];
                if (item.Hidden)
                    continue;
                switch (item.DataType) {
                    case "String": input = "<input type='text' DataType='" + item.DataType + "' id='Search_" + item.Name + "' name='" + item.Name + "' OperatorType='7'/>"; break;
                    case "Boolean":
                        {
                            input = "<input type='hidden' DataType='" + item.DataType + "' id='Search_" + item.Name + "' name='" + item.Name + "' OperatorType='1'/>";
                            var option = "<option></option>";
                            option += "<option value='true'>是</option><option value='false'>否</option>";
                            input += "<select class='easy' ValuePlace='Search_" + item.Name + "'>" + option + "</select>";
                            break;
                        }
                    case "Decimal":
                    case "Single":
                    case "Int16":
                    case "Int64":
                    case "Int32":
                        {
                            input = "<input type='hidden' DataType='" + item.DataType + "' id='Search_" + item.Name + "' name='" + item.Name + "' OperatorType='1'/>" +
                            "<span><input class='NumAdd' type='button' ValuePlace='Search_" + item.Name + "' value=''/><input type='button' class='NumClear' ValuePlace='Search_" + item.Name + "' value='' /></span>";
                            break;
                        }
                    case "DateTime":
                        {
                            input = "<input type='hidden' DataType='" + item.DataType + "' Format='" + item.Format + "' id='Search_" + item.Name + "' name='" + item.Name + "' OperatorType='1'/>" +
                                 "<span><input class='NumAdd' type='button' ValuePlace='Search_" + item.Name + "' value=''/><input type='button' class='NumClear' ValuePlace='Search_" + item.Name + "' value='' /></span>";
                            break;
                        }
                    case "Select":
                        {
                            input = "<input type='hidden' DataType='" + item.DataType + "' id='Search_" + item.Name + "' name='" + item.Name + "' OperatorType='1'/>";
                            var option = "<option></option>";
                            option += item.Data || "";
                            input += "<select class='easy' multiple='multiple' ValuePlace='Search_" + item.Name + "'>" + option + "</select>";
                            break;
                        }
                    case "None": input = "<span></span>"; break;
                    default: input = "<input type='text' DataType='" + item.DataType + "' id='Search_" + item.Name + "' name='" + item.Name + "' OperatorType='7'/>"; break;
                }
                var colWidth = 150;
                if (item.Width)
                    colWidth = item.Width;
                trH += "<th style='width:" + colWidth + "px'>" + input + "</th>";
            }
            trH += "<th style='width:20px'></th>";
            trH += " </tr>";
            search.html(trH);
            gridSearch.html(search);
            gridSearch.children("table").width(width + 20);
            gridHeader.children("table").width(width + 20);
            gridSearch.find("input").keydown(StartSearch);
            gridSearch.find(".NumAdd").click(ShowNumCondition);
            gridSearch.find(".NumClear").click(ClearCondition);
            gridSearch.find(".ClearSearch").click(ClearCondition);
            gridSearch.find("select").change(function () {
                pageIndex = 0;
                gridSearch.find("#" + $(this).attr("ValuePlace")).val($(this).val());
                PlaceGrid();
            });
            if (Easy.UI) {
                Easy.UI.DropDownList();
                Easy.UI.MultiSelect();
                Easy.UI.CheckBox();
                Easy.UI.NumberInput();
            }
        } else {
            gridSearch.hide();
        }
        gridHeader.find(".CheckBoxAll").click(function () {
            gridBody.find("tr").find(".CheckBoxItem").prop("checked", $(this).prop("checked"));
            CheckBack();
        });
        gridHeader.find("th[col]").click(function () {
            var deCol = $(this).attr("col");
            if (deCol == OrderCol) {
                OrderType = OrderType == 1 ? 2 : 1;
            }
            else {
                OrderCol = deCol;
                OrderType = 1;
            }
            orderArray = [];
            orderArray.push({ OrderCol: OrderCol, OrderType: OrderType });
            gridHeader.find("th[col]").removeAttr("class");
            if (OrderType == 1)
                $(this).addClass("OrderUp");
            else $(this).addClass("OrderDown");
            PlaceGrid();
            return false;
        });
        if (!constHeight) {
            $(window).resize(function () {
                Easy.Processor(function () { Height(Grid.parent().height()); }, 200);
                
            });
            $(function () {
                setTimeout(function () { Height(Grid.parent().height()); }, 100);
            });
        }
        var pheight = Grid.parent().height();
        if (!constHeight) {
            Height(pheight);
        }
    }
    function InitData(gdata) {
        gridHeader.find(".CheckBoxAll").prop("checked", false).change();
        gridBody.html("");
        var tableBody = $("<table cellpadding='0' cellspacing='0'></table>");
        var trBs = "";
        var width = 0;
        for (var j = 0; j < gdata.Rows.length; j++) {
            var cl = "trBe";
            if (j % 2 == 0)
                cl = "trAf";
            var trB = "<tr class='" + cl + "'>";
            if (CheckBox && chVale) {
                trB += "<td style='width:20px' align='center'><input type='checkbox' class='CheckBoxItem' val='" + gdata.Rows[j][chVale] + "' /></td>";
                if (j == 0) {
                    width += 20;
                }
            }
            for (var item in model) {
                if (model[item].Hidden) {
                    continue;
                }
                var colWidth = 150;
                if (model[item].Width) {
                    colWidth = model[item].Width;
                }
                trB += "<td style='width:" + colWidth + "px'><div class='coData' style='width:" + (colWidth - 2) + "px'>" + GetTempleteValue(item, gdata.Rows[j]) + "</div></td>";
                if (j == 0) {
                    width += colWidth;
                }
            }
            trB += "</tr>";
            trBs += trB;
            if (rowDataBindEventHandler) {
                rowDataBindEventHandler.call(this, gdata.Rows[j]);
            }
        }
        tableBody.html(trBs);
        gridBody.html(tableBody);
        tableBody.width(width);
        gridBody.find("tr").find(".CheckBoxItem").click(function () {
            CheckBack();
        });
        pageIndex = gdata.PageIndex;
        pageSize = gdata.PageSize;
        allPage = parseInt(gdata.RecordCount / gdata.PageSize);
        allPage = allPage == 0 ? 1 : allPage;
        if (gdata.RecordCount > gdata.PageSize)
            allPage += gdata.RecordCount % gdata.PageSize == 0 ? 0 : 1;
        Grid.find("#GridPageSize").val(pageSize);
        Grid.find("#GridPageIdex").val(pageIndex + 1);
        Grid.find("#PageInfo").html("[" + (pageIndex + 1) + "/" + allPage + "]");
        Grid.find("#Count").html(Easy.GridLan.AllRecord.replace("{0}", gdata.RecordCount));
        gridBody.scrollLeft(scrollLeft);
        if (Easy.UI) {
            Easy.UI.CheckBox();
        }
        if (dataSuccessEventHandler != null) {
            dataSuccessEventHandler.call();
        }
        if (gdata.Rows.length == 0) {
            var emptyTable = "<table><tr>";
            var noData = Easy.GridLan.NoData;
            for (var item in model) {
                if (model[item].Hidden) {
                    continue;
                }
                var colWidth = 150;
                if (model[item].Width) {
                    colWidth = model[item].Width;
                }
                emptyTable += "<td style='width:" + colWidth + "px;border-bottom:none;'><div class='coData' style='width:" + (colWidth - 2) + "px'>" + noData + "</div></td>";
                noData = "";
            }
            emptyTable += "</tr></table>";
            gridBody.html(emptyTable);
        }
    }
    function PlaceGrid(queryString) {
        if (isLoading) {
            return;
        }
        if (!queryString) {
            queryString = "";
        }
        if (queryString != QueryString && queryString != "") {
            QueryString = queryString;
            pageIndex = 0;
        }
        isLoading = true;
        Grid.find("#PageGo").addClass("InnerLonding");
        scrollLeft = gridBody.scrollLeft();
        var Group = [];
        var Cond = [];
        gridSearch.find("input").not("[type='button']").each(function () {
            if ($(this).val() != "") {
                switch ($(this).attr("DataType")) {
                    case "Select": {
                        var values = $(this).val().split(',');
                        var Conditions = [];
                        for (var i = 0; i < values.length; i++) {
                            var Condition = { Property: $(this).attr("name"), Value: values[i], DataType: $(this).attr("DataType"), OperatorType: 1, ConditionType: 2 };
                            Conditions.push(Condition);
                        }
                        var ConditionItem = { Conditions: Conditions };
                        Group.push(ConditionItem);
                        break;
                    }
                    case "Decimal":
                    case "Single":
                    case "Int16":
                    case "Int64":
                    case "Int32": {
                        var Conditions = [];
                        var values = $(this).val().split('@');
                        if (values.length == 3) {
                            if (values[0] != "") {
                                var ConditionGreaterThanOrEqualTo = { Property: $(this).attr("name"), Value: values[0], DataType: $(this).attr("DataType"), OperatorType: 3, ConditionType: 1 };
                                Conditions.push(ConditionGreaterThanOrEqualTo);
                            }
                            if (values[1] != "") {
                                var ConditionLessThanOrEqualTo = { Property: $(this).attr("name"), Value: values[1], DataType: $(this).attr("DataType"), OperatorType: 5, ConditionType: 1 };
                                Conditions.push(ConditionLessThanOrEqualTo);
                            }
                            if (values[2] != "") {
                                var ConditionEqual = { Property: $(this).attr("name"), Value: values[2], DataType: $(this).attr("DataType"), OperatorType: 1, ConditionType: 1 };
                                Conditions.push(ConditionEqual);
                            }
                            var ConditionItem = { Conditions: Conditions };
                            Group.push(ConditionItem);
                        }
                        break;
                    }
                    case "DateTime": {
                        var Conditions = [];
                        var values = $(this).val().split('@');
                        if (values.length == 3) {
                            if (values[0] != "") {
                                values[0] += " 0:0:0";
                                var ConditionGreaterThanOrEqualTo = { Property: $(this).attr("name"), Value: values[0], DataType: $(this).attr("DataType"), OperatorType: 3, ConditionType: 1 };
                                Conditions.push(ConditionGreaterThanOrEqualTo);
                            }
                            if (values[1] != "") {
                                values[1] += " 23:59:59";
                                var ConditionLessThanOrEqualTo = { Property: $(this).attr("name"), Value: values[1], DataType: $(this).attr("DataType"), OperatorType: 5, ConditionType: 1 };
                                Conditions.push(ConditionLessThanOrEqualTo);
                            }
                            if (values[2] != "") {
                                var ConditionGreaterThanOrEqualTo = { Property: $(this).attr("name"), Value: values[2] + " 0:00:00", DataType: $(this).attr("DataType"), OperatorType: 3, ConditionType: 1 };
                                Conditions.push(ConditionGreaterThanOrEqualTo);
                                var ConditionLessThanOrEqualTo = { Property: $(this).attr("name"), Value: values[2] + " 23:59:59", DataType: $(this).attr("DataType"), OperatorType: 5, ConditionType: 1 };
                                Conditions.push(ConditionLessThanOrEqualTo);
                            }
                            var ConditionItem = { Conditions: Conditions };
                            Group.push(ConditionItem);
                        }
                        break;
                    }
                    default: {
                        Cond.push({ Property: $(this).attr("name"), Value: $(this).val(), DataType: $(this).attr("DataType"), OperatorType: $(this).attr("OperatorType"), ConditionType: 1 });
                        break;
                    }
                }
            }
        });
        $.ajax({
            url: DataUrl + (QueryString == "" ? "" : "?" + QueryString),
            data: { ConditionGroups: Group, Conditions: Cond, PageIndex: pageIndex, PageSize: pageSize, OrderBy: orderArray },
            type: "post",
            success: GetDataSucess,
            error: function (msg) {
                isLoading = false;
                Grid.find("#PageGo").removeClass("InnerLonding");
                gridBody.html("<h1>Grid.Ajax.Error:" + msg.status + "</h1>");
            }
        }, "json");
    }
    function CheckBack() {
        var select = [];
        Grid.find(".CheckBoxItem:input:checked").each(function (i) {
            if ($(this).prop("checked")&&jsonData && jsonData.Rows) {
                select.push(jsonData.Rows[i]);
            }
        });
        if (checkBoxEventHandler) {
            checkBoxEventHandler.call(this, select);
        }
    }
    function OnRowDataBind(fun) {
        rowDataBindEventHandler = fun;
        return returnObj;
    }
    function OnCheckBoxChange(fun) {
        checkBoxEventHandler = fun;
        return returnObj;
    }
    function OnSuccess(fun) {
        dataSuccessEventHandler = fun;
        return returnObj;
    }
    function SetColumnTemplete(columnName, strFormate) {
        if (!templetes.ContainsValue(columnName)) {
            templetes.push(columnName);
            templeteValues.push(strFormate);
        }
        return returnObj;
    }
    function GetTempleteValue(columnName, data) {
        var ind = templetes.ValueIndex(columnName);
        if (ind != -1) {
            var reStr = templeteValues[ind];
            for (var item in data) {
                while (reStr.indexOf("{" + item + "}") >= 0) {
                    reStr = reStr.replace("{" + item + "}", data[item]);
                }
            }
            return reStr;
        }
        else {
            return data[columnName];
        }
    }
    function StartSearch(e) {
        if (e.keyCode == 13) {
            pageIndex = 0;
            PlaceGrid();
        }
    }
    /*条件窗口*/
    function ShowNumCondition() {
        if ($(".NumConditionBox").length >= 1) {
            $(".NumConditionBox").remove();
            $(document).unbind("click", RemoveConditionBox);
            return;
        }
        var Boxhtml = [];
        Boxhtml.push("<div class='NumConditionBox'>");
        Boxhtml.push("<div><label for='NumGreaterThanOrEqualTo'>>=</label><input type='text' id='NumGreaterThanOrEqualTo' /><div style='clear:both'></div></div>");
        Boxhtml.push("<div><label for='NumLessThanOrEqualTo'><=</label><input type='text' id='NumLessThanOrEqualTo' /><div style='clear:both'></div></div>");
        Boxhtml.push("<div><label for='NumEqual'>=</label><input type='text' id='NumEqual' /><div style='clear:both'></div></div>");
        Boxhtml.push("</div>");
        var numBox = $(Boxhtml.join(""));
        var effect = gridSearch.find("#" + $(this).attr("ValuePlace"));
        $("body").append(numBox);
        switch (effect.attr("DataType")) {
            case "DateTime": {
                var dateFormat = effect.attr("Format");
                if (!dateFormat) {
                    dateFormat = "yyyy/MM/dd";
                }
                numBox.find("input").attr("DateFormat", dateFormat);
                numBox.find("input").attr("ValueType", "Date");
                numBox.find("input").addClass("Date");
                if (Easy.UI) {
                    Easy.UI.DateInput(numBox.find("input"));
                }
                break;
            }
            case "Decimal":
            case "Single":
            case "Int16":
            case "Int64":
            case "Int32": {
                numBox.find("input").attr("ValueType", "Num");
                numBox.find("input").addClass("Number");
                if (Easy.UI) {
                    Easy.UI.NumberInput();
                }
                break;
            }
        }
        var left = $(this).offset().left;
        if ($(this).offset().left + numBox.width() >= Easy.WindowSize().width) {
            left = Easy.WindowSize().width - numBox.width() - 30;
        }
        numBox.css("left", left);
        numBox.css("top", $(this).offset().top);
        values = effect.val().split("@");
        if (values.length == 3) {
            numBox.find("#NumGreaterThanOrEqualTo").val(values[0]);
            numBox.find("#NumLessThanOrEqualTo").val(values[1]);
            numBox.find("#NumEqual").val(values[2]);
        }

        numBox.find("input").keydown(function (e) {
            if (e.keyCode == 13) {
                var value1 = numBox.find("#NumGreaterThanOrEqualTo").val();
                var value2 = numBox.find("#NumLessThanOrEqualTo").val();
                var value3 = numBox.find("#NumEqual").val();
                effect.val(value1 + "@" + value2 + "@" + value3);
                PlaceGrid();
                if (effect.val() != "@@") {
                    effect.parent().css("background-color", "#CCD4FF");
                }
                $(".NumConditionBox").remove();
                $(document).unbind("click", RemoveConditionBox);
            }
        });
        setTimeout(function () {
            $(document).bind("click", RemoveConditionBox);
        }, 10);
    }
    function RemoveConditionBox(e) {
        var id = e.target.id;
        if (id == "NumGreaterThanOrEqualTo" || id == "NumLessThanOrEqualTo" || id == "NumEqual") {
            return false;
        }

        $(".NumConditionBox").remove();
        $(document).unbind("click", RemoveConditionBox);
    }
    function ClearCondition() {
        if ($(this).attr("ValuePlace")) {
            var effect = gridSearch.find("#" + $(this).attr("ValuePlace"));
            if (effect.val() == "")
                return;
            effect.val("");
            effect.parent().css("background-color", "");
            PlaceGrid();
        }
        else {
            gridSearch.find("input").val("");
            gridSearch.find("input").parent().css("background-color", "");
            gridSearch.find("select").children("option").removeAttr("selected");
            gridSearch.find("select").change();
            PlaceGrid();
        }
        return false;
    }
    /*public function*/
    function SetUrl(value) {
        DataUrl = value;
        return returnObj;
    }
    function SetGridArea(eleID) {
        ele = "#" + eleID;
        var pl = $(ele);
        if (pl.length > 0)
            pl.append(Grid);
        else $("body").append(Grid);
        Easy.Grids.AddGrid(eleID, returnObj);
        return returnObj;
    }
    function ShowCheckBox(valueColumn) {
        chVale = valueColumn;
        CheckBox = true;
        return returnObj;
    }
    function Height(value) {
        Grid.find(".GridBody").height(value - Grid.find(".GridHeader").height() - Grid.find(".GridSearch").height() - Grid.find(".Gridfoot").height() - Grid.find(".GridToolBar").height() - heightFix);
        constHeight = true;
        return returnObj;
    }
    function SetModel(viewModel) {
        model = viewModel;
        InitGrid();
        return returnObj;
    }
    function SearchAble() {
        canSearch = true;
        return returnObj;
    }
    function OrderBy(property, sortType) {
        if (!OrderCol) {
            OrderCol = property;
            OrderType = sortType;
        }
        orderArray.push({ OrderCol: property, OrderType: sortType });
        return returnObj;
    }
    function SetToolbar(selector) {
        $(selector).appendTo(Grid.find(".GridToolBar"));
        Grid.find(".GridToolBar").show();
        return returnObj;
    }
    function SetDeleteUrl(url) {
        deleteUrl = url;
        Grid.find(".GridDelete").show();
        return returnObj;
    }
    function Delete() {
        var vals = [];
        gridBody.find(".CheckBoxItem:checked").each(function () {
            vals.push($(this).attr("val"));
        });
        if (vals.length > 0) {
            Easy.ShowMessageBox(Easy.GridLan.DeleteTitle, Easy.GridLan.DeleteConfirm, function () {
                Grid.find(".GridDelete").addClass("InnerLonding");
                $.ajax({
                    url: deleteUrl,
                    data: { ids: vals.join(',') },
                    success: function (data) {
                        if (data.Status != 1) {
                            Easy.ShowMessageBox(Easy.GridLan.DeleteTitle, data.Message);
                        }
                        if (data.Status != 3) {
                            PlaceGrid();
                        }
                        Grid.find(".GridDelete").removeClass("InnerLonding");
                    },
                    error: function (msg) {
                        Grid.find(".GridDelete").removeClass("InnerLonding");
                        Easy.ShowMessageBox(Easy.GridLan.DeleteTitle, msg.status);
                    },
                    type: "post"
                }, "json");
            }, true);
        }
        else {
            Easy.ShowMessageBox(Easy.GridLan.DeleteTitle, Easy.GridLan.SelectWorm);
        }
    }
    var returnObj = {
        "Show": PlaceGrid, "Reload": PlaceGrid, "OnRowDataBind": OnRowDataBind, "SetColumnTemplete": SetColumnTemplete,
        "SetUrl": SetUrl, "SetGridArea": SetGridArea, "ShowCheckBox": ShowCheckBox,
        "OnCheckBoxChange": OnCheckBoxChange, "Height": Height, "SetModel": SetModel,
        "SearchAble": SearchAble, "OnSuccess": OnSuccess, "OrderBy": OrderBy, "SetBoolBar": SetToolbar, "SetDeleteUrl": SetDeleteUrl,
        "Delete": Delete
    }
    return returnObj;
});

Easy.Grids = (function () {
    var gridNames = new Array();
    var grids = new Array();
    function AddGrid(name, grid) {
        if (gridNames.ContainsValue(name)) {
            var index = gridNames.ValueIndex(name);
            grids[index] = grid;
        }
        else {
            gridNames.push(name);
            grids.push(grid);
        }
    }
    function Find(gridName) {
        if (gridNames.ContainsValue(gridName)) {
            var index = gridNames.ValueIndex(gridName);
            return grids[index];
        }
    }
    return { AddGrid: AddGrid, Find: Find }
})();


