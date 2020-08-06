$(document).ready(function () {
    var $table = $('#tableOfContents');
    $(window).scroll(function () {
        if ($(window).scrollTop() > 490) {
            $table.css({ "position": "fixed", "top": "30px", "left": "0px", "width": "230px", "margin-left": "2.5%", "z-index": "3", "margin-top": "75px" });
        }
        else if ($(window).scrollTop() < 490) {
            $table.css({ "position": "relative", "top": "0px", "width": "90%", "z-index": "3","margin-top": "30px" });
        }
    });

});