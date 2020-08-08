$(document).ready(function () {
    $(".colClick").click(function () {
        var index = $(".colClick").index(this);
        var listForm = $(".career-search-filter-toggle ul");
        $(listForm[index]).slideToggle();

    });
    $(".star").click(function () {
        $(".star").css({ "color": "#666666" });
    })
})