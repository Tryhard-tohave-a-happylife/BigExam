$(document).ready(function () {
    $(".colClick").click(function () {
        var index = $(".colClick").index(this);
        var listForm = $(".borderColJobMenu form");
        $(listForm[index]).slideToggle();
    })
    $("#clearAll").click(function() {
        location.reload();
    })
    $("#findIcon").click(function () {
        var jobBrowser = $("input[name='jobBrowser']");
        var listCareer = $("input[name='ListCareer']");
        var listArea = $("input[name='ListArea']");
        var salary = $("input[name='selectSalary']");
        var positionEmployee = $("input[name='selectPosition']");
        var experience = $("input[name='selectExperience']");
        var sex = $("input[name='selectGender']");
        var levelLearning = $("input[name='selectLevel']");
        var datePosted = $("input[name='selectDate']");
        var dbParam = "OfferName=" + jobBrowser + "&Area=" + listArea + "&listCareer=" + listCareer
            + "&OfferSalary=" + salary + "&PositionJobID=" + positionEmployee + "&Sex=" + sex + "&ExperienceRequest=" + experience
             + "&LearningLevelRequest=" + levelLearning + "&OfrerCreateDate=" + datePosted;
        window.location.href = "/SearchCandidateResult?" + dbParam;
    })

    function checkName() {
        var name = $("input[name='jobBrower']");
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
        $("input[name='jobBrower']").val(name1);
    }
})
