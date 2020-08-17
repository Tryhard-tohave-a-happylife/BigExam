$(document).ready(function () {
    $("#box-create").css("display", "none");
    $(".each-part-small #input-category-big").change(function () {
        $(".each-part-small #input-category-small").empty();
        if ($(this).val() == "none") return;
        var id = Number($(this).val());
        $.ajax({
            data: { parentID: id},
            url: "/Article/ListCategorySmallArticle",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (!res.status) {
                    alert("Hệ thống gặp trục trặc");
                }
                else {
                    for (var i = 0; i < res.list.length; i++) {
                        var eachHTML = $(`<option value = '` + res.list[i].CategoryID + `'>` + res.list[i].NameCategory + `</option>`);
                        $(".each-part-small #input-category-small").append(eachHTML);
                    }
                }
            }
        })
    })
    // Thêm , edit bài viết
    $("#box-footer #both-input").click(function () {
        var title = $("#box-create input[name='Title']").val();
        var categoryBig = $("#box-create select[name='CategoryBig']").val();
        var categorySmall = $("#box-create select[name='CategorySmall']").val();
        var description = $("#box-create textarea[name='Description']").val();
        var ContentArticle = CKEDITOR.instances["input-content"].getData();
        var file = $("#box-create input[name='Image']").get(0).files;
        //console.log(title + " " + categoryBig + " " + categorySmall + " " + description + " " + ContentArticle + " " + file);
        if (!Boolean(title) || !Boolean(categoryBig) || !Boolean(categorySmall) || !Boolean(description) ||
            !Boolean(ContentArticle)) {
            alert("Bạn cần nhập đủ thông tin bài viết!");
            return;
        }
        if ($("input[name='typeAction']").val() == "add" && (file == null || file[0] == null)) {
            alert("Bạn cần nhập đủ thông tin bài viết!");
            return;
        }
        var cf = $("input[name='typeAction']").val() == "add" ? confirm("Thêm mới bài viết này?") : confirm("Chỉnh sửa bài viết này?");
        if (!cf) return;
        $("#box-create form").submit();
    })
    // Chọn thêm mới bài viết
    $("#contain-button #button-new").click(function () {
        $("#box-create").slideDown();
        $("#box-contain").slideUp();
        $("input[name='typeAction']").val("add");
        $("#box-footer #both-input").text("Thêm");
    })
    // Chọn edit bài viết
    $(".each-article .contain-right .edit").click(function () {
        var status = $(this).attr("status");
        if (status == "Complete") {
            alert("Bạn không thể chỉnh sửa bài viết đã đăng");
            return;
        }
        $("#box-create").slideDown();
        $("#box-contain").slideUp();
        var id = Number($(this).attr("data-id"));
        $.ajax({
            data: { articleID : id},
            url: "/Article/ReturnArticle",
            method: "Post",
            type: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (!res.status) {
                    alert("Hệ thống gặp trục trặc!");
                }
                else {
                    $("input[name='typeAction']").val("edit");
                    $("input[name='saveID']").val(id);
                    $("#box-create input[name='Title']").val(res.article.Title);
                    $("#box-create select[name='CategoryBig']").val(res.article.CategoryParent);    
                    $("#box-create textarea[name='Description']").val(res.article.Description);
                    CKEDITOR.instances["input-content"].setData(res.article.ContentArticle);
                    for (var i = 0; i < res.list.length; i++) {
                        var eachHTML = null;
                        if (res.list[i].CategoryID == res.article.CategoryID) {
                            eachHTML = $(`<option value = '` + res.list[i].CategoryID + `' selected>` + res.list[i].NameCategory + `</option>`);
                        }
                        else {
                            eachHTML = $(`<option value = '` + res.list[i].CategoryID + `'>` + res.list[i].NameCategory + `</option>`);
                        }
                        $(".each-part-small #input-category-small").append(eachHTML);
                    }
                    $("#box-footer #both-input").text("Sửa");
                }
            }
        })
    })
    // Xóa bài viết
    $(".each-article .contain-right .delete").click(function () {
        var cf = confirm("Xóa bài viết này ?");
        if (!cf) return;
        var id = Number($(this).attr("data-id"));
        $.ajax({
            data: { articleID: id },
            url: "/Article/DeleteArticle",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (!res.status) {
                    alert("Hệ thống gặp trục trặc");
                }
                else {
                    window.location.href = "/Employee/ListArticleEmployee";
                }
            }
        })
    })
    // Hủy
    $("#box-create #box-footer #cancel").click(function () {
        $("#box-create").slideUp();
        $("#box-contain").slideDown();
        $("#box-create input[name='Title']").val("");
        $("#box-create select[name='CategoryBig']").val("");
        $("#box-create select[name='CategorySmall']").val("");
        $("#box-create textarea[name='Description']").val("");
        CKEDITOR.instances["input-content"].setData("");
        $("#box-create input[name='Image']").val("");
    })
})