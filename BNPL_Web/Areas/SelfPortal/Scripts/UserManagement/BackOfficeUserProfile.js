

$(function () {
    LoadKendoDropDown('Role', '-- Please Select Profile --', '/api/Roles/GetAllRole');
});
function Add() {
    var FullName = $("#UserName").val();
    var Role = $("#Role").val();
    var Password = $("#Password").val();
    var Mobile = $("#Mobile").val();
    var Email = $("#Email").val();

    if (FullName == "" || FullName == undefined) {
        toastr.warning("Please Enter Full Name", { timeOut: 5000 });
        return false;
    }
    if (Role == "" || Role == undefined) {
        toastr.warning("Please Select Role", { timeOut: 5000 });
        return false;
    }
    if (Password == "" || Password == undefined) {
        toastr.warning("Please Enter Password", { timeOut: 5000 });
        return false;
    }
    if (Mobile == "" || Mobile == undefined) {
        toastr.warning("Please Enter Mobile", { timeOut: 5000 });
        return false;
    }
    if (Email == "" || Email == undefined) {
        toastr.warning("Please Enter Email", { timeOut: 5000 });
        return false;
    }
    var User = {
        UserName: FullName,
        RoleId: Role,
        Password: Password,
        PhoneNumber: Mobile,
        Email: Email
    }
    $.ajax({
        url: '/api/User/BackOfficeUserProfile',
        type: 'POST',
        data: JSON.stringify(User),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            toastr.success("User added successfully", { timeOut: 5000 });
            location.href = '/UserManagement/Manage';
        },
        error: function (data) {
            var response = data.responseText.replace(/"/g, '');
            toastr.error(response, { timeOut: 5000 });
            HideModalLoader();

        }
    });
}