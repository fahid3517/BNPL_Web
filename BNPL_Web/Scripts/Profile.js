$(function () {
    populateData();
});


function populateData() {


    ShowLoader();

    $.ajax({
        
        url: "../api/Employee/GetEmployeeDetail",
        contentType: "application/json;charset=utf-8",
        type: 'GET',
       
        success: function (data) {
            console.log(data);

            debugger
            if (data.ImageId == null)
                $('#Img').attr("src", "../Content/assets/img/user.jpg");
            else {
                $('#Img').attr("src", "../ImagesStorageDB/" + data.ImageId);
            }

           

            $('#Name').html(data.Name);
            $('#Class').html(data.RoleName);
            $('#Designer').html(data.DepartmentName);
            $('#Id').html(data.Code);
            $('#Date').html(moment(data.JoiningDate).format('DD/MM/YYYY'));
            $('#Contact').html(data.Mobile);
          
            $('#Email').html(data.Email);
            $('#Address').html(data.PresentAddress);
            $('#Birthday').html(moment(data.DateofBirth).format('DD/MM/YYYY'));

         //   $('#Gender').html(data.Gender);
            if (data.Gender == "M") {
                $('#Gender').html("Male")
            }
            else if (data.Gender == "F") {
                $('#Gender').html("Female")
            }
            else { $('#Gender').html("Other") }

           

           
            $('#Tel').html(data.Telephone); 

            $('#Natn').html(data.CountryName);

            if (data.Gender == "M") {
                $('#Gender').html("Male")
            }
            else if (data.Gender == "F") {
                $('#Gender').html("Female")
            }
          
           
        //    $('#Religion').html(data.Religion);
            if (data.Religion == "M") {
                $('#Religion').html("Muslim")
            }

            else { $('#Religion').html("Non Muslim") }
            
          //  $('#Status').html(data.Marital);

            if (data.Marital == "M") {
                $('#Status').html("Married")
            }
            else if (data.Marital == "S") {
                $('#Status').html("Single")
            }
            else if (data.Marital == "D")
            {
                $('#Status').html("Divorced")
            }

             else { $('#Status').html("Widowed") }



            $('#FatherName').html(data.FatherName);

            

            $('#Phone').html(data.EmergencyContact);

            
            $('#Bank').html(data.BankBranchname);
            $('#account').html(data.AccountNumber);
            $('#IBM').html(data.IBAN);
        

            $('#FT').html(data.FatherName);
            $('#RLN').html(data.ReferenceEmpCode);
            $('#DTE').html(moment(data.DateofBirth).format('DD/MM/YYYY'));
            $('#PHE').html(data.EmergencyContact);

            for (var i = 1; i <= data.Education.length; i++) {
                
                $('#experience').append('<li>'+
                    '<div class="experience-user">' +
                    '<div class="before-circle"></div>' +
                    '</div>' +
                    '<div class="experience-content">' +
                    '<div class="timeline-content">' +
                    '<a href="#/"  class="name">' + data.Education[i - 1].Institute + ' </a>' +
                    '<div >' + data.Education[i - 1].Qualification + ' </div>' +
                    '<span class="time">' + data.Education[i - 1].YearofPassing + ' </span>' +
                           '</div>'+
                            '</div>'+
                        '</li>'
                );

                }
                for (var i = 1; i <= data.Experience.length; i++) {

                    $('#Accept').append('<li>' +
                        '<div class="experience-user">' +
                        '<div class="before-circle"></div>' +
                        '</div>' +
                        '<div class="experience-content">' +
                        '<div class="timeline-content">' +
                        '<a href="#/"  class="name">' + data.Experience[i - 1].JobTitle + ' at ' + data.Experience[i - 1].EmployerName +' </a>' +
                        '<div >' + data.Experience[i - 1].JobFrom + '  -  ' + data.Experience[i - 1].JobTo  +' </div>' +                      
                        '</div>' +
                        '</div>' +
                        '</li>'
                    );




           
            }

       
            HideLoader();
        },
        error: function (dataError) {
            console.log(dataError);
        }
    });
};
