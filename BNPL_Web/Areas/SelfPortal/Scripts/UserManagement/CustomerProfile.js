
function Add() {
    var FullName = $("#UserName").val();
    var Password = $("#Password").val();
    var Mobile = $("#Mobile").val();
    var Dob = $("#Dob").val();
    var Email = $("#Email").val();
    var FirstName = $("#FirstNameAr").val();
    var MiddleName = $("#MiddleNameAr").val();
    var LastName = $("#LastNameAr").val();
    var FirstNameEn = $("#FirstNameEn").val();
    var MiddleNameEn = $("#MiddleNameEn").val();
    var LastNameEn = $("#LastNameEn").val();
    var Language = $("#Language").val();
    var Gender = $("#Gender").val();
    var CivilId = $("#CivilId").val();
    var TitleId = $("#TitleId").val();

    if (FirstName == "" || FirstName == undefined) {
        toastr.warning("Please Enter Firs Name Ar", { timeOut: 5000 });
        return false;
    }
    if (MiddleName == "" || MiddleName == undefined) {
        toastr.warning("Please Enter Middle Name Ar", { timeOut: 5000 });
        return false;
    }
    if (FullName == "" || FullName == undefined) {
        toastr.warning("Please Enter Full Name Ar", { timeOut: 5000 });
        return false;
    }

    if (FirstNameEn == "" || FirstNameEn == undefined) {
        toastr.warning("Please Enter Firs Name En", { timeOut: 5000 });
        return false;
    }
    if (MiddleNameEn == "" || MiddleNameEn == undefined) {
        toastr.warning("Please Enter Middle Name En", { timeOut: 5000 });
        return false;
    }
    if (LastNameEn == "" || LastNameEn == undefined) {
        toastr.warning("Please Enter Full Name En", { timeOut: 5000 });
        return false;
    }
    if (Email == "" || Email == undefined) {
        toastr.warning("Please Enter Email", { timeOut: 5000 });
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
    if (Dob == "" || Dob == undefined) {
        toastr.warning("Please Enter Date Of Birth", { timeOut: 5000 });
        return false;
    }
    if (Language == "" || Language == undefined) {
        toastr.warning("Please Enter Language", { timeOut: 5000 });
        return false;
    }
    if (Gender == "" || Gender == undefined) {
        toastr.warning("Please Enter Gender", { timeOut: 5000 });
        return false;
    }
    if (CivilId == "" || CivilId == undefined) {
        toastr.warning("Please Enter Civil Id", { timeOut: 5000 });
        return false;
    }
    if (TitleId == "" || TitleId == undefined) {
        toastr.warning("Please Enter Civil Id", { timeOut: 5000 });
        return false;
    }
    var User = {
        UserName: FullName,
        FirstNameEn: FirstNameEn,
        LastNameEn: LastNameEn,
        MiddlelNameEn: MiddleNameEn,
        FirstNameAr: FirstNameEn,
        LastNameAr: LastNameEn,
        MiddlelNameAr: MiddleNameEn,
        Title: TitleId,
        Gender: Gender,
        CivilId: CivilId,
        Password: Password,
        PhoneNumber: Mobile,
        DateOfBirth: Dob,
        Email: Email,
        Language: Language
    }
    $.ajax({
        url: '/api/User/Post',
        type: 'POST',
        data: JSON.stringify(User),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            debugger;
            toastr.success("User added successfully", { timeOut: 5000 });
        },
        error: function (data) {
            var response = data.responseText.replace(/"/g, '');
            toastr.error(response, { timeOut: 5000 });
            HideModalLoader();

        }
    });
}