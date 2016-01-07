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
    $(window).resize(function () {
        $(".main-container").height($(window).height() - 70);
    });
    $(".main-container").height($(window).height() - 70);
    $(".cancel").click(function () {
        window.history.back();
    });
    $(".publish").click(function () {
        if (confirm("确认要发布吗？")) {
            return true;
        }
        return false;
    });
    $("input[type=submit]").click(function () {
        $("#ActionType").val($(this).data("value"));
        return true;
    });
    $("#IsPublish").val("false");
    $("#PublishDate").val("");
    $(".select").each(function () {
        $(this).prev(".input-group-addon").append('<span class="glyphicon glyphicon-search text-muted" data-url="' + $(this).data("url") + '"></span>');
    });

    $(document).on("click",".input-group-addon .glyphicon", function () {
        var obj = $(this);
        window.top.Easy.ShowUrlWindow({
            url: $(this).data("url"), onLoad: function (box) {
                var win = this;
                $(this.document).find("#confirm").click(function () {
                    obj.parent().next().val(win.GetSelected());
                    box.close();
                });
                $(this.document).on("click", ".confirm", function() {
                    obj.parent().next().val($(this).data("result"));
                    box.close();
                });
            }
        });
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

    $(document).on("click", ".input-group-collection .add", function () {
        var index = $(this).parents(".input-group-collection").children(".item").size();
        var html = $($(this).parents(".input-group-collection").children(".Template").html().replaceAll("{0}", index));
        html.find("input.actionType").val($(this).data("value"));
        $(this).parents(".input-group-collection").append(html);
    });
    $(document).on("click", ".input-group-collection .delete", function () {
        $(this).parents(".item").find("input.actionType").val($(this).data("value"));
        $(this).parents(".item").hide();
    });
});