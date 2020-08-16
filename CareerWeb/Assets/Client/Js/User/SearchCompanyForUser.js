$(document).ready(function () {
    $(".colClick").click(function () {
        var index = $(".colClick").index(this);
        var listForm = $(".career-search-filter-toggle ul");
        $(listForm[index]).slideToggle();

    });
    $("#clearAll").click(function () {
        location.reload();
    })
    $("#findIcon").click(function () {
        checkName();
        var enterprise = $("input[name='enterprise']").val();
        var a = $('#selectedCareer option[value="' + $('#careerBrowser').val() + '"]:checked').data('id');
        var career = (a != null) ? a : 0 ;
        var listArea = $('#selectedState option[value="' + $('#areaState').val() + '"]').data('id');
        var size = ($("input[name='selectSize']:checked").val() != null) ? $("input[name='selectSize']:checked").val() : 0;
        var dbParam = "EName=" + enterprise + "&EArea=" + listArea + "&ECareer=" + career
            + "&ESize=" + size;
        window.location.href = "/SearchCompanyForUser?" + dbParam;
    })

    function checkName() {
        var name = $("input[name='jobBrowser']").val();
        var name1 = "";
        var i = 1;
        if (name.length != 0) {
            name1 += name[0].toUpperCase();
            while (i < name.length) {
                if (name[i] == " " && name[i + 1] == " ") {
                    i++;
                    if (i == name.length) break;
                }
                else if (name[i - 1] == " ") {
                    name1 = name1 + name[i].toUpperCase();
                    i++;
                }
                else {
                    name1 = name1 + name[i];
                    i++;
                }
            }
        }

        $("input[name='jobBrowser']").val(name1);
    }
})