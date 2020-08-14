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
    $('.log_in').click(function () {
        $('.login_form').css({
            "opacity": "1",
            "visibility": "visible"
        })
        $('.fog_background').css({
            "opacity": "1",
            "visibility": "visible"
        })
        $('body').css("overflow-y", "hidden");
    })
    $('.fog_background').click(function () {
        $('.login_form').css({
            "opacity": "0",
            "visibility": "hidden"
        })
        $('.fog_background').css({
            "opacity": "0",
            "visibility": "hidden"
        })
        $('body').css("overflow-y", "visible");
    })

    //content
})