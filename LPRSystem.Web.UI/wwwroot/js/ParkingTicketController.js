function ParkingTicketController() {
    var self = this;

    self.dbParkingPrices = [
        { "ParkingPriceId": 2, "Duration": "0-1 hour", "Price": 2.50, "IsActive": 0 },
        { "ParkingPriceId": 3, "Duration": "1-2 hours", "Price": 4.00, "IsActive": 0 },
        { "ParkingPriceId": 4, "Duration": "2-3 hours", "Price": 5.50, "IsActive": 0 },
        { "ParkingPriceId": 5, "Duration": "3-4 hours", "Price": 7.00, "IsActive": 0 },
        { "ParkingPriceId": 6, "Duration": "4-5 hours", "Price": 8.50, "IsActive": 0 },
        { "ParkingPriceId": 7, "Duration": "5-6 hours", "Price": 10.00, "IsActive": 0 },
        { "ParkingPriceId": 8, "Duration": "6-12 hours", "Price": 12.00, "IsActive": 0 },
        { "ParkingPriceId": 9, "Duration": "12-24 hours", "Price": 15.00, "IsActive": 0 },
        { "ParkingPriceId": 10, "Duration": "Weekend Rate", "Price": 20.00, "IsActive": 0 },
        { "ParkingPriceId": 12, "Duration": "0-1 hour", "Price": 3.50, "IsActive": 0 },
        { "ParkingPriceId": 13, "Duration": "1-2", "Price": 8.00, "IsActive": 0 },
        { "ParkingPriceId": 14, "Duration": "1-2", "Price": 8.00, "IsActive": 0 },
        { "ParkingPriceId": 15, "Duration": "30 mins", "Price": 1.00, "IsActive": 1 },
        { "ParkingPriceId": 16, "Duration": "60 mins (1hr)", "Price": 2.00, "IsActive": 1 },
        { "ParkingPriceId": 17, "Duration": "90 mins (1.5hr)", "Price": 3.00, "IsActive": 1 },
        { "ParkingPriceId": 18, "Duration": "120 mins (2hr)", "Price": 4.00, "IsActive": 1 },
        { "ParkingPriceId": 19, "Duration": "180 mins (3hr)", "Price": 5.00, "IsActive": 1 },
        { "ParkingPriceId": 20, "Duration": "240 mins (4hr)", "Price": 6.00, "IsActive": 1 },
        { "ParkingPriceId": 21, "Duration": "Daily (24hr)", "Price": 20.00, "IsActive": 1 },
        { "ParkingPriceId": 22, "Duration": "Overnight (12hr)", "Price": 15.00, "IsActive": 1 },
        { "ParkingPriceId": 23, "Duration": "Weekly", "Price": 100.00, "IsActive": 1 },
        { "ParkingPriceId": 24, "Duration": "Monthly", "Price": 350.00, "IsActive": 1 },
        { "ParkingPriceId": 25, "Duration": "15 mins", "Price": 0.50, "IsActive": 1 },
        { "ParkingPriceId": 26, "Duration": "45 mins", "Price": 1.50, "IsActive": 1 },
        { "ParkingPriceId": 27, "Duration": "2 hrs", "Price": 4.00, "IsActive": 1 },
        { "ParkingPriceId": 28, "Duration": "3 hrs", "Price": 5.50, "IsActive": 1 },
        { "ParkingPriceId": 29, "Duration": "4 hrs", "Price": 7.00, "IsActive": 1 },
        { "ParkingPriceId": 30, "Duration": "5 hrs", "Price": 8.50, "IsActive": 1 },
        { "ParkingPriceId": 31, "Duration": "6 hrs", "Price": 10.00, "IsActive": 1 },
        { "ParkingPriceId": 32, "Duration": "7 hrs", "Price": 11.50, "IsActive": 1 },
        { "ParkingPriceId": 33, "Duration": "8 hrs", "Price": 13.00, "IsActive": 1 },
        { "ParkingPriceId": 34, "Duration": "10 hrs", "Price": 15.00, "IsActive": 1 }
    ]
        ;
    self.init = function () {


        //$.ajax({
        //    url: "",
        //    type: "",
        //    success: function (response) {
        //        self.dbParkingPrices = response && response.data ? response.data : [];
        //        self.bindParkingPriceDropdown(self.dbParkingPrices);
        //    },
        //    error: function (error) {

        //    }
        //});

        self.bindParkingPriceDropdown();


        makeFormGeneric("#formAuthentication","#btnSubmit")

    }

    self.bindParkingPriceDropdown = function () {


        var $dropdown = $('#duration');
        $dropdown.empty();

        var $defaultOption = $('<option>', {
            value: '',
            text: 'Select an option'
        });
        $dropdown.append($defaultOption);

        $.each(self.dbParkingPrices, function (index, item) {
            var $option = $('<option>', {
                value: item["ParkingPriceId"],
                text: item["Duration"] + " - " + "$" + item["Price"]
            });
            $dropdown.append($option);
        });

        $dropdown.trigger('change');
    };
}