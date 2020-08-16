
    function search() {
        var area = document.getElementById("dia-diem").value;
        alert(area);
        var job = document.getElementById("linh-vuc").value;
        var name = document.getElementById("key-word").value;
        var salary = document.getElementById("salary").value;
        var careerLevel = document.getElementById("careerLevel").value;
        var gender = document.getElementById("gender").value;
        var education = document.getElementById("education").value;
        var experience = document.getElementById("experience").value;
        
        var dbParam = "OfferName=" + name + "&Area=" + area + "&OfferMajor=" + job
            + "&OfferSalary=" + salary + "&PositionJobID=" + careerLevel + "&Sex=" + gender + "&ExperienceRequest=" + experience
            + "&LearningLevelRequest=" + education;
        window.location.href = "/SearchJobForUser?" + dbParam;
    }
    
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

