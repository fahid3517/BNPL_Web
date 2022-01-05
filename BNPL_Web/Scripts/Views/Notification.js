


$(document).ready(function () {
   

    ShowLoader();
    $.ajax({
        url: "../api/Notification/GetNotificationDetail",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        success: function (data) {

            if (data.Notifications.length >= 1) {

                for (var i = 0; i < data.Notifications.length; i++) {
                    
                    $('#notificationDetail').append('<li>' +
                        '<p><b>' + data.Notifications[i].Name + '</b>:' + ' ' + data.Notifications[i].Description + '</p>' +
                       // '<span class="time">at ' + (data[i].CreatedOn.substr(0, 10)).split('-').reverse().join('-') + '</span>' +
                        '<span class="time">'  + data.Notifications[i].TotalTime + '</span>' +
                        '</li>');
                }
            }
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







    $.ajax({
        url: "../api/Notification/ReadNotfication",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (data) {

        },

        error: function (data) {
            var response = data.responseText.replace(/"/g, '');
            $("#success").hide();
            $("#error").html(response);
            $("#error").show();
            HideLoader();
        }
    });
});