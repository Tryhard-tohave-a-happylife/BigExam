$(document).ready(function () {
    $('.titleNews').on({
        mouseenter: function () {
            $(this).children("div.dropdown").show();
        },
        mouseleave: function () {
            $(this).children("div.dropdown").hide();
        }
    })
})