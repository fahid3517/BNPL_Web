function validate(id) {
    var form = document.getElementById(id);
    var inputs, divs, i, textarea;
    inputs = $('#form input:not([type=hidden])');
    textarea = form.getElementsByTagName("textarea");
    for (i = 0; i < inputs.length; i++) {
        var required = inputs[i].required;
        var value = inputs[i].value;
        var inputId = inputs[i].id;
        var pattern = inputs[i].pattern;
        if (required == true && (value == "" || value == null)) {
            $("#" + inputId).addClass("is-invalid");
            var em = inputs[i].closest('div');
            var classes = $(em).attr('class');
            //if (classes == undefined) {
            //    em = divs[i];
            //    classes = $(em).attr('class');
            //}
            if (classes.includes('defaultDropdown')) {
                $('.k-invalid-msg').text('Required*');
                $("span[data-for='" + inputId + "']").show();
                return false;
            }
            else if (classes.includes('defaultAutoComplete')) {
                $('.k-invalid-msg').text('Required*');
                $("span[data-for='" + inputId + "']").show();
                return false;
            }
            else {
                $("#required-error").remove();
                $(em).append('<div id="required-error" class="invalid-feedback">Required*</div>');
                return false;
            }
        }


        if (pattern != null && pattern != "") {
            var re = RegExp(pattern);
            if (!re.test(value)) {
                $("#" + inputId).addClass("is-invalid");
                var em = inputs[i].closest('div');
                var classes = $(em).attr('class');
                $("#required-error").remove();
                $(em).append('<div id="required-error" class="invalid-feedback">Please enter the valid format</div>');
                return false;
            }
        }
    }
    if (textarea.length > 0) {
        for (j = 0; j < textarea.length; j++) {
            var required = textarea[j].required;
            var value = textarea[j].value;
            var textareaId = textarea[j].id;
            var textaeadiv = textarea[j].parentElement;
            if (required == true && (value == "" || value == null)) {
                $("#" + textareaId).addClass("is-invalid");
                $("#required-error").remove();
                $(textaeadiv).append('<div id="required-error" class="invalid-feedback">Required*</div>');
                return false;
            }
        }
    }
    return true;
}

function AddEmployee() {
    var formId = "form";
    if (validate(formId)) {
        debugger
        var Employeedetails = {
            Firstname: $('#firstname').val(),
            lastname: $('#lastname').val(),
            Email: $('#emailaddress').val(),
            Username: $("#username").val(),
            EmployeeCode: $("#code").val()
        };

        $.ajax({

            url: '../api/Employee/Post',
            type: 'POST',
            data: JSON.stringify(Employeedetails),
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                window.alert("Employee Successfully Registered")


            },
            error: function (resopnse) {

                window.alert("Something Happened Unexpected")

            }
        })



    }
}