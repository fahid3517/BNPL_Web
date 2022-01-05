var rl = 0, selectedRole = "";

$(document).ready(function () {
    GetData();

});

//Get All Roles and Permissions
function GetData() {
    ShowLoader();
    $.ajax({
        url: '/api/Roles/GetAllPrivilegesAndRole',
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var data = data;
            if (data) {
                // populate Roles
                roles = data.Roles;
                populateRoles(roles);
                // populate privileges
                privileges = data.Privileges;
                populatePrivileges(privileges);
            }
            HideLoader();
        },
        error: function (data) {
            var response = data.responseText.replace(/"/g, '');
            HideLoader();
        }
    });
};

function populateRoles(roles) {
    for (k = 0; k < roles.length; k++) {
        var object = roles[k];
        rl++;
        $("#rolesList").append('<li id="role-' + rl + '" class="" onclick="getAssignPermission(' + "'" + object.RoleId + "'" + ",'" + rl + "'" + ')">' +
            '<a href = "javascript:void(0);" >' +
            object.Name +
            '<span class= "role-action" >' +
            '<span class="action-circle large" onclick="populateRoleInfo(' + "'" + object.RoleId + "'" + ",'" + object.Name + "'" + ')">' +
            '<i class="material-icons">edit</i>' +
            '</span>' +
            '<span class="action-circle large delete-btn" data-toggle="modal" data-target="#DeleteModal">' +
            '<i class="material-icons">delete</i>' +
            '</span>' +
            '</span >' +
            '</a >' +
            '</li >');
    }
    return;
}

function populatePrivileges(privileges) {
    var Portals = [];
    $.each(privileges, function (p, privilege) {
        let obj = Portals.find(o => o.Portal === privilege.Portal);
        if ((typeof obj) != "object") {
            Portals.push({
                Id: privilege.PrivilegeId,
                Portal: privilege.Portal
            });
        }
    });
    // appending categories
    $.each(Portals, function (p, portal) {
        portal.Categories = [];
        var tempArray = $.grep(privileges, function (v) {
            return v.Portal === portal.Portal;
        });

        $.each(tempArray, function (c, category) {
            let obj = portal.Categories.find(o => o.Name === category.Category);
            if ((typeof obj) != "object") {
                portal.Categories.push({
                    Id: category.PrivilegeId,
                    Name: category.Category,
                    Privileges: []
                });
            }
        });
    });
    // end appending categories
    // appending privileges
    $.each(Portals, function (p, portal) {
        var data = $.grep(privileges, function (v) {
            return v.Portal === portal.Portal;
        });
        $.each(portal.Categories, function (c, category) {
            var Privileges = $.grep(data, function (v) {
                return v.Category === category.Name;
            });
            $.each(Privileges, function (i, privilege) {
                category.Privileges.push({
                    Name: privilege.Name,
                    Id: privilege.PrivilegeId
                });
            });
        });

    });
    // end appending privileges

    LoadPrivilegesData(Portals);
}

//function to load all privileges
function LoadPrivilegesData(data) {
    var Portal = null;
    var div_list_privileges = $("#list-priviliges > ul");
    div_list_privileges.html("");
    $.each(data, function (p, portal) {
        Portal = "<li id='portal" + portal.Id + "' class='list-group-item'>" +
            "<label data-toggle='collapse' href='#portal-" + portal.Id + "' class='text-capitalize text-normal'> " + portal.Portal + "</label>" +
            "<label class='switch'>" +
            "<input type='checkbox' name='" + portal.Portal + "'>" +
            "<span class='slider round'></span>" +
            "</label>";
        Portal += "<div id='portal-" + portal.Id + "' class='collapse in'><ul></ul>" +
            "</div></li>";
        div_list_privileges.append(Portal);
        $.each(portal.Categories, function (c, category) {
            Category = "<li id='cat" + category.Id + "'class='list-group-item' > " +
                "<label data-toggle='collapse' href='#category-" + category.Id + "' class='text-capitalize text-normal'> " + category.Name + "</label>" +
                "<label class='switch'>" +
                "<input type='checkbox' name='" + category.Name + "'>" +
                "<span class='slider round'></span>" +
                "</label>" +
                "<div id='category-" + category.Id + "' class='collapse in'><ul></ul>" +
                "</div></li>";
            $("#portal-" + portal.Id + " > ul").append(Category);

            $.each(category.Privileges, function (m, privilege) {
                Privilege = "<li class='list-group-item'>" +
                    "<label class='text-capitalize text-normal'> " + privilege.Name + "</label>" +
                    "<label class='switch'>" +
                    "<input class='privilege' type='checkbox' name='" + privilege.Name + "' id='" + privilege.Id + "'>" +
                    "<span class='slider round'></span>" +
                    "</label>" +
                    "</li>";
                $("#category-" + category.Id + " > ul").append(Privilege);
            });
        });
    });
    ImplementClicks();
}

function ImplementClicks() {
    $("input[type='checkbox']").change(function () {
        console.log("changes");
        var listItem = $(this).closest("li");
        $(listItem).find("input[type='checkbox']").prop("checked", $(this).is(":checked"));
        if ($(this).is(":checked")) {
            var parentListItems = $(listItem).parents("li");
            //$.each(parentListItems, function (x, item)
            //{
            //    $(item).find("> .switch > input[type='checkbox']").prop("checked", true);
            //});
            $(parentListItems).find("> .switch > input[type='checkbox']").prop("checked", true);
        }
        else {
            var CurrentListItem = listItem;
            while (CurrentListItem != undefined) {
                var SiblingListItems = $(CurrentListItem).siblings("li");
                var shouldParentUnselect = true;
                $.each(SiblingListItems, function (i, item) {
                    if ($(item).find("> .switch > input[type='checkbox']").is(":checked")) {
                        shouldParentUnselect = false;
                        return false;
                    }
                });
                if (shouldParentUnselect) {
                    var firstParentListItem = $(CurrentListItem).parents("li")[0];
                    $(firstParentListItem).find("> .switch > input[type='checkbox']").prop("checked", false);
                    CurrentListItem = $(CurrentListItem).parents("li")[0];
                }
                else {
                    return false;
                }
            }
        }
    });
}

function getAssignPermission(roleId, mId) {
    var menuid = "role-" + mId;
    activeMenu(menuid)
    $("#btn-role-submit").prop('disabled', false);
    selectedRole = roleId;
    ShowLoader();
    $.ajax({
        url: '/api/Roles/GetAssignPrivilegeByRoleId?id=' + roleId,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            showAssignPrivileges(data);
            HideLoader();
        },
        error: function (data) {
            var response = data.responseText.replace(/"/g, '');
            HideLoader();
        }
    })
}

function showAssignPrivileges(data) {
    $("input[type='checkbox']").prop("checked", false);
    if (data.length > 0) {
        for (var p = 0; p < data.length; p++) {
            var ob = data[p];
            $("input[Id=" + ob.PrivilegeId + "]").prop("checked", true);
            $("input[Id=" + ob.PrivilegeId + "]").trigger("change");
        }

    }
}

function activeMenu(_menuId) {
    $("#" + _menuId).addClass("active");
    var list = $("#rolesList")[0];
    var listEms = list.getElementsByTagName("li");
    for (var le = 0; le < listEms.length; le++) {
        var emId = listEms[le].id;
        if (emId != _menuId) $("#" + emId).removeClass("active");
    }
}

function Submit() {
    var _data = [];
    var selectedPrivileges = $(".privilege:checked");
    if (selectedPrivileges.length > 0 && selectedRole != "") {
        ShowLoader();
        $("#btn-role-submit").prop("disabled", true);
        $.each(selectedPrivileges, function (i, item) {
            var value;
            value = {
                RoleId: selectedRole,
                PrivilegeId: $(item).attr("id")
            }
            _data.push(value);
        });
        $.ajax({
            url: '/api/Roles/AssignPrivilegeToRole',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: JSON.stringify(_data),
            success: function (response) {
                debugger;
                toastr.success("Permission successfully assigned", { timeOut: 5000 });
                HideLoader();
                resetForm();
            },
            error: function (response) {
                toastr.error(response.responseText, { timeOut: 5000 });
                HideLoader();
            }
        });
    }
}

function resetForm() {
    // uncheck all selected checked items
    $("input[type='checkbox']").prop("checked", false);

    // Remove Active class
    var list = $("#rolesList")[0];
    var listEms = list.getElementsByTagName("li");
    for (var le = 0; le < listEms.length; le++) {
        var emId = listEms[le].id;
        $("#" + emId).removeClass("active");
    }
}

function loadAllRole() {
    // Remove All Roles
    $("#rolesList").empty();
    // uncheck all selected checked items
    $("input[type='checkbox']").prop("checked", false);
    rl = 0, selectedRole = "";

    $.ajax({
        url: '/api/Roles/GetAllPrivilegesAndRole',
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var data = data;
            if (data) {
                // populate Roles
                roles = data.Roles;
                populateRoles(roles);
            }
            HideLoader();
        },
        error: function (data) {
            var response = data.responseText.replace(/"/g, '');
            HideLoader();
        }
    });
}

function DeleteRole() {
    $("#DeleteModal").modal("toggle");
    if (selectedRole == null || selectedRole == "") {

        toastr.error("Please select role first", { timeOut: 5000 });
        return;
    }
    ShowLoader();
    $.ajax({
        type: "DELETE",
        url: '/api/Roles/Delete/?Id=' + selectedRole,
        success: function (data) {
            toastr.success("Role deleted successfully", { timeOut: 5000 });
            loadAllRole();
        },
        error: function (response) {
            var response = response.responseText.replace(/"/g, '');
            toastr.error(response, { timeOut: 5000 });
            HideLoader();
        }
    });

}

function AddRole() {
    var RoleName= $("#RoleName").val();
    //var Role = {
    //    Id: "",
    //    RoleName: $("#RoleName").val(),
    //    allPrivelages: []
    //};
    debugger;
    if (RoleName != undefined && RoleName != "") {
        ShowModalLoader();
        $.ajax({
            url: '/api/Roles/Add?role=' + RoleName,
            type: 'POST',
          //  data: JSON.stringify(Role),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                toastr.success("Role added successfully", { timeOut: 5000 });
                loadAllRole();
                HideModalLoader();
                $('#add_role').hide();
                $('.modal-backdrop').hide();

            },
            error: function (data) {
                var response = data.responseText.replace(/"/g, '');
                toastr.error(response, { timeOut: 5000 });
                HideModalLoader();

            }
        });
    }
}

function populateRoleInfo(roleId, roleName) {
    debugger;
    $('#RoleID').val(roleId);
    $('#updateName').val(roleName);
    $("#edit_role").modal("toggle");
}

function UpdateRole() {
    var Role = {
        Id: $('#RoleID').val(),
        RoleName: $('#updateName').val()
    };
    ShowModalLoader();
    $.ajax({
        url: '/api/Roles/Put/',
        type: 'PUT',
        data: JSON.stringify(Role),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            //$("#modalUpdateError").hide();
            //$("#modalUpdateSuccess").html("Role Updated Successfully");
            //$("#modalUpdateSuccess").show();
            toastr.success("Role updated successfully", { timeOut: 5000 });
            loadAllRole();
            HideModalLoader();
        },
        error: function (data) {
            var response = data.responseText.replace(/"/g, '');
            //$("#modalUpdateSuccess").hide();
            //$("#modalUpdateError").html(response);
            //$("#modalUpdateError").show();
            toastr.error(response, { timeOut: 5000 });
            HideModalLoader();
        }
    });
}