$(document).ready(function () {
    // Xóa skill
    $("#box-main #container-skill").on("click", ".each-skill-save .delete-skill", function () {
        var cf = confirm("Xóa kỹ năng này khỏi dự án?");
        if (!cf) return;
        var projectID = Number($(this).attr("project-id"));
        var skillID = Number($(this).attr("skill-id"));
        var remove = $(this).parent();
        $.ajax({
            data: { projectID: projectID, skillID: skillID },
            url: "/Project/DeleteSkillProject",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    $(remove).remove();
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })
    })
    // Chọn thêm mới
    $("#main-right #header #button-new").click(function () {
        $("#cover-screen").css("display", "block");
        $("#box-cover").css("display", "block");
        $("input[name='typeAction']").val("add");
        $("#box-create #box-footer #both-input").text("SUBMIT");
        $("#box-create #box-header").text("TẠO DỰ ÁN MỚI");
    })
    // Chọn edit
    $("#list-create .each-project-small .button-edit").click(function () {
        var projectID = Number($(this).attr("data-id"));
        $.ajax({
            data: { projectID: projectID },
            url: "/Project/ReturnProjectAndSkill",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (!res.status) {
                    alert("Hệ thống gặp trục trặc!");
                }
                else {
                    $("#cover-screen").css("display", "block");
                    $("#box-create").css("display", "block");
                    $("input[name='typeAction']").val("edit");
                    $("#box-create #box-footer #both-input").text("EDIT");
                    $("#box-create #box-header").text("CHỈNH SỬA DỰ ÁN");
                    $("input[name='saveID']").val(projectID);
                    $("input[name='title']").val(res.project.Title);
                    $("input[name='amount']").val(res.project.Amount);
                    $("textarea[name='description']").val(res.project.Description);
                    $("select[name='major']").val(res.project.ProjectMajor);
                    for (var i = 0; i < res.listSkill.length; i++) {
                        var eachHTML = $(`<div class="each-skill-save">
                                        <div class="name-skill"> ` + res.listName[i] + `</div>
                                        <div class="delete-skill" skill-id="`+ res.listSkill[i].ProjectSkill1 + `" project-id="` + projectID +`">x</div>
                                     </div>`);
                        $("#box-main #container-skill").append(eachHTML);
                    }
                }
            }
        });
    })
    //Delete dự án
    $("#list-create .each-project-small .button-delete").click(function () {
        var cf = confirm("Xóa dự án này ?");
        if (!cf) return;
        var projectID = Number($(this).attr("data-id"));
        $.ajax({
            data: { projectID: projectID },
            url: "/Project/DeleteProject",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    window.location.href = "/Project/ListProject";
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })
    })
    // Thêm mới, chỉnh sửa
    $("#box-create #box-footer #both-input").click(function () {
        var title =  $("input[name='title']").val();
        var amount = $("input[name='amount']").val();
        var des = $("textarea[name='description']").val();
        var major = $("select[name='major']").val();
        var listSkill = $("#box-create #container-skill .each-skill");
        //console.log(title + " " + amount + " " + des + " " + major + " " + listSkill.length);
        if (!Boolean(title) || !Boolean(amount) || !Boolean(des) || major == "none") {
            alert("Bạn phải nhập đủ thông tin!");
            return;
        }
        if ($("input[name='typeAction']").val() == "add" && listSkill.length == 0) {
            alert("Bạn phải nhập đủ thông tin!");
            return;
        }
        var cf = $("input[name='typeAction']").val() == "add" ? confirm("Tạo dự án mới ?") : confirm("Chỉnh sửa dự án?");
        if (!cf) return;
        $("#box-create form").submit();
    })
    //Chọn thêm skill
    $("#box-main #input-skill").on("input", function () {
        var opt = $(this).next().children("option");
        for (var i = 0; i < opt.length; i++) {
            if ($(opt[i]).val() == $(this).val()) {
                    $(this).val("");
                    var eachHTML = $(`<div class="each-skill">
                                        <div class="name-skill"> ` + $(opt[i]).val() + `</div>
                                        <div class="delete-skill">x</div>
                                        <input type="hidden" value = "`+ $(opt[i]).attr('data-id') + `" name = "skill[]"/>
                                     </div>`);
                    $("#box-main #container-skill").append(eachHTML);
                    break;
                }
            }
    })
    // Hủy chọn skill
    $("#box-main #container-skill").on("click", ".each-skill .delete-skill", function () {
        $(this).parent().remove();
    })
    // Hủy
    $("#box-create #box-footer #cancel").click(function () {
        $("#cover-screen").css("display", "none");
        $("#box-create").css("display", "none");
        $("input[name='typeAction']").val("");
        $("input[name='saveID']").val("");
        $("input[name='title']").val("");
        $("input[name='amount']").val("");
        $("textarea[name='description']").val("");
        $("#box-create #container-skill").empty();
    })
    //Apply dự án
    $(".each-project .contain-down .apply").click(function () {
        var cf = confirm("Xác nhận ứng tuyển dự án?");
        if (!cf) return;
        var description = prompt("Nhập một chút mô tả về bản thân nào?");
        if (description == null) return;
        var projectID = Number($(this).attr("data-id"));
        var email = $(this).attr("em-ct");
        $.ajax({
            data: { projectID: projectID, description: description, email: email },
            url: "/Project/SendRequestApply",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {
                $("#img-cover-screen").css("display", "block");
            },
            success: function (res) {
                $("#img-cover-screen").css("display", "none");
                if (res.status == -1) {
                    alert("Bạn đã apply hoặc tham gia dự án này rồi");
                }
                else if (res.status == 0) {
                    alert("Hệ thống gặp trục trặc");
                }
                else {
                    alert("Gửi ứng tuyển thành công");
                }
            }
        })
    })
    // Member dự án
    $("#list-create .each-project-small .button-member").click(function () {
        var projectID = Number($(this).attr("data-id"));
        $.ajax({
            data: { projectID: projectID },
            url: "/Project/ReturnListMemberAndRequest",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    $("#cover-screen").css("display", "block");
                    $("#box-member").css("display", "block");
                    for (var i = 0; i < res.listMember.length; i++) {
                        if (res.listMember[i].Status == "request") {
                            var eachHTML = $(`<div class = "each-request">
                                                 <img src = "`+ res.memberDetail[i].UserImage + `" alt = ""/>
                                                 <div class = "contain-infor">
                                                     <div class = "name-user">`+ res.memberDetail[i].UserName + `</div>
                                                     <div class = "user-email">Emai: `+ res.memberDetail[i].UserEmail + `</div>
                                                     <div class = "user-birthDay">Ngày sinh: `+ res.memberBirthDay[i] + `</div>
                                                     <div class = "">Trạng thái: `+ res.listMember[i].Status.toUpperCase() + `</div>
                                                 </div>
                                                 <div class = "contain-button">
                                                     <div class = "view-user" data-id = "`+ res.memberDetail[i].UserId + `">VIEW</div>
                                                     <div class = "accept-user" us-id="`+ res.memberDetail[i].UserId + `" pr-id="` + res.listMember[i].ProjectID + `" em-id="` + res.memberDetail[i].UserEmail + `">ACCPET</div>
                                                     <div class = "delete-user" us-id="`+ res.memberDetail[i].UserId + `" pr-id="` + res.listMember[i].ProjectID + `">DELETE</div>
                                                 </div>
                                              </div>`)
                            $("#box-member #contain-request").append(eachHTML);
                        }
                        else if (res.listMember[i].Status == "master") {
                            var eachHTML = $(`<div class = "each-member">
                                                 <img src = "`+ res.memberDetail[i].UserImage + `" alt = ""/>
                                                 <div class = "contain-infor">
                                                     <div class = "name-user">`+ res.memberDetail[i].UserName + `</div>
                                                     <div class = "user-email">Emai: `+ res.memberDetail[i].UserEmail + `</div>
                                                     <div class = "user-birthDay">Ngày sinh: `+ res.memberBirthDay[i] + `</div>
                                                     <div class = "">Trạng thái: `+ res.listMember[i].Status.toUpperCase() + `</div>
                                                 </div>
                                                 <div class = "contain-button">
                                                     <div class = "view-user" data-id = "`+ res.memberDetail[i].UserId + `">VIEW</div>
                                                 </div>
                                              </div>`)
                            $("#box-member #contain-member").append(eachHTML);
                        }
                        else {
                            var eachHTML = $(`<div class = "each-member">
                                                 <img src = "`+ res.memberDetail[i].UserImage + `" alt = ""/>
                                                 <div class = "contain-infor">
                                                     <div class = "name-user">`+ res.memberDetail[i].UserName + `</div>
                                                     <div class = "user-email">Emai: `+ res.memberDetail[i].UserEmail + `</div>
                                                     <div class = "user-birthDay">Ngày sinh: `+ res.memberBirthDay[i] + `</div>
                                                     <div class = "">Trạng thái: `+ res.listMember[i].Status.toUpperCase() + `</div>
                                                 </div>
                                                 <div class = "contain-button">
                                                     <div class = "view-user" data-id = "`+ res.memberDetail[i].UserId + `">VIEW</div>
                                                     <div class = "delete-user" us-id="`+ res.memberDetail[i].UserId + `" pr-id="` + res.listMember[i].ProjectID + `">DELETE</div>
                                                 </div>
                                              </div>`)
                            $("#box-member #contain-member").append(eachHTML);
                        }
                    }
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })
    })
    // Tắt member
    $("#box-member #box-member-footer #cancel-member").click(function () {
        $("#cover-screen").css("display", "none");
        $("#box-member").css("display", "none");
        $("#box-member #contain-member").empty();
        $("#box-member #contain-request").empty();
    })
    // Apply request
    $("#box-member #contain-request").on("click", ".each-request .accept-user", function () {
        var cf = confirm("Chấp nhận yêu cầu ứng tuyển ?");
        if (!cf) return;
        var projectID = Number($(this).attr("pr-id"));
        var userID = $(this).attr("us-id");
        var email = $(this).attr("em-id");
        $.ajax({
            data: { projectID: projectID, userID: userID, email: email },
            url: "/Project/AcceptRequest",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {
                $("#img-cover-screen").css("display", "block");
            },
            success: function (res) {
                $("#img-cover-screen").css("display", "none");
                if (res.status) {
                    window.location.href = "/Project/ListProject"
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })
    })
    // Xóa thành viên
    $("#box-member-main").on("click", ".delete-user", function () {
        var cf = confirm("Xóa yêu cầu ứng tuyển, thành viên này ?");
        if (!cf) return;
        var projectID = Number($(this).attr("pr-id"));
        var userID = $(this).attr("us-id");
        $.ajax({
            data: { projectID: projectID, userID: userID },
            url: "/Project/DeleteMember",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {
  
            },
            success: function (res) {
                if (res.status) {
                    window.location.href = "/Project/ListProject"
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })
    });
})