$(function () {
    function reCheckToggleContainer() {
        var container = $("#container");
        var isfluid = false;
        if (container.children(".row.active").size() > 0) {
            isfluid = !container.children(".row.active").hasClass("container");
        } else {
            isfluid = !container.hasClass("container");
        }
        if (isfluid) {
            $("input[type=radio][name=toggle-container][value=container-fluid]").prop("checked", true);
        } else {
            $("input[type=radio][name=toggle-container][value=container]").prop("checked", true);
        }
    }

    reCheckToggleContainer();
    $(document).on("click", "input[type=radio][name=toggle-container]", function () {
        var container = $("#container");
        if (container.children(".row.active").size() > 0) {
            if ($(this).val() === "container-fluid") {
                container.children(".row.active").removeClass("container");
            } else {
                container.children(".row.active").addClass($(this).val());
            }
        } else {
            container.children(".row").removeClass("container");
            container.removeClass("container").removeClass("container-fluid").addClass($(this).val());
            $("#ContainerClass").val($(this).val());
        }
    });

    $(document).on("click", "#container>.row", function () {
        var container = $("#container");
        container.children(".row.active").not(this).removeClass("active");
        if (container.hasClass("container-fluid")) {
            $(this).toggleClass("active");
        }
        reCheckToggleContainer();
    });
    $(document).on("blur", ".zone input", function () {
        $(this).attr("value", $(this).val());
    });
    function getNewZone() {
        var zoneParent = $('<div class="additional zone"></div>');
        var zone = $("<zone></zone>");
        zone.append('<input class="form-control" type="text" name="ZoneName" placeholder="输入名称" value="内容 ' + ($("#container input[type=text]").size() + 1) + '" />');
        zone.append('<input class="form-control" type="hidden" name="LayoutId" value="' + $("#LayoutId").val() + '" />');
        zone.append('<input class="form-control" type="hidden" name="ID" value="" />');
        zoneParent.append(zone);
        return zoneParent;
    }
    $(document).on("click", ".dropdown-menu.col-size a", function () {
        $("#add-col-handle").data("val", $(this).data("val")).find(".col-size-info").text($(this).text());
        $(this).parent().parent().find(".active").removeClass("active");
        $(this).parent().addClass("active");
    });

    $(".AddRow,.AddCol,.AddZone").draggable({ helper: "clone", revert: "invalid" });
    $("#toolBar").droppable({
        activeClass: "dropWarning",
        hoverClass: "dropDanger",
        accept: ".additional",
        drop: function (event, ui) {
            ui.draggable.remove();
        }
    });
    $(".RowDroppable").droppable({
        hoverClass: "dropWarning",
        accept: ".AddRow,.widget-design",
        greedy: true,
        tolerance: "pointer",
        drop: rowDropToContent
    });

    var opRowDrop = {
        hoverClass: "dropWarning",
        accept: ".AddCol",
        greedy: true,
        tolerance: "pointer",
        drop: colDroped
    };
    var opColDrop = {
        hoverClass: "dropWarning",
        accept: ".AddRow,.AddZone,.additional.zone",
        greedy: true,
        tolerance: "pointer",
        drop: function (evn, uirz) {
            if (uirz.draggable.hasClass("AddRow")) {
                rowDroped(evn, uirz, this);
            }
            else if (uirz.draggable.hasClass("AddZone") || uirz.draggable.hasClass("additional")) {
                zoneDropable(evn, uirz, this);
            }
        }
    };
    $("#container .additional.row").droppable(opRowDrop).sortable();;
    $("#container .colContent").droppable(opColDrop).sortable();
    $("#container").sortable({ items: ".additional.row:not(.layout.templates)" });
    function rowDropToContent(event, ui, obj) {
        if (obj == null)
            obj = this;
        var row = $("<div class=\"additional row\"></div>");
        $(obj).append(row);
        if (ui.draggable.hasClass("widget-design")) {
            var cols = $(ui.draggable.find(".row").html());
            row.append(cols);
            cols.each(function () {
                $(this).addClass("additional");
                $(this).html($("<div class=\"colContent row\"></div>").append(getNewZone()));
                $(this).children(".colContent").droppable(opColDrop).sortable();
            });
        }
        row.droppable(opRowDrop);
        row.sortable();
        $(".dropWarning").removeClass("dropWarning");
    }
    function rowDroped(event, ui, obj) {
        if (obj == null)
            obj = this;
        var row = $("<div class=\"additional " + ui.draggable.data("container") + "\"><div class=\"row additional\"></div></div>");
        if (ui.draggable.find("input").val()) {
            row.find(".row").css("width", ui.draggable.find("input").val()).css("margin", "0 auto");
        }
        row.children(".row").droppable(opRowDrop).sortable();
        $(obj).append(row);
        $(".dropWarning").removeClass("dropWarning");
    }
    function colDroped(event, ui, obj) {
        if (obj == null)
            obj = this;
        if (ui.draggable.hasClass("AddZone") || ui.draggable.hasClass("additional")) {
            zoneDropable(event, ui, obj);
            return;
        }
        var col = $("<div class=\"additional " + ui.draggable.data("col") + ui.draggable.data("val") + "\"><div class=\"colContent row\"></div></div>");
        col.children(".colContent").droppable(opColDrop).sortable();
        $(obj).append(col);
        $(".dropWarning").removeClass("dropWarning");
    }
    function zoneDropable(event, ui, obj) {
        if (obj == null) {
            obj = this;
        }
        if (ui.draggable.parent().is(obj))
            return;
        if (ui.draggable.hasClass("additional")) {
            $(obj).append('<div class=\"additional zone\">' + ui.draggable.html() + '</div>');
            ui.draggable.remove();
        }
        else {
            $(obj).append(getNewZone());
        }
        $(".dropWarning").removeClass("dropWarning");
    }
    $(document).on("click", "#save", function () {
        $("#container div")
            .removeClass("ui-droppable")
            .removeClass("ui-sortable")
            .removeClass("ui-sortable-handle")
            .removeClass("active")
            .removeAttr("style");

        var html = $.trim($("#container").html());
        var htmlArray = html.split("<zone>");
        var form = $("#LayoutInfo");
        var zoneArray = [];
        for (var i = 0; i < htmlArray.length; i++) {
            zoneArray.push(htmlArray[i]);
            if (i < htmlArray.length - 1) {
                zoneArray.push("<zone>");
            }
        }
        var allZoneResult = [];
        for (var i = 0; i < zoneArray.length; i++) {
            var zoneEnd = zoneArray[i].split("</zone>");
            for (var j = 0; j < zoneEnd.length; j++) {
                allZoneResult.push(zoneEnd[j]);
                if (j < zoneEnd.length - 1) {
                    allZoneResult.push("</zone>");
                }
            }
        }
        $(".layout-html", form).remove();
        for (var i = 0; i < allZoneResult.length; i++) {
            form.append($('<input type="hidden" class="layout-html" name="html"/>').val($.trim(allZoneResult[i])));
        }
        form.submit();
    });
    if ($(window).width() > 1600) {
        $(".templates").addClass("active");
    }
    $(document).on("click", ".templates .tool-open", function() {
        $(this).parent().toggleClass("active");
    });
    $(".templates ul li").draggable({ helper: "clone" });
});