$(document).ready(function () {
    var opening = 0;
    $('.responsive_menu_icon').click(function () {
        if (opening == 0)
            opening++;
        else
            opening--;
        $('.responsive_menu').toggleClass("open");
    });
    $(window).resize(function () {
        if ($(window).width() > 1050) {
            $('.responsive_menu').removeClass("open");
        }
        else {
            if (opening == 1)
                $('.responsive_menu').addClass("open");
        }
    });
    $('.searching, .res_searching').click(function () {
        $(location).attr("href", "./index-2.html");
    });
})