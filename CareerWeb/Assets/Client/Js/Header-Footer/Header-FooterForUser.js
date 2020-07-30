$(document).ready(function () {
    var opening = 0;
    $('.responsive_menu_icon').click(function () {
        if (opening == 0)
            opening++;
        else
            opening--;
        $('.responsive_menu').toggleClass("open");
    });
    $(window).resize(function () {
        if ($(window).width() > 1050) {
            $('.responsive_menu').removeClass("open");
        }
        else {
            if (opening == 1)
                $('.responsive_menu').addClass("open");
        }
    });
    $('.searching, .res_searching').click(function () {
        $(location).attr("href", "./index-2.html");
    });
    $("header .user .log_in").click(function () {
        $("header #box-logIn").css("display", "block");
    });
    $("header #box-logIn #submit").click(function () {
        var accName = $("header #box-logIn #acc").val();
        var pass = $("header #box-logIn #pass").val();
        if (!accName || !pass || accName == "" || pass == "") {
            alert("Bạn cần nhập đủ");
            return;
        }
        $.ajax({
            data: { accName: accName, passWord: pass },
            url: "/Account/Login",
            dataType: "Json",
            method: "Post",
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    if (res.type == 1) {
                        window.location.href = "/User/Index";
                    }
                }
                else {
                    if (res.error == -1) {
                        alert("Tài khoản không tồn tại");
                    }
                    else {
                        alert("Mật khẩu không chính xác");
                    }
                }
            }
        })

    })

})