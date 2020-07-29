$(document).ready(function () {
    $(".colClick").click(function () {
        var index = $(".colClick").index(this);
        var listForm = $(".borderColJobMenu form");
        $(listForm[index]).slideToggle();
    })
})
