var dataUrl = null;
var tableId = null;

function RenderHomePage() {
    window.open("../Home/Index", "_self");
}


function GetPromise(apiUrl) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: apiUrl,
            method: 'GET'
        }).done((response) => {
            //this means my api call suceeded, so I will call resolve on the response
            resolve(response);
        }).fail((error) => {
            //this means the api call failed, so I will call reject on the error
            reject(error);
        });
    });
};

function GetUserImage() {
    $.ajax({
        url: "../api/Employee/GetEmployeeImage",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data === "")
                $('#user-image').attr("src", "../Content/assets/img/profiles/avatar-21.jpg");
            else
                $('#user-image').attr("src", "../ImagesStorageDB/" + data);
        },

        error: function (data) {
            var response = data.responseText.replace(/"/g, '');
            console.log(response);
        }
    });


};
function GetNotifications() {

    $.ajax({
        url: "../api/Notification/GetNotification",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        success: function (data) {

            if (data.Notifications.length >= 1) {

                for (var i = 0; i < data.Notifications.length; i++) {

                    $('#notificationBar').append('<li class="notification-message">' +
                        '<a href="../NotificationManagement/Index">' +
                        '<div class="media">' +
                        '<div class="media-body">' +
                        '<p class="text-dark"><span></span><b>' + data.Notifications[i].Name + ': </b><span class="noti-title">' + data.Notifications[i].Description + '</span></p>' +
                        '<p class="noti-time"><span class="notification-time">' + data.Notifications[i].TotalTime + '</span></p>' +
                        '</div>' +
                        '</div>' +
                        '</a>' +
                        '</li>');


                }
            }      
            $("#notificationCount").text(data.Count)

            HideLoader();
        },

        error: function (data) {
            var response = data.responseText.replace(/"/g, '');
            $("#success").hide();
            $("#error").html(response);
            $("#error").show();
            HideLoader();
        }
    });


};




function LoadKendoDropDown(id, placeholder, url) {
    var promiseObject = GetPromise(url);
    promiseObject
        .then(data =>
            SetKendoDDSource(id, placeholder, data))
        .catch(error =>
            console.log("KENDO DROPDOWN ERROR" + "/r/n" + "Url:" + url + "/r/n" + "htmlElementId:" + id + "/r/n" + "Error Detail" + error));
};

function LoadKendoMultiselect(id, placeholder, url) {
    var promiseObject = GetPromise(url);
    promiseObject
        .then(data =>
            SetKendoMultiSource(id, placeholder, data))
        .catch(error =>
            console.log("KENDO MULTISELECT ERROR" + "/r/n" + "Url:" + url + "/r/n" + "htmlElementId:" + id + "/r/n" + "Error Detail" + error));
};


function LoadKendoAutoComplete(id, placeholder, url) {
    var promiseObject = GetPromise(url);
    promiseObject
        .then(data =>
            SetKendoAutoCompleteSource(id, placeholder, data))
        .catch(error =>
            console.log("KENDO AUTO COMPLETE ERROR" + "/r/n" + "Url:" + url + "/r/n" + "htmlElementId:" + id + "/r/n" + "Error Detail" + error));
};

function SetKendoDDSource(id, placeholder, data) {
    $("#" + id).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        optionLabel: placeholder,
        dataSource: data,
        filter: "contains",
        height: 200
    });
};

function SetKendoDDSourceValue(id, placeholder, data, value) {
    $("#" + id).kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        optionLabel: placeholder,
        dataSource: data,
        height: 100
    });

    if (value != null) {
        $("#" + id).val(value);
    }
};

function SetKendoMultiSource(id, placeholder, data) {
    $("#" + id).kendoMultiSelect({
        dataTextField: "Name",
        dataValueField: "Id",
        optionLabel: placeholder,
        placeholder: placeholder,
        dataSource: data,
        height: 100
    });
};

function SetKendoAutoCompleteSource(id, placeholder, data) {
    $("#" + id).kendoAutoComplete({
        dataTextField: "Name",
        dataValueField: "Id",
        suggest: true,
        dataSource: data,
        filter: "contains",
        placeholder: placeholder,
        select: onSelectRecord,
        change: OnChangeRecord
    });
};
function onSelectRecord(e) {

    var inputId = this.element[0].id;
    var dataItem = this.dataItem(e.item.index());

    $("#" + inputId + "Id").val(dataItem.Id);
    $("#" + inputId + "NameOnSelect").val(dataItem.Name);
}

function OnChangeRecord(e) {

    var inputId = this.element[0].id;
    var newName = $("#" + inputId).val();
    var oldName = $("#" + inputId + "NameOnSelect").val();
    if (newName != oldName) {
        $("#" + inputId + "Id").val("");
    }
}


function InitializeKendoDatePicker(id, disableFutureDates, disablePastDates) {
    $("#" + id).kendoDatePicker({
        value: new Date(),
        disableDates: function (date) {
            if (disableFutureDates) {
                return date > new Date();
            }
            else if (disablePastDates) {
                var oldDate = new Date();
                oldDate.setDate(oldDate.getDate() - 1);
                return date < oldDate;
            }
        }
    });
};

function LoadDropDown(id, url, value, placeholder) {
    var promiseObject = GetPromise(url);
    promiseObject
        .then(data =>
            SetKendoDDSourceValue(id, placeholder, data, value))
        .catch(error => console.log("DROPDOWN ERROR" + "/r/n" + "Url:" + url + "/r/n" + "htmlElementId:" + id + "/r/n" + "Error Detail" + error));
};




function UnselectKendoDropDown(id) {
    var ddList = $('#' + id).data("kendoDropDownList");
    if (ddList != undefined) {
        ddList.text(ddList.options.optionLabel);
        ddList.element.val(-1);
        ddList.selectedIndex = -1;
        ddList._oldIndex = 0;
    }
};

function ClearKendoDropDownDataSource(id) {
    UnselectKendoDropDown(id);
    var ddList = $('#' + id).data("kendoDropDownList");
    if (ddList != undefined) {
        ddList.dataSource.data([]);
    }
};

function GetgridValidFlagDescription(ValidFlag, rowId, dtCommonParam) {
    var flagStatus, styleClasses;
    if (ValidFlag) {
        flagStatus = " Active"
        styleClasses = "fa fa-dot-circle-o text-success"
    }
    else {
        flagStatus = " Inactive"
        styleClasses = "fa " + "fa-dot-circle-o " + "text-danger";
    }
    var actionHtml = '<div class="dropdown action-label">' +
        '<a class="btn btn-white btn-sm btn-rounded dropdown-toggle" href="#" data-toggle="dropdown" aria-expanded="false">' +
        '<i class=' + '"' + styleClasses + '"' + '></i>' + flagStatus +
        '</a>' +
        '<div class="dropdown-menu dropdown-menu-right">' +
        '<a class="dropdown-item" href="#" onclick ="ShowModal(' + "'" + dtCommonParam.activateUrl + rowId + "'," + "'" + dtCommonParam.tableId + "'," + "'" + "active'" + ')"' + '><i class="fa fa-dot-circle-o text-success"></i> active</a>' +
        '<a class="dropdown-item" href="#" onclick ="ShowModal(' + "'" + dtCommonParam.deActivateUrl + rowId + "'," + "'" + dtCommonParam.tableId + "'," + "'" + "inactive'" + ')"><i class="fa fa-dot-circle-o text-danger"></i> Inactive</a>' +
        '</div>' +
        '</div>';
    return actionHtml;
};

function RedirectToUrl(url) {
    location.href = url;
};

function ReloadDataTable(id) {
    $('#' + id).DataTable().ajax.reload();
};
function toggleDropdown(e, rowId) {
    e.preventDefault();
    $(`#dropdown${rowId}`).toggleClass("show");
}
function ActivateRecord() {
    //ShowModal("inactive")
    //var ans = confirm("Are you sure you want to Activate this record?");
    $("#active").modal("hide");
    var ans = true;
    if (ans) {
        //ShowLoader();
        $.ajax({
            url: dataUrl,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            //dataType: "json",
            data: "{}",
            success: function (result) {
                //HideLoader();
                ReloadDataTable(tableId);
            },
            error: function (errormessage) {
                //HideLoader();
                toastr.warning(errormessage.responseText, { timeout: 5000 });
            }
        });

    }
};

function DeactivateRecord() {
    //var ans = confirm("Are you sure you want to Deactivate this record?");
    //ShowModal("active")
    $("#inactive").modal("hide");
    var ans = true;
    if (ans) {
        //ShowLoader();
        $.ajax({
            url: dataUrl,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            // dataType: "json",
            data: "{}",
            success: function (result) {

                if (tableId != undefined && tableId != null) {
                    ReloadDataTable(tableId);
                }
                else {
                    FetchEventAndRenderCalendar();
                }
                HideLoader();
            },
            error: function (errormessage) {
                //HideLoader();
                toastr.warning(errormessage.responseText, { timeout: 5000 });
            }
        });

    }
};

function CancelById() {
    $("#cancelModel").modal("hide");
    var ans = true;
    if (ans) {
        //ShowLoader();
        $.ajax({
            url: dataUrl,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            //dataType: "json",
            data: "{}",
            success: function (result) {
                //HideLoader();
                ReloadDataTable(tableId);
            },
            error: function (errormessage) {
                //HideLoader();
                alert(errormessage.responseText);
            }
        });

    }
};

function RejectedRecord() {
    //ShowModal("inactive")
    //var ans = confirm("Are you sure you want to Activate this record?");
    $("#Rejected").modal("hide");
    var ans = true;
    if (ans) {
        //ShowLoader();
        $.ajax({
            url: dataUrl,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            //dataType: "json",
            data: "{}",
            success: function (result) {
                //HideLoader();
                ReloadDataTable(tableId);
            },
            error: function (errormessage) {
                //HideLoader();
                alert(errormessage.responseText);
            }
        });

    }
};

function ApprovedRecord() {
    //ShowModal("inactive")
    //var ans = confirm("Are you sure you want to Activate this record?");
    $("#Approved").modal("hide");
    var ans = true;
    if (ans) {
        //ShowLoader();
        $.ajax({
            url: dataUrl,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            //dataType: "json",
            data: "{}",
            success: function (result) {
                //HideLoader();
                ReloadDataTable(tableId);
            },
            error: function (errormessage) {
                //HideLoader();
                alert(errormessage.responseText);
            }
        });

    }
};

function CloseRecord() {
    //ShowModal("inactive")
    //var ans = confirm("Are you sure you want to Activate this record?");
    $("#Closed").modal("hide");
    var ans = true;
    if (ans) {
        //ShowLoader();
        $.ajax({
            url: dataUrl,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            //dataType: "json",
            data: "{}",
            success: function (result) {
                //HideLoader();
                ReloadDataTable(tableId);
            },
            error: function (errormessage) {
                //HideLoader();
                alert(errormessage.responseText);
            }
        });

    }
};


function GetAction(rowId, dtCommonParam, username) {
    var actionsHtml = '<div>' +
        (dtCommonParam.resetPasswordUrl ?
            ('<a target="_blank" href=" ' + dtCommonParam.resetPasswordUrl + username + ' " class="text-primary mr-2">' +
                '<span class="waves-light"><li class="ace-icon fa fa-undo bigger-120" title="Reset Password" style="margin-right: 5px;"></li></span>' +
                '</a >') : '') +
        (dtCommonParam.updateUrl ?
            ('<a href=" ' + dtCommonParam.updateUrl + rowId + ' " class="tooltip-success">' +
                '<span class="waves-light"><li class="ace-icon fa fa-pencil-square-o bigger-120" title="Edit" style="margin-right: 5px;"></li></span>' +
                '</a > ') : '') +

        '</div>';
    return actionsHtml;
}
//For Employee
function GetgridStatusDescription(Status, rowId, dtCommonParam) {
    var flagStatus, styleClasses, actionHtml;
    if (Status == "Pending") {
        flagStatus = "Pending";
        styleClasses = "fa fa-dot-circle-o text-info";

        actionHtml = '<div class="dropdown action-label">' +
            '<a class="btn btn-white btn-sm btn-rounded dropdown-toggle" href="#" data-toggle="dropdown" aria-expanded="false">' +
            '<i class=' + '"' + styleClasses + '"' + '></i>' + flagStatus +
            '</a>' +
            '<div class="dropdown-menu dropdown-menu-right">' +
            '<a class="dropdown-item" href="#" onclick ="ShowCancelModel(' + "'" + dtCommonParam.cancelUrl + rowId + "'," + "'" + dtCommonParam.tableId + "'" + ')"><i class="fa fa-dot-circle-o text-danger"></i> Cancel</a>' +
            '</div>' +
            '</div>';
        return actionHtml;
    }
    else if (Status == "New") {
        flagStatus = " New";
        styleClasses = "fa fa-dot-circle-o text-purple";

        actionHtml = '<div class="dropdown action-label">' +
            '<a class="btn btn-white btn-sm btn-rounded dropdown-toggle" href="#" data-toggle="dropdown" aria-expanded="false">' +
            '<i class=' + '"' + styleClasses + '"' + '></i>' + flagStatus +
            '</a>' +
            '<div class="dropdown-menu dropdown-menu-right">' +
            '<a class="dropdown-item" href="#" onclick ="ShowCancelModel(' + "'" + dtCommonParam.cancelUrl + rowId + "'," + "'" + dtCommonParam.tableId + "'" + ')"><i class="fa fa-dot-circle-o text-danger"></i> Cancel</a>' +
            '</div>' +
            '</div>';
        return actionHtml;
    }
    else if (Status == "Submited") {
        flagStatus = " Submited";
        styleClasses = "fa fa-dot-circle-o text-purple";

        actionHtml = '<div class="dropdown action-label">' +
            '<a class="btn btn-white btn-sm btn-rounded dropdown-toggle" href="#" data-toggle="dropdown" aria-expanded="false">' +
            '<i class=' + '"' + styleClasses + '"' + '></i>' + flagStatus +
            '</a>' +
            '<div class="dropdown-menu dropdown-menu-right">' +
            '<a class="dropdown-item" href="#" onclick ="ShowCancelModel(' + "'" + dtCommonParam.cancelUrl + rowId + "'," + "'" + dtCommonParam.tableId + "'" + ')"><i class="fa fa-dot-circle-o text-danger"></i> Cancel</a>' +
            '</div>' +
            '</div>';
        return actionHtml;
    }
    else if (Status == "Inprocess") {
        flagStatus = " In Process";
        styleClasses = "fa fa-dot-circle-o text-info";

        actionHtml = '<div class="dropdown action-label">' +
            '<a class="btn btn-white btn-sm btn-rounded dropdown-toggle" href="#" data-toggle="dropdown" aria-expanded="false">' +
            '<i class=' + '"' + styleClasses + '"' + '></i>' + flagStatus +
            '</a>' +
            '<div class="dropdown-menu dropdown-menu-right">' +
            '<a class="dropdown-item" href="#" onclick ="ShowCloseModel(' + "'" + dtCommonParam.closeUrl + rowId + "'," + "'" + dtCommonParam.tableId + "'" + ')"><i class="fa fa-dot-circle-o text-success"></i> Close</a>' +
            '<a class="dropdown-item" href="#" onclick ="ShowCancelModel(' + "'" + dtCommonParam.cancelUrl + rowId + "'," + "'" + dtCommonParam.tableId + "'" + ')"><i class="fa fa-dot-circle-o text-danger"></i> Cancel</a>' +
            '</div>' +
            '</div>';
        return actionHtml;
    }
    else if (Status == "Closed") {
        flagStatus = " Closed"
        styleClasses = "fa fa-dot-circle-o text-success";

        actionHtml = '<div class="dropdown action-label">' +
            '<a class="btn btn-white btn-sm btn-rounded dropdown-toggle" href="#" data-toggle="dropdown" aria-expanded="false">' +
            '<i class=' + '"' + styleClasses + '"' + '></i>' + flagStatus +
            '</a>' +
            '</div>';
        return actionHtml;
    }
    else if (Status == "Approved") {
        flagStatus = " Approved"
        styleClasses = "fa fa-dot-circle-o text-success";

        actionHtml = '<div class="dropdown action-label">' +
            '<a class="btn btn-white btn-sm btn-rounded" href="#" data-toggle="dropdown" aria-expanded="false">' +
            '<i class=' + '"' + styleClasses + '"' + '></i>' + flagStatus +
            '</a>' +
            '</div>';
        return actionHtml;
    }
    else if (Status == "Rejected") {
        flagStatus = " Rejected";
        styleClasses = "fa fa-dot-circle-o text-danger";

        actionHtml = '<div class="dropdown action-label">' +
            '<a class="btn btn-white btn-sm btn-rounded" href="#" data-toggle="dropdown" aria-expanded="false">' +
            '<i class=' + '"' + styleClasses + '"' + '></i>' + flagStatus +
            '</a>' +
            '</div>';
        return actionHtml
    }
    else {
        flagStatus = " Cancelled";
        styleClasses = "fa " + "fa-dot-circle-o " + "text-danger";

        actionHtml = '<div class="dropdown action-label">' +
            '<a class="btn btn-white btn-sm btn-rounded dropdown-toggle" href="#" data-toggle="dropdown" aria-expanded="false">' +
            '<i class=' + '"' + styleClasses + '"' + '></i>' + flagStatus +
            '</a>' +
            '</div>';
        return actionHtml;
    }
}

//For Manager
function GetManagerAction(rowId, dtCommonParam) {

    var actionHtml = '<div class="dropdown dropdown-action">' +
        '<a href="#" class="action-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>' +
        '<div class="dropdown-menu dropdown-menu-right">' +
        '<a class="dropdown-item" href="#" onclick ="ShowApprovedModal(' + "'" + dtCommonParam.approvedUrl + rowId + "'," + "'" + dtCommonParam.tableId + "'" + ')"' + '><i class="fa fa-dot-circle-o text-success"></i> Approve</a>' +
        '<a class="dropdown-item" href="#" onclick ="ShowRejectedModel(' + "'" + dtCommonParam.rejectedUrl + rowId + "'," + "'" + dtCommonParam.tableId + "'" + ')"><i class="fa fa-dot-circle-o text-danger"></i> Reject</a>' +
        '</div>' +
        '</div>';
    return actionHtml;

}

function GetgridButton(rowId, ValidFlag, dtCommonParam, refNumber) {
    if (ValidFlag) {
        var actionsHtml = '<div>' +

            (dtCommonParam.updateUrl ?
                ('<a href=" ' + dtCommonParam.updateUrl + rowId + ' " class="tooltip-success">' +
                    '<span class="waves-light"><li class="ace-icon fa fa-pencil-square-o bigger-120" title="Edit" style="margin-right: 5px;"></li></span>' +
                    '</a > ') : '')
            +

            (dtCommonParam.approveUrl ?
                ('<button type="button" class="btn btn-primary waves-effect waves-light" style="float: none;margin: 5px;" onclick="RedirectToUrl(' + "'" + dtCommonParam.approveUrl + refNumber + "'" + ')">' +
                    '   ' +
                    '<span class="icofont icofont-tick-mark"></span>' +
                    '   '
                    +
                    '</button>') :
                '')
            +

            (dtCommonParam.approveOnScreen ?
                ('<button type="button" class="btn btn-primary waves-effect waves-light" style="float: none;margin: 5px;" data-toggle="modal" data-target="#OnScreenApprovalModal"' + ')">' +
                    '   ' +
                    '<span class="icofont icofont-tick-mark"></span>' +
                    '   '
                    +
                    '</button>') :
                '')
        //+

        //(dtCommonParam.deActivateUrl ?
        //    (
        //    '<a href="#"><li  class="fa fa-dot-circle-o text-danger" title="Inactive" onclick ="ShowModal(' + "'" + dtCommonParam.deActivateUrl + rowId + "'," + "'" + dtCommonParam.tableId + "'," + "'" + "inactive'" +')">' +
        //        '</li></a>') : '') +
        '</div>';
        return actionsHtml;
    }
    else {
        var actionsHtml = '<div>' +

            (dtCommonParam.updateUrl ?
                ('<a href=" ' + dtCommonParam.updateUrl + rowId + ' " class="tooltip-success">' +
                    '<span class="waves-light"><li class="ace-icon fa fa-pencil-square-o bigger-120" title="Edit" style="margin-right: 5px;"></li></span>' +
                    '</a > ') : '')
            +
            (dtCommonParam.approveUrl ?
                ('<button type="button" class="btn btn-primary waves-effect waves-light" style="float: none;margin: 5px;" onclick="RedirectToUrl(' + "'" + dtCommonParam.approveUrl + refNumber + "'" + ')">' +
                    '   ' +
                    '<span class="icofont icofont-tick-mark"></span>' +
                    '   '
                    +
                    '</button>') :
                '')
            +
            (dtCommonParam.approveOnScreen ?
                ('<button type="button" class="btn btn-primary waves-effect waves-light" style="float: none;margin: 5px;" data-toggle="modal" data-target="#OnScreenApprovalModal" onclick="' + dtCommonParam.approveOnScreenFunction + '(' + "'" + refNumber + "'" + ')">' +
                    '   ' +
                    '<span class="icofont icofont-tick-mark"></span>' +
                    '   '
                    +
                    '</button>') :
                '')
            +
            //(dtCommonParam.activateUrl ? (
            //'<a href="#"><li  class="fa fa-dot-circle-o text-success" title="Active" onclick ="ShowModal(' + "'" + dtCommonParam.activateUrl + rowId + "'," + "'" + dtCommonParam.tableId + "'," + "'" + "active'"+ ')">' +
            //'</li></a>') : '') +
            '</div>';
        return actionsHtml;
    }
};

function GetgridPriceButton(rowId, ValidFlag, dtCommonParam, refNumber) {
    if (ValidFlag) {
        var actionsHtml = '<div class="btn-group btn-group-sm" style="float: none;">' +
            '   ' +
            (dtCommonParam.updateUrl ?
                ('<button type="button" class="btn btn-primary waves-effect waves-light" style="float: none;margin: 5px;" onclick="UpdatePrice(' + "'" + rowId + "'" + ')">' +
                    '   ' +
                    '<span class="icofont icofont-ui-edit"></span>' +
                    '   '
                    +
                    '</button>') :
                '')
        return actionsHtml;
    }
};

function GetgridViewButton(rowId, ValidFlag, dtCommonParam) {
    if (ValidFlag) {
        var actionsHtml = '<div class="btn-group btn-group-sm" style="float: none;">' +
            '   ' +
            (dtCommonParam.updateUrl ?
                ('<button type="button" class="btn btn-primary waves-effect waves-light" style="float: none;margin: 5px;" onclick="RedirectToUrl(' + "'" + dtCommonParam.updateUrl + rowId + "'" + ')">' +
                    '   ' +
                    '<span class="icofont icofont-ui-edit"></span>' +
                    '   '
                    +
                    '</button>') :
                '')
        return actionsHtml;
    }
};

function GetPrintButton(refNumber, ValidFlag, dtCommonParam, printFunc) {
    var actionsHtml = '<div class="btn-group btn-group-sm" style="float: none;">' +
        '   ' +
        '<button type="button" class="btn btn-primary waves-effect waves-light" style="float: none;margin: 5px;" onclick=" ' + printFunc + ' (' + "'" + refNumber + "'" + ')">' +
        '   ' +
        '<span class="fa fa-print"></span>' +
        '   '
        +
        '</button>' +
        '</div>';
    return actionsHtml;
}

function GetButtonForAdjustment(refNumber, ValidFlag, dtCommonParam, printFunc) {
    var actionsHtml = '<div class="btn-group btn-group-sm" style="float: none;">' +
        '   ' +
        '<button type="button" class="btn btn-primary waves-effect waves-light" style="float: none;margin: 5px;" onclick=" ' + printFunc + ' (' + "'" + refNumber + "'" + ')">' +
        '   ' +
        '<span class="fa fa-print"></span>' +
        '   '
        +
        '</button>' +
        '</div>';
    return actionsHtml;
}


function ShowModal(url, id, status) {
    dataUrl = url;
    tableId = id;
    if (status == "active") {
        $("#active").modal("toggle");
    }
    if (status == "inactive") {
        $("#inactive").modal("toggle");
    }
}

function ShowCancelModel(url, id) {

    dataUrl = url;
    tableId = id;
    $("#cancelModel").modal("toggle");
}

function ShowApprovedModal(url, id) {
    dataUrl = url;
    tableId = id;
    $("#Approved").modal("toggle");
}

function ShowRejectedModel(url, id) {
    dataUrl = url;
    tableId = id;
    $("#Rejected").modal("toggle");
}

function ShowCloseModel(url, id) {
    dataUrl = url;
    tableId = id;
    $("#Closed").modal("toggle");
}

function GetHoursAndMinutesByDecimalHours(DecimalHours) {

    var decimalTime = parseFloat(DecimalHours);
    decimalTime = decimalTime * 60 * 60;
    var hours = Math.floor((decimalTime / (60 * 60)));
    decimalTime = decimalTime - (hours * 60 * 60);
    var minutes = Math.floor((decimalTime / 60));
    decimalTime = decimalTime - (minutes * 60);
    var seconds = Math.round(decimalTime);
    if (hours < 10) {
        hours = "0" + hours;
    }
    if (minutes < 10) {
        minutes = "0" + minutes;
    }
    if (seconds < 10) {
        seconds = "0" + seconds;
    }
    var HoursAndMinute = "" + hours + ":" + minutes;
    return HoursAndMinute;
    // alert("" + hours + ":" + minutes + ":" + seconds);
}

function TimeFormat(time) {
    // Check correct time format and split into components
    time = time.toString().match(/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];

    if (time.length > 1) { // If time format correct
        time = time.slice(1, 4);  // Remove full string match value
        time[4] = +time[0] < 12 ? ' AM' : ' PM'; // Set AM/PM
        time[0] = +time[0] % 12 || 12; // Adjust hours
    }
    return time.join(''); // return adjusted time or original string
}