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
    var scrollLeft = 0;
    var constHeight = false;
    var isLoading = false;
    var QueryString = "";
    var deleteUrl = "";
    var heightFix = 15;
    var jsonData;

    var returnObj = {
        "Show": placeGrid, "Reload": placeGrid, "OnRowDataBind": onRowDataBind, "SetColumnTemplete": setColumnTemplete,
        "SetUrl": setUrl, "SetGridArea": setGridArea, "ShowCheckBox": showCheckBox,
        "OnCheckBoxChange": onCheckBoxChange, "Height": height, "SetModel": setModel,
        "SearchAble": searchAble, "OnSuccess": onSuccess, "OrderBy": orderBy, "SetBoolBar": setToolbar, "SetDeleteUrl": setDeleteUrl,
        "Delete": deleteData
    }

    var grid = $(["<div class='EasyGrid panel panel-default'>",
    "<div class='GridContent'><div class='GridToolBar panel-heading'></div><div class='GridHeader'></div><div class='GridBody'></div></div>",
    "<div class='Gridfoot'>",
    "<div class='GridDelete' title='" + Easy.GridLan.Delete + "'>&nbsp;</div>",
    "<label>",
    Easy.GridLan.PageSize,
    "</label>",
    "<select id='GridPageSize' easy='easy'>",
    "<option value='10'>10</option>",
    "<option value='20' selected='selected'>20</option>",
    "<option value='30'>30</option>",
    "<option value='50'>50</option>",
    "<option value='100'>100</option>",
    "</select>",
    "<label for='GridPageIdex'>",
    Easy.GridLan.CurrentPage,
    "</label><input id='GridPageIdex' type='text' value='0' ValueType='Num' />",
    "<label id='PageInfo'></label>",
    "<label id='Count'></label>",
    "<a id='PageGo' class='glyphicon glyphicon-repeat' title='" + Easy.GridLan.PageGo + "'>",
    "</a>",
    "<a id='PageFirst' class='glyphicon glyphicon-fast-backward' title='" + Easy.GridLan.FirstPage + "'>",
    "</a>",
    "<a id='PagePre' class='glyphicon glyphicon-step-backward' title='" + Easy.GridLan.PrePage + "'>",
    "</a>",
    "<a id='PageNext' class='glyphicon glyphicon-step-forward' title='" + Easy.GridLan.NextPage + "'>",
    "</a>",
    "<a id='PageLast' class='glyphicon glyphicon-fast-forward' title='" + Easy.GridLan.LastPage + "'>",
    "</a>",
    "</div></div>"].join(""));
    var gridHeader = grid.find(".GridHeader");
    var gridBody = grid.find(".GridBody");
    var search = $("<tbody class='GridSearch'></tbody>");
    gridBody.scroll(function () {
        gridHeader.scrollLeft($(this).scrollLeft());
    });
    grid.find("#GridPageIdex").BeNumber(Easy.GridLan.PageNumberError);
    grid.find("#GridPageIdex").blur(function () {
        if ($(this).val() == "") {
            $(this).val(pageIndex + 1);
        }
    });
    grid.find("#GridPageIdex").keyup(function (e) {
        if (e.keyCode == 13) {
            grid.find("#PageGo").click();
        }
    });
    grid.find("#GridPageSize").change(function () {
        pageIndex = 0;
        pageSize = $(this).val();
        placeGrid();
    });
    grid.find("#PageGo").click(function () {
        pageIndex = parseInt(grid.find("#GridPageIdex").val()) - 1;
        if (pageIndex < 0 || pageIndex >= allPage) {
            pageIndex = 0;
        }
        placeGrid();
    });
    grid.find("#PageFirst").click(function () {
        if (pageIndex == 0)
            return;
        pageIndex = 0;
        placeGrid();
    });
    grid.find("#PagePre").click(function () {
        pageIndex -= 1;
        if (pageIndex < 0) {
            pageIndex = 0;
            return;
        }
        placeGrid();
    });
    grid.find("#PageNext").click(function () {
        pageIndex += 1;
        if (pageIndex >= allPage) {
            pageIndex = allPage - 1;
            return;
        }
        placeGrid();
    });
    grid.find("#PageLast").click(function () {
        if (pageIndex == allPage - 1) {
            return;
        }
        pageIndex = allPage - 1;
        placeGrid();
    });
    grid.find(".GridDelete").click(function () { deleteData(); });
    if (json) {
        if (json.url) { DataUrl = json.url; }
        if (json.id) {
            ele = "#" + json.id;
            $(ele).append(grid);
        }
    }

    function getDataSucess(data) {
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
            initGrid(jsonData);
        }
        initData(jsonData);
        grid.find("#PageGo").removeClass("InnerLonding");
    }
    function initGrid(gdata) {
        var width = 0;
        var tableHeader = $("<table class='header' cellpadding='0' cellspacing='0'></table>");
        var trH = "<tr class='trTitle'>";
        if (CheckBox && chVale) {
            trH += "<th style='width:20px' align='center'><input type='checkbox' class='CheckBoxAll' /></th>";
            width += 20;
        }
        if (!model) {
            model = gdata.Columns;
        }
        for (var itemProperty in model) {
            if (model.hasOwnProperty(itemProperty)) {
                if (model[itemProperty].Hidden)
                    continue;
                var itemWidth = 150;
                if (model[itemProperty].Width)
                    itemWidth = model[itemProperty].Width;
                if (!OrderCol)
                    OrderCol = model[itemProperty].Name;
                trH += "<th style='width:" + itemWidth + "px' col='" + model[itemProperty].Name + "'><div class='coData'>" + model[itemProperty].DisplayName + "</div></th>";
                width += itemWidth;
            }
        }
        tableHeader.html("<thead>" + trH + "</thead>");
        tableHeader.width(width);
        gridHeader.html(tableHeader);
        if (OrderType === 1) {
            gridHeader.find("th[col='" + OrderCol + "']").addClass("OrderUp");
        }
        else {
            gridHeader.find("th[col='" + OrderCol + "']").addClass("OrderDown");
        }
        trH = "";
        if (canSearch) {
            trH = "<tr>";
            if (CheckBox && chVale) {
                trH += "<th style='width:20px' align='center'><input type='button' class='ClearSearch' /></th>";
            }
            for (var itemName in model) {
                if (model.hasOwnProperty(itemName)) {
                    var input = "";
                    var item = model[itemName];
                    if (item.Hidden)
                        continue;
                    switch (item.DataType) {
                        case "String":
                            input = "<input type='text' DataType='" + item.DataType + "' id='Search_" + item.Name + "' name='" + item.Name + "' OperatorType='7' title='" + item.DisplayName + "'/>";
                            break;
                        case "Boolean":
                            {
                                input = "<input type='hidden' DataType='" + item.DataType + "' id='Search_" + item.Name + "' name='" + item.Name + "' OperatorType='1' title='" + item.DisplayName + "'/>";
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
                                input = "<input type='hidden' DataType='" + item.DataType + "' id='Search_" + item.Name + "' name='" + item.Name + "' OperatorType='1' title='" + item.DisplayName + "'/>" +
                                    "<div class='RangeOption clearfix'><input class='RangeAdd' type='button' ValuePlace='Search_" + item.Name + "' value='' title='" + item.DisplayName + "'/><input type='button' class='RangeClear' ValuePlace='Search_" + item.Name + "' value='' title='" + item.DisplayName + "'/></div>";
                                break;
                            }
                        case "DateTime":
                            {
                                input = "<input type='hidden' DataType='" + item.DataType + "' Format='" + item.Format + "' id='Search_" + item.Name + "' name='" + item.Name + "' OperatorType='1' title='" + item.DisplayName + "'/>" +
                                    "<div class='RangeOption clearfix'><input class='RangeAdd' type='button' ValuePlace='Search_" + item.Name + "' value='' title='" + item.DisplayName + "'/><input type='button' class='RangeClear' ValuePlace='Search_" + item.Name + "' value='' title='" + item.DisplayName + "'/></div>";
                                break;
                            }
                        case "Select":
                            {
                                input = "<input type='hidden' DataType='" + item.DataType + "' id='Search_" + item.Name + "' name='" + item.Name + "' OperatorType='1' title='" + item.DisplayName + "'/>";
                                var option = "<option></option>";
                                option += item.Data || "";
                                input += "<select class='easy' multiple='multiple' ValuePlace='Search_" + item.Name + "'>" + option + "</select>";
                                break;
                            }
                        case "None":
                            input = "<span></span>";
                            break;
                        default:
                            input = "<input type='text' DataType='" + item.DataType + "' id='Search_" + item.Name + "' name='" + item.Name + "' OperatorType='7' title='" + item.DisplayName + "'/>";
                            break;
                    }
                    var colWidth = 150;
                    if (item.Width)
                        colWidth = item.Width;
                    trH += "<th style='width:" + colWidth + "px'><div class='searchbox'>" + input + "</div></th>";
                }
            }
            trH += "<th style='width:20px'></th>";
            trH += " </tr>";
            search.html(trH);
            gridHeader.find("table").append(search);
            search.find("input").keydown(startSearch);
            search.find(".RangeAdd").click(showRangeCondition);
            search.find(".RangeClear").click(clearCondition);
            search.find(".ClearSearch").click(clearCondition);
            search.find("select").change(function () {
                pageIndex = 0;
                search.find("#" + $(this).attr("ValuePlace")).val($(this).val());
                placeGrid();
            });
            if (Easy.UI) {
                Easy.UI.DropDownList();
                Easy.UI.MultiSelect();
                Easy.UI.CheckBox();
                Easy.UI.NumberInput();
            }
        } else {
            search.hide();
        }
        gridHeader.find(".CheckBoxAll").click(function () {
            gridBody.find("tr").find(".CheckBoxItem").prop("checked", $(this).prop("checked"));
            checkBack();
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
            placeGrid();
            return false;
        });
        if (!constHeight) {
            $(window).resize(function () {
                Easy.Processor(function () { height(grid.parent().height()); }, 200);

            });
            $(function () {
                setTimeout(function () { height(grid.parent().height()); }, 100);
            });
        }
        var pheight = grid.parent().height();
        if (!constHeight) {
            height(pheight);
        }
    }
    function initData(gdata) {
        gridHeader.find(".CheckBoxAll").prop("checked", false).change();
        gridBody.html("");
        var tableBody = $("<table class='body' cellpadding='0' cellspacing='0'></table>");
        var trBs = "";
        var width = 0;
        for (var j = 0; j < gdata.Rows.length; j++) {
            var cl = "trBe";
            if (j % 2 === 0)
                cl = "trAf";
            var trB = "<tr class='" + cl + "'>";
            if (CheckBox && chVale) {
                trB += "<td style='width:20px' align='center'><input type='checkbox' class='CheckBoxItem' val='" + gdata.Rows[j][chVale] + "' /></td>";
                if (j === 0) {
                    width += 20;
                }
            }
            for (var p in model) {
                if (model.hasOwnProperty(p)) {
                    if (model[p].Hidden) {
                        continue;
                    }
                    var dataWidth = 150;
                    if (model[p].Width) {
                        dataWidth = model[p].Width;
                    }
                    trB += "<td " + (j === 0 ? "style='width:" + dataWidth + "px'" : "") + "><div class='coData' style='width:" + dataWidth + "px'>" + getTempleteValue(p, gdata.Rows[j]) + "</div></td>";
                    if (j === 0) {
                        width += dataWidth;
                    }
                }
            }
            trB += "</tr>";
            trBs += trB;
            if (rowDataBindEventHandler) {
                rowDataBindEventHandler.call(this, gdata.Rows[j]);
            }
        }
        tableBody.html("<tbody>" + trBs + "</tbody>");
        gridBody.html(tableBody);
        tableBody.width(width);
        gridBody.find("tr").find(".CheckBoxItem").click(function () {
            checkBack();
        });
        pageIndex = gdata.PageIndex;
        pageSize = gdata.PageSize;
        allPage = parseInt(gdata.RecordCount / gdata.PageSize);
        allPage = allPage === 0 ? 1 : allPage;
        if (gdata.RecordCount > gdata.PageSize)
            allPage += gdata.RecordCount % gdata.PageSize == 0 ? 0 : 1;
        grid.find("#GridPageSize").val(pageSize);
        grid.find("#GridPageIdex").val(pageIndex + 1);
        grid.find("#PageInfo").html("[" + (pageIndex + 1) + "/" + allPage + "]");
        grid.find("#Count").html(Easy.GridLan.AllRecord.replace("{0}", gdata.RecordCount));
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
                if (model.hasOwnProperty(item)) {
                    if (model[item].Hidden) {
                        continue;
                    }
                    var colWidth = 150;
                    if (model[item].Width) {
                        colWidth = model[item].Width;
                    }
                    emptyTable += "<td style='width:" + colWidth + "px;border-bottom:none;'><div class='coData'>" + noData + "</div></td>";
                    noData = "";
                }
            }
            emptyTable += "</tr></table>";
            gridBody.html(emptyTable);
        }
    }
    function placeGrid(queryString) {
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
        grid.find("#PageGo").addClass("InnerLonding");
        scrollLeft = gridBody.scrollLeft();
        var Group = [];
        var Cond = [];
        search.find("input").not("[type='button']").each(function () {
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
                                Conditions.push({ Property: $(this).attr("name"), Value: values[0], DataType: $(this).attr("DataType"), OperatorType: 3, ConditionType: 1 });
                            }
                            if (values[1] != "") {
                                Conditions.push({ Property: $(this).attr("name"), Value: values[1], DataType: $(this).attr("DataType"), OperatorType: 5, ConditionType: 1 });
                            }
                            if (values[2] != "") {
                                Conditions.push({ Property: $(this).attr("name"), Value: values[2], DataType: $(this).attr("DataType"), OperatorType: 1, ConditionType: 1 });
                            }
                            Group.push({ Conditions: Conditions });
                        }
                        break;
                    }
                    case "DateTime": {
                        var Conditions = [];
                        var values = $(this).val().split('@');
                        if (values.length == 3) {
                            if (values[0] != "") {
                                if (values[0].indexOf(":") < 0) {
                                    values[0] += " 0:0:0";
                                }
                                var ConditionGreaterThanOrEqualTo = { Property: $(this).attr("name"), Value: values[0], DataType: $(this).attr("DataType"), OperatorType: 3, ConditionType: 1 };
                                Conditions.push(ConditionGreaterThanOrEqualTo);
                            }
                            if (values[1] != "") {
                                if (values[1].indexOf(":") < 0) {
                                    values[1] += " 23:59:59";
                                }
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
            success: getDataSucess,
            error: function (msg) {
                isLoading = false;
                grid.find("#PageGo").removeClass("InnerLonding");
                gridBody.html("<h1>Grid.Ajax.Error:" + msg.status + "</h1>");
            }
        }, "json");
    }
    function checkBack() {
        var select = [];
        grid.find(".CheckBoxItem:input:checked").each(function (i) {
            if ($(this).prop("checked") && jsonData && jsonData.Rows) {
                select.push(jsonData.Rows[i]);
            }
        });
        if (checkBoxEventHandler) {
            checkBoxEventHandler.call(this, select);
        }
    }
    function onRowDataBind(fun) {
        rowDataBindEventHandler = fun;
        return returnObj;
    }
    function onCheckBoxChange(fun) {
        checkBoxEventHandler = fun;
        return returnObj;
    }
    function onSuccess(fun) {
        dataSuccessEventHandler = fun;
        return returnObj;
    }
    function setColumnTemplete(columnName, strFormate) {
        if (!templetes.ContainsValue(columnName)) {
            templetes.push(columnName);
            templeteValues.push(strFormate);
        }
        return returnObj;
    }
    function getTempleteValue(columnName, data) {
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
    function startSearch(e) {
        if (e.keyCode == 13) {
            pageIndex = 0;
            placeGrid();
        }
    }
    /*条件窗口*/
    function showRangeCondition() {

        var th = $(this);

        if (th.data("shown")) {
            return;
        }
        th.data("shown", true);
        var rangeBox = $([
            "<div class='RangeConditionBox form' tabindex='-1'>",
            "<div class='form-group'><div class='input-group'><label for='GreaterThanOrEqualTo' class='input-group-addon'>>=</label><input  class='form-control' type='text' id='GreaterThanOrEqualTo' /></div></div>",
            "<div class='form-group'><div class='input-group'><label for='LessThanOrEqualTo' class='input-group-addon'><=</label><input  class='form-control' type='text' id='LessThanOrEqualTo' /></div></div>",
            "<div class='form-group'><div class='input-group'><label for='EqualTo' class='input-group-addon'>&nbsp;=&nbsp;</label><input type='text'  class='form-control' id='EqualTo' /></div></div>",
            "</div>"
        ].join(""));

        var effect = search.find("#" + th.attr("ValuePlace"));
        gridBody.append(rangeBox);
        var normalLeft = th.offset().left - gridBody.offset().left + gridBody.scrollLeft();
        if (normalLeft + rangeBox.outerWidth() > grid.outerWidth() + gridBody.scrollLeft()) {
            normalLeft -= normalLeft + rangeBox.outerWidth() - grid.outerWidth() - gridBody.scrollLeft() + 3;
        }
        rangeBox.css("left", normalLeft);
        switch (effect.attr("DataType")) {
            case "DateTime": {
                var dateFormat = effect.attr("Format");
                if (!dateFormat) {
                    dateFormat = "YYYY/MM/DD";
                }
                rangeBox.find(".form-control").attr("DateFormat", dateFormat).attr("ValueType", "Date").addClass("Date");
                if (Easy.UI) {
                    Easy.UI.DateInput(rangeBox.find("input"));
                }
                break;
            }
            case "Decimal":
            case "Single":
            case "Int16":
            case "Int64":
            case "Int32": {
                rangeBox.find("input").attr("ValueType", "Num").addClass("Number");
                if (Easy.UI) {
                    Easy.UI.NumberInput();
                }
                break;
            }
        }
        var values = effect.val().split("@");
        if (values.length === 3) {
            rangeBox.find("#GreaterThanOrEqualTo").val(values[0]);
            rangeBox.find("#LessThanOrEqualTo").val(values[1]);
            rangeBox.find("#EqualTo").val(values[2]);
        }
        function setValue() {
            var value1 = rangeBox.find("#GreaterThanOrEqualTo").val();
            var value2 = rangeBox.find("#LessThanOrEqualTo").val();
            var value3 = rangeBox.find("#EqualTo").val();
            effect.val(value1 + "@" + value2 + "@" + value3);
            if (effect.val() !== "@@") {
                effect.parent().addClass("bg-info");
            }
        }

        var closeThread;
        function removeConditionBox() {
            if (closeThread) return;
            closeThread = setTimeout(function () {
                th.data("shown", false);
                rangeBox.remove();
            }, 50);
        }

        rangeBox.find("input").on("keyup", function (e) {
            if (e.keyCode === 13) {
                removeConditionBox();
                placeGrid();
                return;
            }
            setValue();
        }).on("dp.change", setValue).on("focus", function () {
            clearTimeout(closeThread);
            closeThread = null;
        }).on("blur", removeConditionBox);

        rangeBox.on("focus", function () {
            clearTimeout(closeThread);
            closeThread = null;
        }).on("blur", removeConditionBox).focus();
    }
    function clearCondition() {
        if ($(this).attr("ValuePlace")) {
            var effect = search.find("#" + $(this).attr("ValuePlace"));
            effect.val("");
            effect.parent().removeClass("bg-info");
        }
        else {
            search.find("input").val("");
            search.find("input").parent().css("background-color", "");
            search.find("select").children("option").removeAttr("selected");
            search.find("select").change();
        }
        $(".RangeAdd").data("shown", false);
        placeGrid();
        return false;
    }
    /*public function*/
    function setUrl(value) {
        DataUrl = value;
        return returnObj;
    }
    function setGridArea(eleID) {
        ele = "#" + eleID;
        var pl = $(ele);
        if (pl.length > 0)
            pl.append(grid);
        else $("body").append(grid);
        Easy.Grids.AddGrid(eleID, returnObj);
        return returnObj;
    }
    function showCheckBox(valueColumn) {
        chVale = valueColumn;
        CheckBox = true;
        return returnObj;
    }
    function height(value) {
        grid.find(".GridBody").height(value - grid.find(".GridHeader").height() - grid.find(".Gridfoot").height() - grid.find(".GridToolBar").height() - heightFix);
        constHeight = true;
        return returnObj;
    }
    function setModel(viewModel) {
        model = viewModel;
        initGrid();
        return returnObj;
    }
    function searchAble() {
        canSearch = true;
        return returnObj;
    }
    function orderBy(property, sortType) {
        if (!OrderCol) {
            OrderCol = property;
            OrderType = sortType;
        }
        orderArray.push({ OrderCol: property, OrderType: sortType });
        return returnObj;
    }
    function setToolbar(selector) {
        $(selector).appendTo(grid.find(".GridToolBar"));
        grid.find(".GridToolBar").show();
        return returnObj;
    }
    function setDeleteUrl(url) {
        deleteUrl = url;
        grid.find(".GridDelete").show();
        return returnObj;
    }
    function deleteData() {
        var vals = [];
        gridBody.find(".CheckBoxItem:checked").each(function () {
            vals.push($(this).attr("val"));
        });
        if (vals.length > 0) {
            Easy.ShowMessageBox(Easy.GridLan.DeleteTitle, Easy.GridLan.DeleteConfirm, function () {
                grid.find(".GridDelete").addClass("InnerLonding");
                $.ajax({
                    url: deleteUrl,
                    data: { ids: vals.join(',') },
                    success: function (data) {
                        if (data.Status != 1) {
                            Easy.ShowMessageBox(Easy.GridLan.DeleteTitle, data.Message);
                        }
                        if (data.Status != 3) {
                            placeGrid();
                        }
                        grid.find(".GridDelete").removeClass("InnerLonding");
                    },
                    error: function (msg) {
                        grid.find(".GridDelete").removeClass("InnerLonding");
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


