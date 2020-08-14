$(document).ready(function () {
    $(".colClick").click(function () {
        var index = $(".colClick").index(this);
        var listForm = $(".career-search-filter-toggle ul");
        $(listForm[index]).slideToggle();

    });
    $(".star").click(function () {
        $(".star").css({ "color": "#666666" });
    })
    $("#clearAll").click(function () {
        location.reload();
    })
    $("#findIcon").click(function () {
        var enterprise = $("input[name='enterprise']").val();
        var a = $('#selectedCareer option[value="' + $('#careerBrowser').val() + '"]:checked').data('id');
        var career = (a != null) ? a : 0 ;
        var listArea = $('#selectedState option[value="' + $('#areaState').val() + '"]').data('id');
        var size = ($("input[name='selectSize']:checked").val() != null) ? $("input[name='selectSize']:checked").val() : 0;
        alert(enterprise);
        alert(career);
        alert(listArea);
        alert(size);
        var dbParam = "EName=" + enterprise + "&EArea=" + listArea + "&ECareer=" + career
            + "&ESize=" + size;
        window.location.href = "/SearchCompanyForUser?" + dbParam;
    })

    function checkName() {
        var name = $("input[name='enterprise']");
        var name1 = "";
        var i = 1;
        name1 = name.charAt(0).toUpperCase();
        while (i < name.length) {
            if (name.charAt(i) == " " && name.charAt(i + 1) == " ") {
                i++;
                if (i == name.length) break;
            }
            else if (name.charAt(i - 1) == " ") {
                name1 = name1 + name.charAt(i).toUpperCase();
                i++;
            }
            else {
                name1 = name1 + name.charAt(i);
                i++;
            }
        }
        $("input[name='enterprise']").val(name1);
    }
})