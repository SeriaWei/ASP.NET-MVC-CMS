$(function () {
    var container = $("#containers");
    if (container.children().size() > 0 && container.children(".container").size() == 0 && container.children(".container-fluid").size() == 0) {
        var containerItem = $('<div class="container"></div>');
        container.children().appendTo(containerItem);
        container.append(containerItem);
    }
    var containerTools = '<i class="tools glyphicon glyphicon-resize-horizontal"></i><i class="tools glyphicon glyphicon-resize-small"></i><i class="tools glyphicon glyphicon-sort"></i><i class="tools glyphicon glyphicon-remove-circle"></i>';
    var tools = '<i class="tools glyphicon glyphicon-remove-circle"></i>';
    $(document).on("blur", ".zone input", function () {
        $(this).attr("value", $(this).val());
    });
    function getNewZone() {
        var zoneParent = $('<div class="additional zone"></div>');
        var zone = $("<zone></zone>");
        zone.append('<input class="form-control" type="text" name="ZoneName" placeholder="输入名称" value="内容 ' + ($("#containers input[type=text]").size() + 1) + '" />');
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
    $(".AddContainer").draggable({ helper: "clone" });
    $(".AddRow").draggable({ helper: "clone", revert: "invalid", connectToSortable: ".container,.container-fluid" });
    $(".AddCol").draggable({ helper: "clone", connectToSortable: ".additional.row" });
    $(".templates ul li").draggable({ helper: "clone", connectToSortable: "#containers>div" });

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
                var col = $('<div class="additional ' + ui.item.data("col") + ui.item.data("val") + ' ui-sortable-handle">' + tools + '<div class="colContent row"></div></div>');
                col.find(".colContent").append(getNewZone());
                ui.item.replaceWith(col);
            }
        }
    };
    var rowSortOption = {
        placeholder: "additional row muted",
        tolerance: "pointer",
        connectWith: ".container,.container-fluid",
        items: ".additional.row",
        stop: function (event, ui) {
            var row = $('<div class="additional row">' + tools + '</div>');
            if (ui.item.hasClass("AddRow")) {
                ui.item.replaceWith(row);
            } else if (ui.item.data("add")) {
                var cols = $(ui.item.find(".row").html());
                row.append(cols);
                ui.item.replaceWith(row);
                cols.each(function () {
                    $(this).addClass("additional");
                    $(this).append(tools);
                    $(this).append($('<div class="colContent row"></div>').append(getNewZone()));
                });
            }
            row.sortable(colSortOption);
        }
    };

    $("#containers").sortable({
        placeholder: "design",
        axis: 'y',
        tolerance: "pointer",
        start: function (event, ui) {
            if (ui.helper.hasClass("container")) {
                ui.placeholder.addClass("container");
                ui.helper.css("left", ui.placeholder.offset().left);
            }
            else {
                ui.placeholder.addClass("container-fluid");
            }
        },
        handle: ".glyphicon-sort"
    });

    $("#containers>div").sortable(rowSortOption).append(containerTools).addClass("design main");
    $(".additional.row").sortable(colSortOption).append(tools).children(".additional").append(tools);

    $("body").droppable({
        greedy: true,
        accept: ".AddContainer",
        hoverClass: "dropWarning",
        drop: function (event, ui) {
            var container = $('<div class="container design main">' + containerTools + '</div>');
            container.sortable(rowSortOption);
            $(".additional.row", container).sortable(colSortOption);
            $("#containers").append(container);
        }
    });
    $(document).on("click", ".container.design .glyphicon-resize-horizontal", function () {
        $(this).closest(".container").removeClass("container").addClass("container-fluid");
    });
    $(document).on("click", ".container-fluid.design .glyphicon-resize-small", function () {
        $(this).closest(".container-fluid").removeClass("container-fluid").addClass("container");
    });
    $(document).on("click", "#containers .glyphicon-remove-circle", function () {
        $(this).parent().remove();
    });

    if ($(window).width() > 1600) {
        $(".templates").addClass("active");
    }
    $(document).on("click", ".templates .tool-open", function () {
        $(this).parent().toggleClass("active");
    });
    container.removeClass("hide");

    $(document).on("click", "#save", function () {
        $('input[name="ZoneName"]').each(function () {
            if (!$.trim($(this).val())) {
                $(this).attr("value", "未命名");
            }
        });
        if ($(this).data("done")) {
            return;
        }
        $(this).data("done", true);

        var copyContainer = $('<div id="containers"/>').append(container.html());

        $("div", copyContainer)
                    .removeClass("ui-droppable")
                    .removeClass("ui-sortable")
                    .removeClass("ui-sortable-handle")
                    .removeClass("active")
                    .removeClass("design")
                    .removeAttr("style");

        $(".tools", copyContainer).remove();

        var html = copyContainer.html();
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
            form.append($('<textarea name="html" class="layout-html hide"></textarea>').val($.trim(allZoneResult[i])));
        }
        form.submit();
        return false;
    });
});