$(function () {
    $(".navigation>ul>li").mouseenter(function () {
        $(this).children("ul").show(200);
    }).mouseleave(function () {
        $(this).children("ul").hide(200);
    });
    $(".navigation>ul>li>a").click(function () {
        Easy.Cookie.SetCookie("nav_actived", $(this).attr("id"), 1);
    });
});