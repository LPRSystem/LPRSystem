function UserController() {
    var self = this;
    self.coreDBRoles = [];
    self.ApplicationUser = [];
    var actions = [];
    var dataObject = [];
    actions.push('/Role/FetchRoles');

    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        self.ApplicationUser = appuser;

        self.usersgrid = new Tabulator("#usersgrid", {
            ajaxURL: '/User/FetchUsers',
            ajaxParams: {},
            ajaxConfig: {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                },
            },

            ajaxResponse: function (url, params, response) {
                var userData = [];

                userData = response.data;

                return userData;
            },
            height: "600px",
            layout: "fitColumns",
            resizableColumnFit: true,
            columns: [
                { title: "Name", field: "FullName" },
                { title: "Email", field: "Email" },
                { title: "Phone", field: "Phone" },
                { title: "Role", field: "RoleName" },
                { title: "IsBlocked", field: "IsBlocked" },
                { title: "IsActive", field: "IsActive" },
            ]
        });

        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            if (index === 2) {
                ajaxConfig.data = dataObjects[0];
            }
            return $.ajax(ajaxConfig);
        });

        $.when.apply($, requests).done(function (...responses) {
            self.coreDBRoles = responses[0][0]?.data || [];
            self.coreDBDealers = responses[2][0]?.data || [];

            var loggedInUserRole = self.coreDBRoles.find(role => role.Id === self.ApplicationUser.RoleId);
            if (loggedInUserRole) {
                var userRoles = self.filterRoles(loggedInUserRole.Name, self.coreDBRoles);
                genarateDropdown("RoleId", userRoles, "Id", "Name");
            }

            hideLoader();
        }).fail(function () {
            console.log('One or more requests failed.');
        });

        self.filterRoles = function (loggedInRole, roles) {
            if (loggedInRole === 'Administrator') {
                return roles;
            }
            if (loggedInRole === 'System Administrator') {
                return roles.filter(role => !['Administrator', 'System Administrator'].includes(role.Name));
            }

            return [];
        };
        makeFormGeneric('#AddEditUserForm', '#btnsubmit');

        $(document).on("click", "#addBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });
        $('#closeSidebar, .modal-backdrop').on('click', function () {
            $('#AddEditUserForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        self.addeditUser = function (userRegistration) {
            makeAjaxRequest({
                url: '/User/InsertOrUpdateUser',
                data: userRegistration,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        $('#AddEditUserForm')[0].reset();
                        $('#sidebar').removeClass('show');
                        $('.modal-backdrop').remove();
                        self.usersgrid.setData();
                    }
                    console.info(response);
                    hideLoader();
                },
                errorCallback: function (xhr, status, error) {
                    console.error("Error in saving user data: " + error);
                }
            });
        };
    }
}