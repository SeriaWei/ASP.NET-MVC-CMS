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
    "</label><input id='GridPageIdex' type='text' value='1' ValueType='Num' />",
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
                                var option = "<option></option>";
                                option += item.Data || "";
                                input = "<select class='easy condition' multiple='multiple' name='" + item.Name + "' DataType='" + item.DataType + "'  name='" + item.Name + "' OperatorType='1' title='" + item.DisplayName + "'>" + option + "</select>";
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
            trH += " </tr>";
            search.html(trH);
            gridHeader.find("table").append(search);
            handlers.multiSelect();
        } else {
            search.hide();
        }

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
        if (gdata.Rows.length > 0) {
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
                        trB += "<td " + (j === 0 ? "style='width:" + dataWidth + "px'" : "") + "><div class='coData' style='width:" + dataWidth + "px'>" + handlers.getTempleteValue(p, gdata.Rows[j]) + "</div></td>";
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
        }
        else {
            var emptyTr = "<tr>";
            var noData = Easy.GridLan.NoData;
            for (var item in model) {
                if (model.hasOwnProperty(item)) {
                    if (model[item].Hidden) {
                        continue;
                    }
                    var colWidth = model[item].Width || 150;
                    width += colWidth;
                    emptyTr += "<td style='width:" + colWidth + "px;border-bottom:none;'><div class='coData'>" + noData + "</div></td>";
                    noData = "";
                }
            }
            emptyTr += "</tr>";
            trBs += emptyTr;
        }
        tableBody.html("<tbody>" + trBs + "</tbody>");
        gridBody.html(tableBody);
        tableBody.width(width);

        handlers.setPageIndex(gdata.PageIndex);

        allPage = Math.floor(gdata.RecordCount / gdata.PageSize);
        allPage += gdata.RecordCount % gdata.PageSize === 0 ? 0 : 1;
        grid.find("#PageInfo").html("[" + (gdata.PageIndex + 1) + "/" + allPage + "]");
        grid.find("#Count").html(Easy.GridLan.AllRecord.replace("{0}", gdata.RecordCount));
        gridBody.scrollLeft(scrollLeft);

        if (dataSuccessEventHandler != null) {
            dataSuccessEventHandler.call();
        }
    }
    handlers.reload = function () {
        if (isLoading) {
            return;
        }
        isLoading = true;
        grid.find("#PageGo").addClass("InnerLonding");
        scrollLeft = gridBody.scrollLeft();
        var groups = [];
        var conditions = [];
        search.find(".condition").each(function () {
            if ($(this).val()) {
                switch ($(this).attr("DataType")) {
                    case "Select":
                        {
                            var selectConditions = [];
                            var select = $(this);
                            $("option:selected", this).each(function (i) {
                                selectConditions.push({ Property: select.attr("name"), Value: $(this).val(), DataType: select.attr("DataType"), OperatorType: 1, ConditionType: 2 });
                            });
                            groups.push({ Conditions: selectConditions });
                            break;
                        }
                    case "Decimal":
                    case "Single":
                    case "Int16":
                    case "Int64":
                    case "Int32": {
                        var numConditions = [];
                        var numValues = $(this).val().split('@');
                        if (numValues.length === 3) {
                            if (numValues[0]) {
                                numConditions.push({ Property: $(this).attr("name"), Value: numValues[0], DataType: $(this).attr("DataType"), OperatorType: 3, ConditionType: 1 });

                            }
                            if (numValues[1]) {
                                numConditions.push({ Property: $(this).attr("name"), Value: numValues[1], DataType: $(this).attr("DataType"), OperatorType: 5, ConditionType: 1 });

                            }
                            if (numValues[2]) {
                                numConditions.push({ Property: $(this).attr("name"), Value: numValues[2], DataType: $(this).attr("DataType"), OperatorType: 1, ConditionType: 1 });

                            }
                            groups.push({ Conditions: numConditions });
                        }
                        break;
                    }
                    case "DateTime": {
                        var dateConditions = [];
                        var values = $(this).val().split('@');
                        if (values.length === 3) {
                            if (values[0]) {
                                if (values[0].indexOf(":") < 0) {
                                    values[0] += " 0:0:0";
                                }
                                dateConditions.push({ Property: $(this).attr("name"), Value: values[0], DataType: $(this).attr("DataType"), OperatorType: 3, ConditionType: 1 });

                            }
                            if (values[1]) {
                                if (values[1].indexOf(":") < 0) {
                                    values[1] += " 23:59:59";
                                }
                                dateConditions.push({ Property: $(this).attr("name"), Value: values[1], DataType: $(this).attr("DataType"), OperatorType: 5, ConditionType: 1 });

                            }
                            if (values[2]) {
                                var hasTime = values[2].indexOf(":") >= 0;
                                dateConditions.push({ Property: $(this).attr("name"), Value: values[2] + hasTime ? "" : " 0:00:00", DataType: $(this).attr("DataType"), OperatorType: 3, ConditionType: 1 });

                                dateConditions.push({ Property: $(this).attr("name"), Value: values[2] + hasTime ? "" : " 23:59:59", DataType: $(this).attr("DataType"), OperatorType: 5, ConditionType: 1 });

                            }
                            groups.push({ Conditions: dateConditions });

                        }
                        break;
                    }
                    default:
                        {
                            conditions.push({ Property: $(this).attr("name"), Value: $(this).val(), DataType: $(this).attr("DataType"), OperatorType: $(this).attr("OperatorType"), ConditionType: 1 });
                            break;
                        }
                }
            }
        });
        $.ajax({
            url: dataUrl + (queryString === "" ? "" : "?" + queryString),
            data: { ConditionGroups: groups, Conditions: conditions, PageIndex: handlers.getPageIndex(), PageSize: handlers.getPageSize(), OrderBy: orderArray },
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
                return true;
            },
            error: function (msg) {
                isLoading = false;
                grid.find("#PageGo").removeClass("InnerLonding");
                gridBody.html("<h1>Grid.Ajax.Error:" + msg.status + "</h1>");
            }
        }, "json");
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
    handlers.showRangeCondition = function () {
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
        th.parent().append(rangeBox);
        var normalLeft = th.position().left;
        if (normalLeft + rangeBox.outerWidth() > grid.outerWidth()) {
            normalLeft = grid.outerWidth() - rangeBox.outerWidth();
        }
        rangeBox.css("left", normalLeft);
        rangeBox.css("top", th.position().top + th.outerHeight());
        switch (effect.attr("DataType")) {
            case "DateTime": {
                var dateFormat = effect.attr("Format");
                if (!dateFormat) {
                    dateFormat = "YYYY/MM/DD";
                }
                rangeBox.find(".form-control").attr("DateFormat", dateFormat).attr("ValueType", "Date").addClass("Date");
                break;
            }
            case "Decimal":
            case "Single":
            case "Int16":
            case "Int64":
            case "Int32": {
                rangeBox.find("input").attr("ValueType", "Num").addClass("Number");
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
            }, 100);
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
    handlers.getTempleteValue = function (columnName, data) {
        var ind = templetes.ValueIndex(columnName);
        if (ind !== -1) {
            var reStr = templeteValues[ind];
            for (var item in data) {
                if (data.hasOwnProperty(item)) {
                    while (reStr.indexOf("{" + item + "}") >= 0) {
                        reStr = reStr.replace("{" + item + "}", data[item]);
                    }
                }
            }
            return reStr;
        }
        else {
            return data[columnName];
        }
    }
    handlers.multiSelect = function () {
        $("select.easy[multiple]", gridHeader).each(function () {
            if ($(this).attr("easy")) {
                return;
            }
            $(this).attr("easy", "easy");
            var oldSelect = $(this);
            var selectList = $(["<div class='DropDownList'>",
            "<div class='TextPlace' title=" + oldSelect.attr("title") + "><span></span></div><div class='DropIcon'>&nbsp;</div>",
            "<div style='clear:both'></div>",
            "</div>"].join(""));
            selectList.insertAfter(oldSelect);
            selectList.css("width", $(this).outerWidth());
            selectList.on("click", ".Clear", function () {
                $("option", oldSelect).prop("selected", false);
                oldSelect.change();
                selectList.removeClass("Open");
                options.hide();
                return false;
            });
            oldSelect.change(function () {
                var text = "";
                $("option:checked", oldSelect).each(function () {
                    if (!text)
                        text += $(this).text();
                    else text += "," + $(this).text();
                });
                if (text) {
                    var textplace = selectList.find(".TextPlace");
                    textplace.attr("title", text);
                    textplace.html("<span>" + text + "</span><div class='Clear'></div>");
                }
                else selectList.find(".TextPlace").html("<span>&nbsp;</span>");
                selectList.find(".TextPlace").css("position", "relative");
            });
            var options = $("<ul class='DropDownList_Options dropdown-menu' tabindex='-1'></ul>");
            var clickIn;
            options.on("blur", function () {
                if (!clickIn) {
                    selectList.removeClass("Open");
                    options.hide();
                }
                clickIn = false;
            });
            options.on("mousedown", function () {
                clickIn = true;
            });
            options.on("click", ".checkbox input[type=checkbox]", function () {
                var val = $(this).val();
                var checked = $(this).prop("checked");
                $("option", oldSelect).each(function () {
                    if ($(this).val() === val) {
                        $(this).prop("selected", checked);
                    }
                });
                oldSelect.trigger("change");
                options.focus();
            });
            oldSelect.hide();
            options.hide();
            options.insertAfter(selectList);
            selectList.click(function () {
                if (selectList.hasClass("Open")) {
                    selectList.removeClass("Open");
                    options.hide();
                    return true;
                }
                selectList.addClass("Open");
                var lists = [];
                $("option", oldSelect).each(function () {
                    var val = $(this).attr("value");
                    if (val) {
                        var txt = $(this).text();
                        var selected = $(this).prop("selected");
                        if (selected) {
                            lists.push("<li val='" + val + "'><a><label class='checkbox'><input type='checkbox' checked='checked' value='" + val + "' />" + txt + "</label></a></li>");
                        } else {
                            lists.push("<li val='" + val + "'><a><label class='checkbox'><input type='checkbox' value='" + val + "' />" + txt + "</label></a></li>");
                        }
                    }
                });
                options.empty();
                options.append(lists.join(""));
                options.show();
                options.focus();
                options.css("left", $(this).position().left);
                options.css("top", $(this).position().top + $(this).outerHeight());
                return true;
            });
        });
    }

    handlers.checkBack = function () {
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
    gridBody.scroll(function () {
        gridHeader.scrollLeft($(this).scrollLeft());
    });

    search.on("keydown", function (e) {
        if (e.keyCode === 13) {
            handlers.reload();
        }
    });
    search.on("click", ".RangeAdd", handlers.showRangeCondition);
    search.on("click", ".RangeClear", function () {
        $(this).parent().find("input.condition").val("");
        $(this).parent().removeClass("bg-info");
        handlers.reload();
    });
    search.on("click", ".ClearSearch", handlers.clearCondition);
    search.on("change", "select", handlers.reload);

    grid.on("keyup", "#GridPageIdex", function (e) {
        if (e.keyCode === 13) {
            grid.find("#PageGo").click();
        }
    });
    grid.on("change", "#GridPageSize", function () {
        handlers.loadPage(0);
    });
    grid.on("click", "#PageGo", handlers.reload);
    grid.on("click", "#PageFirst", function () {
        handlers.loadPage(0);
    });
    grid.on("click", "#PagePre", function () {
        var pageIndex = handlers.getPageIndex();
        if (pageIndex > 0) {
            handlers.loadPage(pageIndex - 1);
        }
    });
    grid.on("click", "#PageNext", function () {
        var pageIndex = handlers.getPageIndex();
        if (pageIndex < allPage - 1) {
            handlers.loadPage(pageIndex + 1);
        }
    });
    grid.on("click", "#PageLast", function () {
        handlers.loadPage(allPage - 1);
    });
    grid.on("click", ".GridDelete", handlers.deleteData);
    gridBody.on("click", "CheckBoxItem", handlers.checkBack);
    gridHeader.on("click", ".CheckBoxAll", function () {
        gridBody.find("tr").find(".CheckBoxItem").prop("checked", $(this).prop("checked"));
        handlers.checkBack();
    });
    gridHeader.on("click", "th[col]", function () {
        var deCol = $(this).attr("col");
        if (deCol === orderCol) {
            orderType = orderType === 1 ? 2 : 1;
        }
        else {
            orderCol = deCol;
            orderType = 1;
        }
        orderArray = [];
        orderArray.push({ OrderCol: orderCol, OrderType: orderType });
        gridHeader.find("th[col]").removeAttr("class");
        if (orderType === 1)
            $(this).addClass("OrderUp");
        else $(this).addClass("OrderDown");
        handlers.reload();
        return false;
    });

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


