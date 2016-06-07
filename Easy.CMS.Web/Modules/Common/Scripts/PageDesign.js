$(function () {
    $(".zone").sortable({
        placeholder: "sorting",
        handle: ".sort-handle",
        tolerance: "pointer",
        connectWith: ".zone",
        stop: function (event, ui) {
            
            var target = ui.item.parent();
            if (ui.item.data("add")) {
                $.ajax({
                    type: "POST",
                    url: $("#append-widget-url").val(),
                    dataType: 'html',
                    async: false,
                    data: {
                        ID: ui.item.data("id"),
                        ZoneID: $("input.zoneId", this).val(),
                        PageID: $("#pageId").val(),
                        AssemblyName: ui.item.data("assemblyname"),
                        ServiceTypeName: ui.item.data("servicetypename"),
                        Position: 1
                    },
                    success: function (data) {
                        ui.item.replaceWith(data);
                    }
                });
            }
            var widgets = [];
            target.find(".widget-design").each(function (i, ui) {
                widgets.push({
                    ID: $(ui).data("widgetid"),
                    ZoneId: $(".zoneId", target).val(),
                    Position: i + 1
                });
            });
            $.ajax({
                type: "POST",
                url: $("#save-widget-zone-url").val(),
                dataType: 'json',
                contentType: "application/json;charset=utf-8",
                async: false,
                data: JSON.stringify(widgets),
                success: function () {
                }
            });
            return true;
        }
    });

    $(".templates ul li").draggable({ helper: "clone", connectToSortable: ".zone" });
    $(document).on("click", ".delete", function () {
        var th = $(this);
        Easy.ShowMessageBox("提示", "确定要删除该组件吗？", function () {
            $.post(th.data("url"), { ID: th.data("id") }, function (data) {
                if (data) {
                    $("#widget_" + data).parent().remove();
                }
            }, "json");
        }, true, 10);
    });
    $(document).on("click", ".templates .tool-open", function () {
        $(this).parent().toggleClass("active");
    }).on("click", ".templates .delete-template", function () {
        var th = $(this);
        Easy.ShowMessageBox("提示", "确定要删除该模板吗？", function () {
            $.post(th.data("url"), { Id: th.data("id") }, function (data) {
                if (data) {
                    $("#template_" + data).remove();
                }
            }, "json");
        }, true, 10);
    });
    $(".helper").click(function () {
        $("#containers").toggleClass($(this).data("class"));
    });
    if ($(window).width() > 1600) {
        $(".templates").addClass("active");
    }
});