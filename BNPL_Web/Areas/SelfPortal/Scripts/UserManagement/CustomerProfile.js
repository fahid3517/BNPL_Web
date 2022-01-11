var userId;
var ContractNumber;
function Add() {
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

    if (FirstNameEn == "" || FirstNameEn == undefined) {
        toastr.warning("Please Enter First Name En", { timeOut: 5000 });
        return false;
    }
    if (MiddleNameEn == "" || MiddleNameEn == undefined) {
        toastr.warning("Please Enter Middle Name En", { timeOut: 5000 });
        return false;
    }
    if (LastNameEn == "" || LastNameEn == undefined) {
        toastr.warning("Please Enter Last Name En", { timeOut: 5000 });
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
        // UserName: FullName,
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
            userId = data.UserId;
            ContractNumber = data.ContractNumber;
            $("#UserId").val(data.UserId);
            $("#Number").val(data.ContractNumber);
            debugger;
            SendOTP(data.UserId, data.ContractNumber);
            Reset();
          
            //location.href = '/Account/Login';
        },
        error: function (data) {
            var response = data.responseText.replace(/"/g, '');
            toastr.error(response, { timeOut: 5000 });
            HideModalLoader();

        }
    });
}
function SendOTP(UserId, Mobile) {




    $.ajax({
        url: '/api/User/SendOTP?UserId=' + UserId + '&Mobile=' + Mobile,
        type: 'POST',
        contentType: "application/json;charset=utf-8",
        success: function (data) {

            toastr.success("Send OTP successfully", { timeOut: 5000 });
            $('#OTPConfirm_Modal').modal('show');
           
        },
        error: function (data) {
            var response = data.responseText.replace(/"/g, '');
            toastr.error(response, { timeOut: 5000 });
            HideModalLoader();

        }
    });
}
function VerifyOTP() {
    var UserId = $("#UserId").val();
    var Number = $("#Number").val();
    var OTP = $("#OTP").val();
    $.ajax({
        url: '/api/User/VerifyOtp?UserId=' + UserId + '&Number=' + Number + '&OTP=' + OTP,
        type: 'POST',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            toastr.success("Verified successfully", { timeOut: 5000 });
            $('#OTPConfirm_Modal').modal('hide');
            location.href = '/Account/Customer';

        },
        error: function (data) {
            var response = data.responseText.replace(/"/g, '');
            toastr.error(response, { timeOut: 5000 });
            HideModalLoader();

        }
    });
}
function Reset() {

    $("#UserName").val('');
    $("#Password").val('');
    $("#Mobile").val('');
    $("#Dob").val('');
    $("#Email").val('');
    $("#FirstNameAr").val('');
    $("#MiddleNameAr").val('');
    $("#LastNameAr").val('');
    $("#FirstNameEn").val('');
    $("#MiddleNameEn").val('');
    $("#LastNameEn").val('');
    $("#Language").val('');
    $("#Gender").val('');
    $("#CivilId").val('');
    $("#TitleId").val('');

}