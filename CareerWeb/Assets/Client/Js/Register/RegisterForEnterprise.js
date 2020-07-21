$(document).ready(function () {
    $("#imageBrowse").change(function () {
        var File = this.files;
        if (File && File[0]) {
            ReadImage(File[0]);
        }
    })
    $("#submit-first").click(function () {
        var string = "123";
        $.ajax({
            data: {name : string},
            url: '/TypeOfEnterprise/ReturnListType',
            dataType: 'json',
            method: 'POST',
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    $.each(res.listType, function (index, item) {
                        var eachHTML = $(`<option value = '` + item.TypeOfEnterpriseID + `'>` + item.NameOfEnterprise + `</option>`);
                        $(".container-input #typeOfCompany").append(eachHTML);
                    });
                }
            }
        })
    })
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
                $("#loadImg").attr("src", res.srcImage);
            }
        })
    })
    $("#chooseImage").click(function () {
        $("#imageBrowse").click();
    })
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