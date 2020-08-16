
var AreaID = 0, JobID = 0, Name = 0;

$(document).ready(function () {
    GetURLParameter();
    setSelectValue();
    
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

});

function GetURLParameter() {
    var string  = window.location.search.substring(1);

    var sPageURL = decodeURI(string);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == "AreaID") {
            AreaID = sParameterName[1];
        }
        else if (sParameterName[0] == "JobID") {
            JobID = sParameterName[1];
        }
        else if (sParameterName[0] == "Name") {
            NameID = sParameterName[1];
           
        }
    }
}

function setSelectValue() {
    if (AreaID != 0) {
        document.getElementById("dia-diem").value = AreaID;
    }
    if (JobID != 0) {
        document.getElementById("linh-vuc").value = JobID;
    }
    if (Name != 0) {
        document.getElementById("key-word").value = NameID;

        console.log(Name);
    }
}


