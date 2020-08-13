$(document).ready(function () {
    ///Check định dạng tháng năm
    function checkFormatDate(inp) {
        var ind = -1;
        for (var i = 0; i < inp.length; i++) {
            if (inp[i] == '/') {
                if (ind != - 1) {
                    return false;
                }
                ind = i;
            }
        }
        if (ind == -1) return false;
        var twoElements = inp.split("/");
        if (isNaN(twoElements[0]) || Number(twoElements[0]) < 1 || Number(twoElements[0]) > 12) {
            return false;
        }
        if (isNaN(twoElements[1]) || Number(twoElements[1]) < 1 || Number(twoElements[1]) > Number(new Date().getFullYear())) {
            return false;
        }
        return true;
    }
    function validateEmail(email) {
        const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }
    function checkFormatFullDate(inp) {
        var indFirst = -1;
        var indSecond = -1;
        for (var i = 0; i < inp.length; i++) {
            if (inp[i] == '/') {
                if (indFirst == -1) {
                    indFirst = i;
                    continue;
                }
                else if (indSecond == -1) {
                    indSecond = i;
                    continue;
                }
                else {
                    return false;
                }
            }
        }
        if (indFirst == -1 || indSecond == -1) return false;
        var threeElements = inp.split("/");
        if (isNaN(threeElements[0]) || Number(threeElements[0]) < 1 || Number(threeElements[0]) > 31) {
            return false;
        }
        if (isNaN(threeElements[1]) || Number(threeElements[1]) < 1 || Number(threeElements[0]) > 12) {
            return false;
        }
        if (isNaN(threeElements[2]) || Number(threeElements[2]) < 1 || Number(threeElements[2]) > Number(new Date().getFullYear())) {
            return false;
        }
        return true;

    }
    // Common
    $("#chart-infor").easyPieChart({
        size: 210,
        barColor: "blue",
        scaleColor: false,
        lineWidth: 12,
        trackColor: "#d1dced",
        lineCap: "circle",
        animate: 1000
    })
    $(".list-part").click(function (e) {
        e.preventDefault();
        var id = $(this).attr("data-link");
        var top = document.getElementById(id);
        var ps = Number($(top).offset().top) - 100;
        $("html, body").animate({ scrollTop: ps }, 650);
    })
    $(".add-more").click(function (e) {
        e.preventDefault();
        var ind = $(".add-more").index(this);
        var scroll = document.getElementById($(this).attr("add-for"));
        var topScroll = Number($(scroll).offset().top) - 100;
        var listInputInfor = $(".input-infor");
        $(listInputInfor[ind]).slideDown();
        $("html, body").animate({ scrollTop: topScroll }, 650);
    })
    $(".infor-details .cancel").click(function () {
        var ind = $(".infor-details .cancel").index(this);
        var listInputInfor = $(".input-infor");
        $(listInputInfor[ind]).slideUp();
        $(listInputInfor[ind]).children().children("input[type = 'text']").val("");
        $(listInputInfor[ind]).children().children("textarea").val("");
    })
    $(".input-time").blur(function () {
        var inp = $(this).val();
        if (inp == "" || !inp) return;
        if (!checkFormatDate(inp)) {
            $(this).css("background-color", "rgba(255, 117, 102, 0.4)");
            $(this).next().css("display", "block");
        }
        else {
            $(this).css("background-color", "white");
            $(this).next().css("display", "none");
        }
    })
    $("#user-dob").blur(function () {
        var inp = $(this).val();
        if (inp == "" || !inp) return;
        if (!checkFormatFullDate(inp)) {
            $(this).css("background-color", "rgba(255, 117, 102, 0.4)");
            $(this).next().css("display", "block");
        }
        else {
            $(this).css("background-color", "white");
            $(this).next().css("display", "none");
        }
    })
    $("#user-email").blur(function () {
        var inp = $(this).val();
        if (inp == "" || !inp) return;
        if (!validateEmail(inp)) {
            $(this).css("background-color", "rgba(255, 117, 102, 0.4)");
            $(this).next().css("display", "block");
        }
        else {
            $(this).css("background-color", "white");
            $(this).next().css("display", "none");
        }
    })
    $("#contact-id .first-title #edit-profile").click(function () {
        $("#contact-id .input-infor-user").slideDown();
    })
    $("#contact-id .infor-details .cancel").click(function () {
        $("#contact-id .input-infor-user").slideUp();
    })
    //Chỉnh sửa thông tin
    $(".input-infor-user .infor-details .submit").click(function () {
        var userName = $(".infor-details #user-name").val();
        var userDob = $(".infor-details #user-dob").val();
        var userGender = $(".infor-details #user-gender").val();
        var userEmail = $(".infor-details #user-email").val();
        var userMobile = $(".infor-details #user-mobile").val();
        var userArea = $(".infor-details #user-area").val();
        var userAddress = $(".infor-details #user-address").val();
        if (userName == "" || userDob == "" || userEmail == "" || userMobile == "" || userArea == ""
            || !userName || !userDob || !userEmail || !userMobile || !userArea || !checkFormatFullDate(userDob)
            || !validateEmail(userEmail)) {
            alert("Bạn phải nhập đủ và chính xác thông tin!");
            return;
        }
        var data = new FormData;
        data.append("userName", userName);
        data.append("userDob", userDob);
        data.append("userGender", userGender);
        data.append("userEmail", userEmail);
        data.append("userMobile", userMobile);
        data.append("userArea", Number(userArea));
        data.append("userAddress", userAddress);
        $.ajax({
            data: data,
            url: "/User/ModifyUser",
            method: "Post",
            dataType: "json",
            contentType: false,
            processData: false,
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    window.location.href = "/User/Index";
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })
    })
    ///Upload Image;
    $("#container-img #save-image").click(function () {
        var file = $("#container-img #image-browser").get(0).files;
        var data = new FormData;
        if (!file || !file[0]) {
            alert("Bạn cần chọn hình ảnh!");
        }
        data.append("ImageFile", file[0]);
        $.ajax({
            data: data,
            url: '/User/ImageUpload',
            dataType: 'json',
            method: 'POST',
            contentType: false,
            processData: false,
            beforeSend: function () {
            },
            success: function (res) {
                if (res.status) {
                    alert("Upload ảnh thành công!");
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })
    })
    var ReadImage = function (file) {
        var reader = new FileReader;
        var image = new Image;
        reader.readAsDataURL(file);
        reader.onload = function (_file) {
            image.src = _file.target.result;
            image.onload = function () {
                $("#container-img #img-profile").attr('src', image.src);
            }
        }
    }
    $("#container-img #choose-image").click(function () {
        $("#container-img #image-browser").click();
    })
    $("#container-img #image-browser").change(function () {
        var File = this.files;
        if (File && File[0]) {
            ReadImage(File[0]);
        }
    })
    //Limit length Select
    $("select").focus(function () {
        if ($(this).children().length > 6) {
            this.size = 6;
        }
    })
    $("select").mousedown(function () {
        if ($(this).children().length > 6) {
            this.size = 6;
        }
    })
    $("select").change(function () {
        this.size = 0;
    })
    $("select").blur(function () {
        this.size = 0;
    })
})