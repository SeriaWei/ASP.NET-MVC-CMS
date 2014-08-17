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
    $("#IsPublish").val("false");
    $("#PublishDate").val("");
    $(".select").each(function () {
        $(this).prev(".input-group-addon").append('<span class="glyphicon glyphicon-search" data-url="' + $(this).data("url") + '"></span>');
    });

    $(".input-group-addon .glyphicon").live("click", function () {
        var obj = $(this);
        Easy.ShowUrlWindow({
            url: $(this).data("url"), onLoad: function (box) {
                var win = this;
                $(this.document).find("#confirm").click(function () {
                    obj.parent().next().val(win.GetSelected());
                    box.close();
                });
            }
        });
    });
    var currentSelect = $("a.selectAble[href='" + location.pathname + "']");
    if (currentSelect.size()) {
        currentSelect.addClass("active");
        Easy.Cookie.SetCookie("selectAble", location.pathname);
    }
    else {
        $("a.selectAble[href='" + Easy.Cookie.GetCookie("selectAble") + "']").addClass("active");
    }
    var activeHref = $("a.selectAble.active");
    if (activeHref.parent().hasClass("accordion-inner")) {
        activeHref.parent().show();
        activeHref.parent().prev().addClass("active");
    }
});