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
    AllRecord: "{0} 条记录",
    DeleteTitle: "提示",
    DeleteConfirm: "确定要删除选中项吗？",
    SelectWorm: "请至少选择一项进行操作！",
    Delete: "删除选中项",
    Yes: "是",
    No: "否"
}
Easy.Grid = (function (json) {
    var dataUrl = "";
    var ele;
    var chVale;
    var templetes = new Array();
    var templeteValues = new Array();
    var model = {};
    var canSearch = false;
    var rowDataBindEventHandler;
    var checkBoxEventHandler;
    var dataSuccessEventHandler;
    var checkBox = false;
    var orderArray = [];
    var orderCol = "";
    var orderType = 1;
    var scrollLeft = 0;
    var constHeight = false;
    var isLoading = false;
    var queryString = "";
    var deleteUrl = "";
    var heightFix = 15;
    var jsonData;
    var allPage = 0;

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


    if (json) {
        if (json.url) { dataUrl = json.url; }
        if (json.id) {
            ele = "#" + json.id;
            $(ele).append(grid);
        }
    }

    var gridHeader = grid.find(".GridHeader");
    var gridBody = grid.find(".GridBody");
    var search = $("<tbody class='GridSearch'></tbody>");


    var handlers = {};
    handlers.getPageIndex = function () {
        var pageIndex = parseInt(grid.find("#GridPageIdex").val());
        if (!(pageIndex >= 0)) {
            pageIndex = 1;
            grid.find("#GridPageIdex").val(pageIndex);
        }
        return pageIndex - 1;
    }
    handlers.getPageSize = function () {
        return parseInt(grid.find("#GridPageSize").val());
    }
    handlers.setPageIndex = function (index) {
        index = parseInt(index) || 0;
        grid.find("#GridPageIdex").val(index + 1);
    }
    handlers.reload = function (query) {
        if (isLoading) {
            return;
        }
        if (!query) {
            query = "";
        }
        if (query !== queryString && query !== "") {
            queryString = query;
            handlers.setPageIndex(0);
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
                                Conditions.push({ Property: $(this).attr("name"), Value: values[0], DataType: $(this).attr("DataType"), OperatorType: 3, ConditionType: 1 });
                            }
                            if (values[1] != "") {
                                if (values[1].indexOf(":") < 0) {
                                    values[1] += " 23:59:59";
                                }
                                Conditions.push({ Property: $(this).attr("name"), Value: values[1], DataType: $(this).attr("DataType"), OperatorType: 5, ConditionType: 1 });
                            }
                            if (values[2] != "") {
                                Conditions.push({ Property: $(this).attr("name"), Value: values[2] + " 0:00:00", DataType: $(this).attr("DataType"), OperatorType: 3, ConditionType: 1 });

                                Conditions.push({ Property: $(this).attr("name"), Value: values[2] + " 23:59:59", DataType: $(this).attr("DataType"), OperatorType: 5, ConditionType: 1 });
                            }
                            Group.push({ Conditions: Conditions });
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
            url: dataUrl + (queryString === "" ? "" : "?" + queryString),
            data: { ConditionGroups: Group, Conditions: Cond, PageIndex: handlers.getPageIndex(), PageSize: handlers.getPageSize(), OrderBy: orderArray },
            type: "post",
            success: function (data) {
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
                handlers.initGridData(jsonData);
                grid.find("#PageGo").removeClass("InnerLonding");
            },
            error: function (msg) {
                isLoading = false;
                grid.find("#PageGo").removeClass("InnerLonding");
                gridBody.html("<h1>Grid.Ajax.Error:" + msg.status + "</h1>");
            }
        }, "json");
    }
    handlers.loadPage = function (index) {
        handlers.setPageIndex(index);
        handlers.reload();
    }
    handlers.initGrid = function (viewModel) {
        var width = 0;
        var tableHeader = $("<table class='header' cellpadding='0' cellspacing='0'></table>");
        var trH = "<tr class='trTitle'>";
        if (checkBox && chVale) {
            trH += "<th style='width:20px' align='center'><input type='checkbox' class='CheckBoxAll' /></th>";
            width += 20;
        }
        model = viewModel;
        for (var itemProperty in model) {
            if (model.hasOwnProperty(itemProperty)) {
                if (model[itemProperty].Hidden)
                    continue;
                var itemWidth = 150;
                if (model[itemProperty].Width)
                    itemWidth = model[itemProperty].Width;
                if (!orderCol)
                    orderCol = model[itemProperty].Name;
                trH += "<th style='width:" + itemWidth + "px' col='" + model[itemProperty].Name + "'><div class='coData'>" + model[itemProperty].DisplayName + "</div></th>";
                width += itemWidth;
            }
        }
        tableHeader.html("<thead>" + trH + "</thead>");
        tableHeader.width(width);
        gridHeader.html(tableHeader);
        if (orderType === 1) {
            gridHeader.find("th[col='" + orderCol + "']").addClass("OrderUp");
        }
        else {
            gridHeader.find("th[col='" + orderCol + "']").addClass("OrderDown");
        }
        trH = "";
        if (canSearch) {
            trH = "<tr>";
            if (checkBox && chVale) {
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
                            input = "<input class='condition' type='text' DataType='" + item.DataType + "'  name='" + item.Name + "' OperatorType='7' title='" + item.DisplayName + "'/>";
                            break;
                        case "Boolean":
                            {
                                input = ["<select class='easy condition' DataType='" + item.DataType + "'  name='" + item.Name + "' OperatorType='1' title='" + item.DisplayName + "'>",
                                    "<option></option>",
                                    "<option value='true'>" + Easy.GridLan.Yes + "</option>",
                                    "<option value='false'>" + Easy.GridLan.No + "</option>",
                                    "</select>"].join("");
                                break;
                            }
                        case "Decimal":
                        case "Single":
                        case "Int16":
                        case "Int64":
                        case "Int32":
                            {
                                input = ["<div class='RangeOption clearfix'>",
                                    "<input class='condition' type='hidden' DataType='" + item.DataType + "'  name='" + item.Name + "' OperatorType='1' title='" + item.DisplayName + "'/>",
                                    "<input class='RangeAdd' type='button'  value='' title='" + item.DisplayName + "'/>",
                                    "<input type='button' class='RangeClear'  value='' title='" + item.DisplayName + "'/></div>"].join("");
                                break;
                            }
                        case "DateTime":
                            {
                                input = ["<div class='RangeOption clearfix'>",
                                    "<input class='condition' type='hidden' DataType='" + item.DataType + "' Format='" + item.Format + "'  name='" + item.Name + "' OperatorType='1' title='" + item.DisplayName + "'/>",
                                    "<input class='RangeAdd' type='button'  value='' title='" + item.DisplayName + "'/>",
                                    "<input type='button' class='RangeClear'  value='' title='" + item.DisplayName + "'/></div>"].join("");
                                break;
                            }
                        case "Select":
                            {
                                input = "<input class='condition' type='hidden' DataType='" + item.DataType + "'  name='" + item.Name + "' OperatorType='1' title='" + item.DisplayName + "'/>";
                                var option = "<option></option>";
                                option += item.Data || "";
                                input += "<select class='easy' multiple='multiple' >" + option + "</select>";
                                break;
                            }
                        case "None":
                            input = "<span></span>";
                            break;
                        default:
                            input = "<input class='condition' type='text' DataType='" + item.DataType + "'  name='" + item.Name + "' OperatorType='7' title='" + item.DisplayName + "'/>";
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
            search.find("input").keydown(function (e) {
                if (e.keyCode === 13) {
                    handlers.reload();
                }
            });
            search.find(".RangeAdd").click(showRangeCondition);
            search.find(".RangeClear").click(function() {
                $(this).parent().find("input.condition").val("");
            });
            search.find(".ClearSearch").click(handlers.clearCondition);
            search.find("select").change(handlers.reload);
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
            if (deCol == orderCol) {
                orderType = orderType == 1 ? 2 : 1;
            }
            else {
                orderCol = deCol;
                orderType = 1;
            }
            orderArray = [];
            orderArray.push({ OrderCol: orderCol, OrderType: orderType });
            gridHeader.find("th[col]").removeAttr("class");
            if (orderType == 1)
                $(this).addClass("OrderUp");
            else $(this).addClass("OrderDown");
            handlers.reload();
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
    handlers.initGridData = function (gdata) {
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
            if (checkBox && chVale) {
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
        handlers.setPageIndex(gdata.PageIndex);

        allPage = gdata.RecordCount / gdata.PageSize;
        allPage += gdata.RecordCount % gdata.PageSize === 0 ? 0 : 1;
        grid.find("#PageInfo").html("[" + (gdata.PageIndex + 1) + "/" + allPage + "]");
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
    handlers.clearCondition = function () {
        search.find("input.condition").val("");
        search.find(".bg-info").removeClass("bg-info");
        search.find("select.condition option").prop("selected", false);
        search.find("select").change();
        $(".RangeAdd").data("shown", false);
        handlers.reload();
        return false;
    }
    handlers.deleteData = function () {
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
                            handlers.reload();
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

    gridBody.scroll(function () {
        gridHeader.scrollLeft($(this).scrollLeft());
    });
    grid.find("#GridPageIdex").keyup(function (e) {
        if (e.keyCode == 13) {
            grid.find("#PageGo").click();
        }
    });
    grid.find("#GridPageSize").change(function () {
        handlers.loadPage(0);
    });
    grid.find("#PageGo").click(handlers.reload);
    grid.find("#PageFirst").click(function () {
        handlers.loadPage(0);
    });
    grid.find("#PagePre").click(function () {
        var pageIndex = handlers.getPageIndex();
        if (pageIndex > 0) {
            handlers.loadPage(pageIndex - 1);
        }
    });
    grid.find("#PageNext").click(function () {
        var pageIndex = handlers.getPageIndex();
        if (pageIndex < allPage - 1) {
            handlers.loadPage(pageIndex + 1);
        }
    });
    grid.find("#PageLast").click(function () {
        handlers.loadPage(allPage - 1);
    });
    grid.find(".GridDelete").click(handlers.deleteData);


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

        var effect = th.parent().find("input.condition");
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
                handlers.reload();
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
    /*public function*/


    var returnObj = {
        "Show": handlers.reload,
        "Reload": handlers.reload,
        "OnRowDataBind": onRowDataBind,
        "SetColumnTemplete": setColumnTemplete,
        "SetUrl": setUrl,
        "SetGridArea": setGridArea,
        "ShowCheckBox": showCheckBox,
        "OnCheckBoxChange": onCheckBoxChange,
        "Height": height,
        "SetModel": setModel,
        "SearchAble": searchAble,
        "OnSuccess": onSuccess,
        "OrderBy": orderBy,
        "SetBoolBar": setToolbar,
        "SetDeleteUrl": setDeleteUrl,
        "Delete": handlers.deleteData
    }
    function setUrl(value) {
        dataUrl = value;
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
        checkBox = true;
        return returnObj;
    }
    function height(value) {
        grid.find(".GridBody").height(value - grid.find(".GridHeader").height() - grid.find(".Gridfoot").height() - grid.find(".GridToolBar").height() - heightFix);
        constHeight = true;
        return returnObj;
    }
    function setModel(viewModel) {
        handlers.initGrid(viewModel);
        return returnObj;
    }
    function searchAble() {
        canSearch = true;
        return returnObj;
    }
    function orderBy(property, sortType) {
        if (!orderCol) {
            orderCol = property;
            orderType = sortType;
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


