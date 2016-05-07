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
        $("#add-col-handle").attr("data-val", $(this).data("val")).find(".col-size-info").text($(this).text());
        $(this).parent().parent().find(".active").removeClass("active");
        $(this).parent().addClass("active");
    });

    $(".AddRow").draggable({ helper: "clone", revert: "invalid", connectToSortable: "#container" });
    $(".AddCol").draggable({ helper: "clone", connectToSortable: ".additional.row" });
    $(".templates ul li").draggable({ helper: "clone", connectToSortable: "#container" });

    $("#toolBar").droppable({
        activeClass: "dropWarning",
        hoverClass: "dropDanger",
        accept: ".additional",
        drop: function (event, ui) {
            ui.draggable.remove();
        }
    });
    var colSortOption = {
        placeholder: "additional",
        tolerance: "pointer",
        connectWith: ".additional.row",
        over: function (event, ui) {
            if (ui.item.hasClass("AddCol")) {
                ui.placeholder.addClass(ui.item.data("col") + ui.item.data("val")).html('<div class="colContent row muted"></div>');
            } else {
                ui.placeholder.addClass(ui.item.attr("class")).html('<div class="colContent row muted"></div>');
            }
        },
        stop: function (event, ui) {
            if (ui.item.hasClass("AddCol")) {
                var col = $('<div class="additional ' + ui.item.data("col") + ui.item.data("val") + ' ui-sortable-handle"><div class="colContent row"></div></div>');
                col.find(".colContent").append(getNewZone());
                ui.item.replaceWith(col);
            }
        }
    };
    $("#container").sortable({
        placeholder: "additional row muted",
        tolerance: "pointer",
        stop: function (event, ui) {
            var row = $('<div class="additional row"></div>');
            if (ui.item.hasClass("AddRow")) {
                ui.item.replaceWith(row);
            } else if (ui.item.data("add")) {
                var cols = $(ui.item.find(".row").html());
                row.append(cols);
                ui.item.replaceWith(row);
                cols.each(function () {
                    $(this).addClass("additional");
                    $(this).html($("<div class=\"colContent row\"></div>").append(getNewZone()));
                });
            }
            row.sortable(colSortOption);
        }
    });
    $(".additional.row").sortable(colSortOption);

    $(document).on("click", "#save", function () {

        if ($(this).data("done")) {
            return;
        }
        $(this).data("done", true);
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
        return false;
    });
    if ($(window).width() > 1600) {
        $(".templates").addClass("active");
    }
    $(document).on("click", ".templates .tool-open", function () {
        $(this).parent().toggleClass("active");
    });
});