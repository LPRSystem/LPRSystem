function ParkingTicketController() {

    var self = this;

    self.dbParkingPrices = [];

    self.currentUser = {};

    makeFormGeneric("#formParkingTicket", "#btnSubmit");

    self.init = function () {

        var appUser = storageService.get('ApplicationUser');
        if (appUser) {
            self.currentUser = appUser;
            console.log(self.currentUser);
        }

        $.ajax({
            type: "GET",
            url: "/ParkingPrice/GetParkingPrices",
            success: function (response) {

                console.log(response);

                self.dbParkingPrices = response.data;

                self.BindDurationDropDown(self.dbParkingPrices);

                /*genarateDropdown("duration", self.dbParkingPrices, "ParkingPriceId", "Duration");*/

            },
            error: function (error) {
                console.error(error);
            }
        });


        $('#formParkingTicket').on('submit', function (e) {
            console.log("buttong submited");

            var carnumber = $("#carnumber").val();
            var phone = $("#phonenumber").val();
            var duration = $("#duration").val();

            console.log(carnumber, phone, duration);
        });
    }

    self.BindDurationDropDown = function (data) {

        var $dropdown = $('#duration');
        $dropdown.empty();

        var $defaultOption = $('<option>', {
            value: '',
            text: 'Select an option'
        });
        $dropdown.append($defaultOption);

        $.each(data, function (index, item) {
            var $option = $('<option>', {
                value: item["ParkingPriceId"],
                text: item["Duration"]
            });
            $dropdown.append($option);
        });

        $dropdown.trigger('change');
    }
}