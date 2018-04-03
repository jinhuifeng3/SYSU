$(function() {
    $("#userName").blur(checkUserName);
    $("#studentId").blur(checkStudentId);
    $("#phone").blur(checkPhone);
    $("#email").blur(checkEmail);
    $("input").blur(deleteWarning);
    $("#reset").click(resetFunction);
    $("#submit").click(checkAll);
});

function checkUserName() {
    var userName = $("#userName").val();
    $(this).parent().find(".warning").html("");
    var reg = /^[a-zA-Z][a-zA-Z0-9_]{5,17}$/;
    if (userName == '') {
        return false;
    } else if (reg.test(userName)) {
        return true;
    } else if (userName.length < 6 || userName.length > 18) {
        $(this).parent().find(".warning").html("用户名长度为6-18个字符");
        return false;
    } else if (/^[^a-zA-Z]/.test(userName)) {
        $(this).parent().find(".warning").html("开头应该为英文字母");
        return false;
    } else if (/[^a-zA-Z0-9_]/.test(userName)) {
        $(this).parent().find(".warning").html("用户名只能由英文字母、数字或者下划线组成");
        return false;
    }
}

function checkStudentId() {
    var studentId = $("#studentId").val();
    $(this).parent().find(".warning").html("");
    var reg = /^[^0][0-9]{7}$/;
    if (studentId == "") {
        return false;
    } else if (reg.test(studentId)) {
        return true;
    } else if (studentId.length != 8) {
        $(this).parent().find(".warning").html("学号应为8位");
        return false;
    } else if (/[^0-9]/.test(studentId)) {
        $(this).parent().find(".warning").html("学号应为数字");
        return false;
    } else if (/^[0]/.test(studentId)) {
        $(this).parent().find(".warning").html("学号开头不能为0");
        return false;
    }
}

function checkPhone() {
    var phone = $("#phone").val();
    $(this).parent().find(".warning").html("");
    var reg = /^[^0][0-9]{10}$/;
    if (phone == "") {
        return false;
    } else if (reg.test(phone)) {
        return true;
    } else if (phone.length != 11) {
        $(this).parent().find(".warning").html("电话应为11位");
        return false;
    } else if (/[^0-9]/.test(phone)) {
        $(this).parent().find(".warning").html("电话应为数字");
        return false;
    } else if (/^[0]/.test(phone)) {
        $(this).parent().find(".warning").html("电话开头不能为0");
        return false;
    }
}

function checkEmail() {
    var email = $("#email").val();
    $(this).parent().find(".warning").html("");
    var reg = /^[0-9a-zA-Z_\-]+@(([a-zA-Z_\-])+\.)+[a-zA-Z]{2,4}$/;
    if (email == "") {
        return false;
    } else if (reg.test(email)) {
        return true;
    } else {
        $(this).parent().find(".warning").html("请输入正确的邮箱地址");
        return false;
    }
}

function resetFunction() {
    $(".warning").html("");
}

function checkAll() {
    if (checkUserName() && checkStudentId() &&
        checkPhone() && checkEmail())
        return true;
    else return false;
}

function deleteWarning() {
    $("#warnings").html("");
}