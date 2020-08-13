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
        var jobBrowser = $("input[name='jobBrowser']").val();
        var OfferMajor = $('#OfferMajor option[value="' + $('#as').val() + '"]').data('id');
        var listArea = $('#ListArea option[value="' + $('#bs').val() + '"]').data('id');
        var salary = ($("input[name='selectSalary']:checked").val() != null) ? $("input[name='selectSalary']:checked").val() : 0;
        var positionEmployee = ($("input[name='selectPosition']:checked").val() != null) ? $("input[name='selectPosition']:checked").val() : 0;
        var experience = ($("input[name='selectExperience']:checked").val() != null) ? $("input[name='selectExperience']:checked").val() : 0;
        var sex = ($("input[name='selectGender']:checked").val() != null) ? $("input[name='selectGender']:checked").val() : 0;
        var levelLearning = ($("input[name='selectLevel']:checked").val() != null) ? $("input[name='selectLevel']:checked").val() : 0;
        var dbParam = "OfferName=" + jobBrowser + "&Area=" + listArea + "&OfferMajor=" + OfferMajor
            + "&OfferSalary=" + salary + "&PositionJobID=" + positionEmployee + "&Sex=" + sex + "&ExperienceRequest=" + experience
             + "&LearningLevelRequest=" + levelLearning;
        window.location.href = "/SearchJobForUser?" + dbParam;
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
