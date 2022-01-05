var JobId = 0;
$(document).ready(function () {
    debugger;
    AppendJob();
    // Append Job Detail 

    $("#jobOpenings").on('mouseover', function () {
        $('.single-job').on('click', function () {
            debugger;
            $('#jobOpeningsDetail').empty();
            let id = this.id;
            JobId = id;
            $.ajax({
                url: '../api/JobAdvertisement/GetById?id=' + id,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    debugger;
                    let row = `<div class="col-md-11 card mb-0 ml-4 mt-3" style="border-right:3px solid #1e73bf ">
                <div class="card-body height">
                    <div class="row">
                    <div class="col-md-12">
                        <h3 class="page-title m-t-0 mb-0" style="color:#1e73bf">${data.Name}</h3>
                        <div class="row">
                            <div class="col-md-4">
                                <i class="fa fa-money fa-sm" aria-hidden="true"></i>
                                <small style="font-size:16px !important">Salary: Rs.${data.FromSalary} - Rs.${data.ToSalary}/Month</small>
                            </div>
                            <div class="col-md-8 text-right">
                                <button class="btn btn-lg btn-primary" type="button" onclick="showModal()">Apply</button>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12">
                             <strong style="font-size:16px; color:#1e73bf">Job Description</strong></br>
                             <small style="font-size:16px">${data.JobDescription}</small>
                            </div>
                        </div>
                    </div>
                </div></div></div>`;
                    $('#jobOpeningsDetail').html(row);


                    HideLoader();
                },
                error: function (data) {
                    var response = data.responseText.replace(/"/g, '');
                    toastr.error(response, { timeOut: 5000 });

                    HideLoader();
                }
            });
        });
    });
    $('#mobile').mask('+99-9999999999');
    $('#CNIC').mask('99999-9999999-9');
});

function AppendJob() {
    debugger;
    ShowLoader();
    $.ajax({
        url: '../api/JobAdvertisement/GetActiveJobAdvertisements',
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.length > 0) {

                $.each(data, function (i, jobOpening) {

                    let row = `<div class="col-md-12 card mb-0 ml-4 pb-3 mt-3" style="border-left:3px solid #1e73bf">
                    <div class="card-body single-job" id="${jobOpening.Id}" >
                     <div class="row">
                        <div class="col-md-12">
                         <h3 class="page-title m-t-0 mb-1" style="color:#1e73bf">${jobOpening.Name}</h3>
                        <div class="row">
                         <div class="col-md-6">
                            <i class="fa fa-money fa-sm" aria-hidden="true"></i>
                            <small style="font-size:14px !important">Salary: Rs.${jobOpening.FromSalary} - Rs.${jobOpening.ToSalary}/Month</small>
                         </div>
                        <div class="col-md-6">
                            <i class="fa fa-user-o fa-sm" aria-hidden="true"></i>
                            <small style="font-size:14px !important">Experience: ${jobOpening.MinimumExperience} - ${jobOpening.MaximumExperience}</small>
                        </div>
                   </div>
                    <div class="row">
                        <div class="col-md-6">
                            <i class="fa fa-users fa-sm" aria-hidden="true"></i>
                            <small style="font-size:14px !important">Total Positions: ${jobOpening.TotalPosition} </small>
                        </div>
                        <div class="col-md-6">
                            <i class="fa fa-calendar fa-sm" aria-hidden="true"></i>
                            <small style="font-size:14px !important">${jobOpening.ApplyDateString}</small>
                        
                        </div>
                    </div>
                    <hr>
                    <div class="row mt-3">
                        <div class="col-md-12">
                            <strong style="color:#1e73bf">Job Description</strong></br>
                            <small style="font-size:14px !important">${jobOpening.JobDescription.substr(0, 250)}...</small>
                        </div> 
                    </div>
                </div>
            </div></div></div>`;
                    $('#jobOpenings').append(row);

                });
            }
            else {
                $('#jobOpenings').append('<p style="font-size:30px;" class="ml-5">No Current Opening</p>');
            }
            HideLoader();
        },
        error: function (data) {
            var response = data.responseText.replace(/"/g, '');
            toastr.error(response, { timeOut: 5000 });

            HideLoader();
        }
    });

}

function showModal() {
    $('#add_custom_policy').modal("toggle");
}

function fileInputChange() {
    if (document.getElementById("File").files.length === 0) {
        $('#lblFile').addClass('text-danger').text("Please select a file").attr('fileselected', false);
        return false;
    }
    let fileName = document.getElementById("File").files[0].name;
    $('#lblFile').removeClass('text-danger').text(fileName).attr('fileselected', true);
}

function AddApplication() {
    var formId = "formModal";
    if (validate(formId)) {
        let formData = new FormData();
        if ($('#lblFile').attr('fileselected') !== "true") {
            $('#lblFile').addClass('text-danger').text('Please select a file');
            return false;
        }
        
        var file = document.getElementById("File").files[0];
        formData.append("File", file);
        formData.append("JobId", JobId);
        formData.append("Name", $('#Name').val());
        formData.append("CNIC", $('#CNIC').val());
        formData.append("Email", $('#Email').val());
        formData.append("ContactNo", $('#mobile').val());
        ShowLoader();
        
        $.ajax({
            type: "POST",
            url: '../api/JobApplication/Post',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (data) {
                toastr.success("Job Application has been added successfully.", { timeOut: 10000 });
                ResetForm();
                HideLoader();
            },
            error: function (data) {
                var response = data.responseText.replace(/"/g, '');
                toastr.error(response, { timeOut: 5000 });

                HideLoader();
            }
            
        });
    }
}

function ValidateEmail() {
    var email = document.getElementById("Email").value;
    var emailError = document.getElementById("emailError");
    emailError.innerHTML = "";
    var expr = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (!expr.test(email)) {
        emailError.innerHTML = "Invalid email address.";
    }

    if (email === "") {
        emailError.innerHTML = "";
    }

}

function ResetForm() {
    $('#Name').val("");
    $('#CNIC').val("");
    $('#Email').val("");
    $('#mobile').val("");
    $('#add_custom_policy').modal("hide");
}






