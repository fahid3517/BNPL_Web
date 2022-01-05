var EmployeeCode = "";
var EmployeeId = 0;

//function InitializeForm() {
//    InitializeBasicForm();
//    InitializeQualificationForm();
//    InitializeBank();
//    /*InitializeSalaryForm();*/
//}

//function PopulateData(id) {
//    debugger;
//    EmployeeId = id;
//    ShowLoader();
//    $.ajax({
//        url: "../api/Employee/GetById?EmployeeId=" + id,
//        type: 'GET',
//        contentType: "application/json;charset=utf-8",
//        success: function (data) {
//            PopulateBasicForm(data);
//            PopulateLocationForm(data);
//            PopulateQualificationForm(data);
//            PopulateExperienceForm(data);
//            PopulateBankForm(data);
//            PopulateCredentials(data);
//            PopulateNominees(data);
//            PopulateManagerForm(data);
//            //PopulateSalaryForm(data);
//            EnableAllTabs();
//            HideLoader();
//        },
//        error: function (data) {
//            var response = data.responseText.replace(/"/g, '');
//            toastr.error(response, { timeOut: 5000 });

//            HideLoader();
//        }
//    });
//}

function EnableAllTabs() {
    $("#basic-enab").removeClass("disabled");
    $("#loc-enab").removeClass("disabled");
    $("#qual-enab").removeClass("disabled");
    $("#exp-enab").removeClass("disabled");
    $("#bank-enab").removeClass("disabled");
    $("#credential-enab").removeClass("disabled");
    $("#nominee-enab").removeClass("disabled");
    $("#manager-enab").removeClass("disabled");
    /* $("#salary-enab").removeClass("disabled");*/
}

















