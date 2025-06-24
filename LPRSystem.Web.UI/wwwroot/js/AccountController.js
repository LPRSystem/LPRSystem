function AccountController() {
    var self = this;
    self.init = function () {

        makeFormGeneric("#formAuthentication", "#btnSubmit");
       
        $(document).on("click", "#btnSubmit", function (e) {
            e.preventDefault();
            $(".se-pre-con").show();

            var userAuthetnication = {
                Username: $("#username").val(),
                Password: $("#password").val()
            };

            makeAjaxRequest({
                url: API_URLS.AuthenticateAsync,
                data: userAuthetnication,
                type: 'POST',
                successCallback: handleAuthenticationSuccess,
                errorCallback: handleAuthenticationError
            });
        });

        function handleAuthenticationSuccess(response) {
            console.info(response);
            if (response.data.Status) {

                var _appUserInfo = storageService.get('ApplicationUser');
                if (_appUserInfo) {
                    storageService.remove('ApplicationUser');
                }

                var applicationUser = response.data;

                storageService.set('ApplicationUser', applicationUser);
               
                window.location.href = "/Home/Index";

                updateEnvironmentAndVersion();

                $(".se-pre-con").hide();
            }
        }

        function handleAuthenticationError(xhr, status, error) {
            console.error("Error in upserting data to server: " + error);
            $(".se-pre-con").hide();
        }
        function updateEnvironmentAndVersion() {
            var environment = storageService.get('Environment');
            if (environment) {
                storageService.remove('Environment');
            }
            storageService.set('Environment', window.location.hostname);

            var version = storageService.get('Version');
            if (version) {
                storageService.remove('Version');
            }
            storageService.set('Version', '1.0.0.0');
        }
    };
}