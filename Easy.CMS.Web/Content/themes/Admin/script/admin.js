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
        $(this).parents("ul").find("a.active").removeClass("active");
        $(this).addClass("active");
    });
    $(window).resize(function () {
        $(".main-container").height($(window).height() - 70);
    });
    $(".main-container").height($(window).height() - 70);
    
});