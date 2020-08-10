
var AreaID = 0, JobID = 0, Name = "";

$(document).ready(function () {
    GetURLParameter();
    setSelectValue();
    
    $("#tim-nang-cao").click(function () {
        $("#search-more").slideToggle(500);
    });
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
            Name = sParameterName[1];
           
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
    if (Name != "") {
        document.getElementById("key-word").value = Name;

        console.log(Name);
    }
}

function search() {
    var area = document.getElementById("dia-diem").value;
    var job = document.getElementById("linh-vuc").value;
    var name = document.getElementById("key-word").value;
    var dbParam = "Name=" + name + "&AreaID=" + area + "&JobID=" + job;
    window.location.href = "/SearchCandidateResult?" + dbParam;
}

function refresh() {
    if (document.getElementById("linh-vuc").value != 0 || document.getElementById("dia-diem").value != 0 || document.getElementById("key-word").value) {
        console.log(document.getElementById("key-word").value);
        window.location.href = "/SearchCandidateResult";
        document.getElementById("linh-vuc").value = 0;
        document.getElementById("dia-diem").value = 0;
        document.getElementById("key-word").value = "";
      
    }
}

