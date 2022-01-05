BasicContinue = 0;
function CheckBasicContinue() {
    BasicContinue = 1;
    AddBasicForm();
    
}
$(document).ready(function () {
    
   
    $('#Mobile').inputmask('+99-999-9999999');
    $('#CNIC').inputmask('99999-9999999-9');
})

function AddBasicForm() {
    $("#form-1").validate();
    var formId = "form-1";
    var DobError = document.getElementById("DobError");
    if (validate(formId)) {
        debugger
        var Employeedetails = {
            Firstname: $('#FirstName').val(),
            // EmpImage: $('#fileupload').val(),
            Fathername: $('#FhName').val(),
            DateOfBirth: $('#Dob').val(),
            Gender: $("#Gender").val(),
            CNIC: $("#CNIC").val(),
            Mobile: $("#Mobile").val(),
            CNICIssue: $('#CNICIssue').val(),
            CNICExpiry: $('#CNICExpiry').val(),
            EOBI: $('#EOBI').val(),
            Location: $('#Location').val(),
            Department: $("#Department").val(),
            SubDepartment: $("#SubDepartment").val(),
            Designation: $('#Designation').val(),
            JoinDate: $('#JoinDate').val(),
            EmploymentType: $('#EmploymentType').val(),
        };

        $.ajax({

            url: '../api/Employee/EnrollEmployee',
            type: 'POST',
            data: JSON.stringify(Employeedetails),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                toastr.success("Employee Successfully Registered", { timeOut: 5000 });
                ResetForm();
                EnableAllTabs();
                if (BasicContinue === 1) {
                    $('#loc-enab').click();
                }
                BasicContinue = 0;


            },
            error: function () {
                var response = data.responseText.replace(/"/g, '');
                toastr.error(response, { timeOut: 5000 });
            }
        })



    }
}

function CheckBasicContinue() {
    BasicContinue = 1;
    AddBasicForm();
}

function ResetForm()
{
   // $('#form-1')[0].reset();
    document.getElementById("form-1").reset();
}