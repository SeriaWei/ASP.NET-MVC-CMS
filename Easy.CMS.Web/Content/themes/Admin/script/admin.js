$(function () {
    $(".accordion-group>a").click(function () {
        if ($(this).nextAll(".accordion-inner").hasClass("active")) {
            return false;
        }
        $(this).parents("ul").find(".accordion-inner.active").removeClass("active").hide(200);
        $(this).nextAll(".accordion-inner").addClass("active").show(200);
        return false;
    });
    $(".navbar-nav a").click(function () {
        if (!$(this).parents("li").hasClass("accordion-group")) {
            $(this).parents("ul").find("a.active").removeClass("active");
        } else {
            $(this).parents("ul").find("li.accordion-group>a.active").removeClass("active");
        }
        $(this).addClass("active");
    });
    var mainContainer = $(".main-container");
    $(window).resize(function () {
        mainContainer.height($(window).height() - 70);
    });
    mainContainer.height($(window).height() - 70);
    $(document).on("click", ".cancel", function () {
        window.history.back();
    }).on("click", ".publish", function () {
        if (confirm("确认要发布吗？")) {
            return true;
        }
        return false;
    }).on("click", "input[type=submit]", function () {
        $("#ActionType").val($(this).data("value"));
        return true;
    }).on("click", ".input-group-collection .add", function () {
        var index = $(this).parents(".input-group-collection").children(".item").size();
        var html = $($(this).parents(".input-group-collection").children(".Template").html().replaceAll("{0}", index));
        html.find("input.actionType").val($(this).data("value"));
        $(this).parents(".input-group-collection").append(html);
    }).on("click", ".input-group-collection .delete", function () {
        $(this).parents(".item").find("input.actionType").val($(this).data("value"));
        $(this).parents(".item").hide();
    }).on("click", ".glyphicon.glyphicon-search.text-muted", function () {
        var obj = $(this);
        window.top.Easy.ShowUrlWindow({
            url: $(this).data("url"),
            onLoad: function (box) {
                var win = this;
                $(this.document).find("#confirm").click(function () {
                    var target = obj.data("target-input") || obj.parent().next().find("input.form-control");
                    target.val(win.GetSelected());
                    box.close();
                });
                $(this.document).on("click", ".confirm", function () {
                    var target = obj.data("target-input") || obj.parent().next().find("input.form-control");
                    target.val($(this).data("result"));
                    box.close();
                });
            }
        });
    }).on("click", ".form-group select#ZoneID", function () {
        var obj = $(this);
        var url = "/admin/Layout/SelectZone?layoutId=" + $("#Hiddens #LayoutID").val() + "&pageId=" + $("#Hiddens #PageID").val() + "&zoneId=" + obj.val();
        window.top.Easy.ShowUrlWindow({
            url: url,
            width: 1000,
            title:"选择区域",
            onLoad: function (box) {
                var win = this;
                $(this.document).find("#confirm").click(function () {
                    obj.val(win.GetSelected());
                    box.close();
                });
            }
        });
    });
    $(".form-group select#ZoneID").on("mousedown",false);
    $("#IsPublish").val("false");
    $("#PublishDate").val("");

    $(".select").each(function () {
        var selectBtn = $(' <span class="glyphicon glyphicon-search text-muted" data-url="' + $(this).data("url") + '"></span>');
        selectBtn.data("target-input", $(this));
        $(this).parent().siblings(".control-label").append(selectBtn);
    });

    var currentSelect = $(".nav.navbar-nav a[href='" + location.pathname + "']");
    if (currentSelect.size()) {
        currentSelect.addClass("active");
        Easy.Cookie.SetCookie("selectAble", location.pathname);
    }
    else {
        $(".nav.navbar-nav a[href='" + Easy.Cookie.GetCookie("selectAble") + "']").addClass("active");
    }
    var activeHref = $(".nav.navbar-nav a.active");
    if (activeHref.parent().hasClass("accordion-inner")) {
        activeHref.parent().show();
        activeHref.parent().prev().addClass("active");
    }
    $(".Date").each(function () {
        $(this).datetimepicker({ locale: "zh_cn", format: $(this).attr("JsDateFormat") });
    });
    $('.nav.nav-tabs a').click(function (e) {
        $(this).tab('show');
        return false;
    }).each(function(i) {
        if (i === 0 || location.hash===$(this).attr("href")) {
            $(this).tab("show");
        }
    });
    tinymce.init({
        content_css: ["../../../Content/bootstrap/css/bootstrap.css", "../../../Content/bootstrap/css/bootstrap-theme.css"],
        selector: "textarea.html",
        plugins: [
            "advlist autolink lists link image charmap print preview anchor",
            "searchreplace visualblocks code fullscreen",
            "insertdatetime media table contextmenu paste"
        ],
        toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
        height: 300,
        relative_urls: false,
        language: "zh_CN"
    });
});