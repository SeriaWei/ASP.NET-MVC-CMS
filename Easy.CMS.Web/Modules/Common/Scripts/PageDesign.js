$(function () {
    function checkEmptyZone() {
        $(".zone").each(function () {
            if ($(this).children(":not(.zoneName)").size() === 0) {
                $(this).addClass("empty-zone");
            } else {
                $(this).removeClass("empty-zone");
            }
        });
    }

    $(".zone").sortable({
        handle: ".sort-handle",
        stop: function (event, ui) {
            var tempForm = "";
            ui.item.parent().children(".widget-design").each(function (i, ui) {
                tempForm += "&widgets[" + i + "].ID=" + $(ui).data("widgetid") + "&widgets[" + i + "].Position=" + (i + 1);
            });
            if (tempForm) {
                $.post($("#save-widget-position-url").val(), tempForm, function (data) {
                    checkEmptyZone();
                }, "html");
            }
            return true;
        }
    }).droppable({
        hoverClass: "dropWarning",
        accept: ".widget-design",
        greedy: true,
        tolerance: "pointer",
        drop: function (event, ui) {
            if (ui.draggable.data("add")) {
                var area = $(this);
                $.post($("#append-widget-url").val(), {
                    ID: ui.draggable.data("id"),
                    ZoneID: $("input.zoneId", this).val(),
                    PageID: $("#pageId").val(),
                    AssemblyName: ui.draggable.data("assemblyname"),
                    ServiceTypeName: ui.draggable.data("servicetypename"),
                    Position: $(".widget-design", this).size() + 1
                }, function (data) {
                    area.append(data);
                }, "html");
            } else {
                if ($("input.zoneId", ui.draggable.parent()).val() === $("input.zoneId", this).val()) {
                    return true;
                }
                var target = ui.draggable.clone();
                target.removeAttr("style");
                $(this).append(target);
                ui.draggable.remove();
                $.post($("#save-widget-zone-url").val(), {
                    ID: target.data("widgetid"),
                    ZoneId: $("input.zoneId", this).val(),
                    Position: $(this).children().size()
                }, function (data) {
                    checkEmptyZone();
                }, "html");
            }
            return true;
        }
    });
    $(".templates ul li").draggable({ helper: "clone" });
    $(document).on("click", ".delete", function () {
        if (confirm("确定要删除该组件吗？")) {
            $.post($(this).data("url"), { ID: $(this).data("id") }, function (data) {
                if (data) {
                    $("#widget_" + data).remove();
                    checkEmptyZone();
                }
            }, "json");
        }
    });
    $(".helper").click(function () {
        $("#container").toggleClass($(this).data("class"));
    });
    checkEmptyZone();
});