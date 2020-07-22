$(document).ready(function () {
    //common
    $("input[type='text']").blur(function () {
        var str = $(this).val().trim();
        var ret = "";
        for (var i = 0; i < str.length; i += 1) {
            if (str[i] != ' ') {
                if (i == 0 || str[i - 1] == ' ' || str[i - 1] == '/') ret += str[i].toUpperCase();
                else ret += str[i].toLowerCase();
            }
            else {
                if (str[i - 1] != ' ') ret += ' ';
            }
        }
        $(this).val(ret);
        return;
    })
    $("#submit-first").click(function () {
        $.ajax({
            data: { accountName: emailReg, accountPass: passReg, typeOfAccount : "Enterprise" },
            url: '/Account/CreateAccount',
            dataType: 'json',
            method: 'POST',
            beforeSend: function () {

            },
            success: function (res) {
                listArea = res.listArea;
                listJob = res.listJob;
            }
        })
    })
    //Part 2
    $("#search-area").keyup(function () {
        var sg = $("#suggest-area");
        $(sg).empty();
        var str = $(this).val();
        if (str == "" || str == null) return;
        str = str.toLowerCase();
        $.each(listArea, function (index, item) {
            if (item.NameArea.toLowerCase().indexOf(str) != -1) {
                var eachHTML = $(`<div area-id = "` + item.AreaId + `" class = "list-sg"> ` + item.NameArea + `</div>`);
                $(sg).append(eachHTML);
            }
        });
    })
    $("#suggest-area").on("click", ".list-sg", function () {
        var ind = Number($(this).attr("area-id"));
        var text = $(this).text();
        if (addIdListArea.indexOf(ind) == -1) {
            addIdListArea.push(ind);
            var eachHTML = $(`<div class = "option-choosed" area-id = "` + ind + `"><div>` + text + `</div> <span>x</span></div>`);
            $("#option-area").append(eachHTML);
        }
        //console.log(addIdListJob)
    })
    $("#option-area").on("click", ".option-choosed span", function () {
        var ind = $("#option-area .option-choosed span").index(this);
        var opt = $("#option-area .option-choosed");
        var removeID = addIdListArea.indexOf(Number($(opt[ind]).attr("area-id")));
        $(opt[ind]).remove();
        var swap = 0;
        swap = addIdListArea[addIdListArea.length - 1];
        addIdListArea[addIdListArea.length - 1] = addIdListArea[removeID];
        addIdListArea[removeID] = swap;
        addIdListArea.pop();
        //console.log(addIdListArea);
    })
    $("#search-important").keyup(function () {
        var sg = $("#suggest-important");
        $(sg).empty();
        var str = $(this).val();
        if (str == "" || str == null) return;
        str = str.toLowerCase();
        $.each(listJob, function (index, item) {
            if (item.JobName.toLowerCase().indexOf(str) != -1) {
                var eachHTML = $(`<div job-id = "` + item.JobID + `" class = "list-sg"> ` + item.JobName + `</div>`);
                $(sg).append(eachHTML);
            }
        });
    })
    $("#suggest-important").on("click", ".list-sg", function () {
        var ind = Number($(this).attr("job-id"));
        var text = $(this).text();
        if (addIdListJobSub.indexOf(ind) != -1) {
            alert("Bạn đã chọn ngành nghề này dưới phần phụ!");
            return;
        }
        if (addIdListJobImp.indexOf(ind) == -1) {
            addIdListJobImp.push(ind);
            var eachHTML = $(`<div class = "option-choosed" job-id = "` + ind + `"><div>` + text + `</div> <span>x</span></div>`);
            $("#option-important").append(eachHTML);
        }
        //console.log(addIdListJob)
    })
    $("#option-important").on("click", ".option-choosed span", function () {
        var ind = $("#option-important .option-choosed span").index(this);
        var opt = $("#option-important .option-choosed");
        var removeID = addIdListJobImp.indexOf(Number($(opt[ind]).attr("job-id")));
        $(opt[ind]).remove();
        var swap = 0;
        swap = addIdListJobImp[addIdListJobImp.length - 1];
        addIdListJobImp[addIdListJobImp.length - 1] = addIdListJobImp[removeID];
        addIdListJobImp[removeID] = swap;
        addIdListJobImp.pop();
        //console.log(addIdListJobImp);
    })
    $("#search-sub").keyup(function () {
        var sg = $("#suggest-sub");
        $(sg).empty();
        var str = $(this).val();
        if (str == "" || str == null) return;
        str = str.toLowerCase();
        $.each(listJob, function (index, item) {
            if (item.JobName.toLowerCase().indexOf(str) != -1) {
                var eachHTML = $(`<div job-id = "` + item.JobID + `" class = "list-sg"> ` + item.JobName + `</div>`);
                $(sg).append(eachHTML);
            }
        });
    })
    $("#suggest-sub").on("click", ".list-sg", function () {
        var ind = Number($(this).attr("job-id"));
        var text = $(this).text();
        if (addIdListJobImp.indexOf(ind) != -1) {
            alert("Bạn đã chọn ngành nghề này trên phần chính!");
            return;
        }
        if (addIdListJobSub.indexOf(ind) == -1) {
            addIdListJobSub.push(ind);
            var eachHTML = $(`<div class = "option-choosed" job-id = "` + ind + `"><div>` + text + `</div> <span>x</span></div>`);
            $("#option-sub").append(eachHTML);
        }
        //console.log(addIdListJob)
    })
    $("#option-sub").on("click", ".option-choosed span", function () {
        var ind = $("#option-sub .option-choosed span").index(this);
        var opt = $("#option-sub .option-choosed");
        var removeID = addIdListJobSub.indexOf(Number($(opt[ind]).attr("job-id")));
        $(opt[ind]).remove();
        var swap = 0;
        swap = addIdListJobSub[addIdListJobSub.length - 1];
        addIdListJobSub[addIdListJobSub.length - 1] = addIdListJobSub[removeID];
        addIdListJobSub[removeID] = swap;
        addIdListJobSub.pop();
        //console.log(addIdListJobSub);
    })
    $(".add-more").click(function () {
        $("#choose-box-second #cover").css("display", "block");
        $("#container-add #add").attr("job-id", $(this).attr("atr"));
    })
    $("#container-add #add").click(function () {
        var jobAdd = $("#container-add input[type='text']").val();
        if (jobAdd == "" || jobAdd == null) {
            alert("Bạn cần nhập tên công việc!");
            return;
        }
        if ($(this).attr("atr") == "job-imp") {
            if (newListJobImp.indexOf(jobAdd.toLowerCase()) == -1) {
                newListJobImp.push(jobAdd.toLowerCase());
                var eachHTML = $(`<div class = "option-add" atr = 'job-imp'><div>` + jobAdd + `</div> <span>x</span></div>`);
                $("#container-add input[type='text']").val("");
                $("#choose-box-second #cover").css("display", "none");
                $("#option-important").append(eachHTML);
            }
            else {
                alert("Ngành nghề đã được lựa chọn");
                return;
            }
        }
        else {
            if (newListJobSub.indexOf(jobAdd.toLowerCase()) == -1) {
                newListJobSub.push(jobAdd.toLowerCase());
                var eachHTML = $(`<div class = "option-add" atr = 'job-sub'><div>` + jobAdd + `</div> <span>x</span></div>`);
                $("#container-add input[type='text']").val("");
                $("#choose-box-second #cover").css("display", "none");
                $("#option-sub").append(eachHTML);
            }
            else {
                alert("Ngành nghề đã được lựa chọn");
                return;
            }
        }
    })
    $("#option-important").on("click", ".option-add span", function () {
        var ind = $("#option-important .option-add span").index(this);
        var opt = $("#option-important .option-add");
        var removeID = newListJobImp.indexOf($(opt).text().toLowerCase());
        $(opt[ind]).remove();
        var swap = 0;
        swap = newListJobImp[newListJobImp.length - 1];
        newListJobImp[newListJobImp.length - 1] = newListJobImp[removeID];
        newListJobImp[removeID] = swap;
        newListJobImp.pop();
    })
    $("#option-sub").on("click", ".option-add sub", function () {
        var ind = $("#option-sub .option-add span").index(this);
        var opt = $("#option-sub .option-add");
        var removeID = newListJobSub.indexOf($(opt).text().toLowerCase());
        $(opt[ind]).remove();
        var swap = 0;
        swap = newListJobSub[newListJobSub.length - 1];
        newListJobSub[newListJobSub.length - 1] = newListJobSub[removeID];
        newListJobSub[removeID] = swap;
        newListJobSub.pop();
    })
    $("#container-add #cancel").click(function () {
        $("#container-add input[type='text']").val("");
        $("#choose-box-second #cover").css("display", "none");
    })
    $("#choose-box-second button").click(function () {
        var name = $("#enterpriseName").val();
        var mobile = $("#mobile").val();
        var establishYear = $("#establishYear").val();
        var enterpriseSize = Number($("#enterprise-size").val());
        var typeEnterprise = Number($("#type-company").val());
    })
    ///Upload Image;
    $("#uploadImage").click(function () {
        var file = $("#imageBrowse").get(0).files;
        var data = new FormData;
        if (!file || !file[0]) {
            alert("Bạn cần chọn hình ảnh logo!");
        }
        data.append("ImageFile", file[0]);
        $.ajax({
            data: data,
            url: '/Enterprise/ImageUpload',
            dataType: 'json',
            method: 'POST',
            contentType: false,
            processData: false,
            beforeSend: function () {
              
            },
            success: function (res) {
                srcImage = res.srcImage;
            }
        })
    })
    $("#chooseImage").click(function () {
        $("#imageBrowse").click();
    })
    $("#imageBrowse").change(function () {
        var File = this.files;
        if (File && File[0]) {
            ReadImage(File[0]);
        }
    })
    // Biến lưu dữ liệu
    var srcImage = "";
    var emailReg = "";
    var passReg = "";
    var code = "";
    var userID;
    var listJob;
    var listArea;
    var addIdListArea = [];
    var addIdListJobImp = [];
    var addIdListJobSub = [];
    var newListJobImp = [];
    var newListJobSub = [];
})
var ReadImage = function (file) {
    var reader = new FileReader;
    var image = new Image;
    reader.readAsDataURL(file);
    reader.onload = function (_file) {
        image.src = _file.target.result;
        image.onload = function () {
            $("#targetImg").attr('src', image.src);
        }
    }
}