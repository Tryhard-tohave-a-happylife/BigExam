$(document).ready(function () {
    $("#tim-nang-cao").click(function () {
        $("#search-more").slideToggle(500);
    });
    $("#tim-ho-so").click(function () {
        alert(1);
        var area = ($('#dia-diem :selected').val() != null) ? $('#dia-diem :selected').val() : 0;
        var job = ($('#linh-vuc option:selected').val() != null) ? $('#linh-vuc option:selected').val() : 0;
        alert(job);
        var name = ($('#key-word:selected').val() != null) ? $('#key-word:selected').val() : "";
        var experience = ($('#TTKN:selected').val() != null) ? $('#TTKN:selected').val() : 0;
        var salary = ($('#TTML:selected').val() != null) ? $('#TTML:selected').val() : 0;
        var language = ($('#TTNN:selected').val() != null) ? $('#TTNN:selected').val() : 0;
        var levelLanguage = ($('#TTTDNN:selected').val() != null) ? $('#TTTDNN:selected').val() : 0;

        var dbParam = "&KeyWord=" + name + "&AreaID=" + area + "&JobID=" + job + "&experienceID=" + experience +
            "&salaryID=" + salary + "&languageID=" + language + "&levelLanguageID=" + levelLanguage;
        window.location.href = "/SearchCandidateResult?" + dbParam;
    })

    $("#xoa-loc").click(function () {
        location.reload();
    })

    function checkName() {
        var name = document.getElementById("key-word").value;
        var name1 = "";
        var i = 1;
        name1 = name.charAt(0).toUpperCase();
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
        document.getElementById("key-word").value = name1;
    }
})