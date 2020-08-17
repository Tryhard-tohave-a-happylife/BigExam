$(document).ready(function(){
    $('.button_contact_information').click(function(){
        $('.contact_information').hide();
        $('.contact_information_modify').show();
    })
    $('#cancel_3').click(function(){
        $('.contact_information').show();
        $('.contact_information_modify').hide();
    })
    $('.button_company_information').click(function(){
        $('.company_information').hide();
        $('.company_information_modify').show();
    })
    $('#cancel_2').click(function(){
        $('.company_information').show();
        $('.company_information_modify').hide();
    })
    $('.logo_button_upload').click(function(){
        $('.company_logo').hide();
        $('.company_logo_modify').show();
    })
    $('#cancel_1').click(function(){
        $('.company_logo').show();
        $('.company_logo_modify').hide();
    })
    $("#submit_3").click(function () {
        var email = $("input[name='email']").val();
        var mobile = $("input[name='mobile']").val();
        if (!Boolean(email) || !Boolean(mobile)) {
            alert("Bạn phải nhập thông tin")
        } else {
        var cf = confirm("Thay đổi thông tin này?");
        if (!cf) return;
        $.ajax({
            data: { email: email, mobile: mobile },
            url: "/Enterprise/ChangeContactInfo",
            method: "Post",
            dataType: "Json",
            success: function (res) {
                if (res.status) {
                    location.reload();
                }
                else {
                    alert("Hệ thống gặp trục trặc")
                }
            }
        })
        $('.contact_information').show();
        $('.contact_information_modify').hide();}
    })
    $("#submit_2").click(function () {
        var type = parseInt($("select[name='type']").val(), 10);
        var size = parseInt($("select[name='size']").val(), 10);
        var establishYear = parseInt($("input[name='establishYear']").val(), 10);
        var description = $("textarea[name='description']").val();
        if (!Boolean(type) || !Boolean(size) || !Boolean(establishYear) || !Boolean(description)) {
            alert("Bạn phải nhập thông tin")
        } else {
        var cf = confirm("Thay đổi thông tin này?");
        if (!cf) return;
        $.ajax({
            data: { type: type, size: size, establishYear: establishYear, description: description },
            url: "/Enterprise/ChangeCompanyInfo",
            method: "Post",
            dataType: "Json",
            success: function (res) {
                if (res.status) {
                    location.reload();
                }
                else {
                    alert("Hệ thống gặp trục trặc")
                }
            }
        })
        $('.company_information').show();
        $('.company_information_modify').hide();}
    })
    $("#submit_1").click(function () {
        var file = $("#logo").get(0).files;
        var data = new FormData;
        if (!file || !file[0]) {
            alert("Bạn cần chọn hình ảnh logo!");
        } else {
            data.append("ImageFile", file[0]);
            var cf = confirm("Thay đổi Logo?");
            if (!cf) return;
        $.ajax({
            data: data,
            url: '/Enterprise/ChangeCompanyLogo',
            dataType: 'json',
            method: 'POST',
            contentType: false,
            processData: false,
            success: function (res) {
                location.reload();
            }
        })
            $('.company_logo').show();
            $('.company_logo_modify').hide();
        }
    })
})