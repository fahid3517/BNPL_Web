function validate(id) {
    var form = document.getElementById(id); 
    var inputs, divs, i, textarea; 
    inputs = $(`#${id} input:not([type=hidden])`);
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
                $("span[data-for='"+inputId+"']").show();
                return false;
            }
            else if (classes.includes('defaultAutoComplete'))
            {
                $('.k-invalid-msg').text('Required*');
                $("span[data-for='" + inputId + "']").show();
                return false;
            } 
            else
            {
                $("#required-error").remove();
                $(em).append('<div id="required-error" class="invalid-feedback">Required*</div>');
                return false;
            }
        }
        

        if (pattern != null && pattern != "")
        {
            var re = RegExp(pattern);
            if (!re.test(value))
            {
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
function IsDateGreater(DateA, DateB) {
    var a = new Date(DateA);
    var b = new Date(DateB);

    var msDateA = Date.UTC(a.getFullYear(), a.getMonth() + 1, a.getDate());
    var msDateB = Date.UTC(b.getFullYear(), b.getMonth() + 1, b.getDate());


    if (parseFloat(msDateA) > parseFloat(msDateB))
        return 1;
    else
        0;
};
function IsDateSmaller(DateA, DateB) {
    var a = new Date(DateA);
    var b = new Date(DateB);

    var msDateA = Date.UTC(a.getFullYear(), a.getMonth() + 1, a.getDate());
    var msDateB = Date.UTC(b.getFullYear(), b.getMonth() + 1, b.getDate());

    if (parseFloat(msDateA) < parseFloat(msDateB))
        return 1;
    else
        0;
}
function DateOfBirthCheck(id) {
    var Error = id + "Error";
    var FieldInput = $("#" + id).val();
    var ErrorField = document.getElementById(Error);
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    var CurrentDate = yyyy + '-' + mm + '-' + dd;
    if (IsDateGreater(FieldInput, CurrentDate)) {
        ErrorField.innerHTML = "Date should be less than current date.";
    }
    else {
        ErrorField.innerHTML = "";
    }
}
$(document).ready(function () {
    $(document).on("focus", "textarea", function () {
        $(this).removeClass("is-invalid");
        $("#required-error").remove();
        $('#error').html('');
        $("#error").hide();
    });
    $(document).on("focus", "input", function () {
        $("input").removeClass("is-invalid");
        $("#required-error").remove();
        $('#error').html('');
        $("#error").hide();
    });
    $(".defaultDropdown").change(function () {
        $(".k-invalid-msg").hide();
    });
    $(".defaultAutoComplete").select(function () {
        $(".k-invalid-msg").hide();
    });
});


















