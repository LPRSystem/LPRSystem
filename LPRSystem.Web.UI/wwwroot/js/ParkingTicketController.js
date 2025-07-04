function ParkingTicketController() {

    var self = this;

    self.dbParkingPrices = [];

    self.currentUser = {};

    self.currentAtm = {};

    self.formatDateForTicketCode = {};

    makeFormGeneric("#formParkingTicket", "#btnSubmit");

    self.init = function () {

        var appUser = storageService.get('ApplicationUser');
        if (appUser) {
            self.currentUser = appUser;
            console.log(self.currentUser);
        }

        if (appUser.RoleId === 15) {
            var atm = storageService.get("ATMMachine");
            if (atm) {
                self.currentAtm = atm;
            }
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

            e.preventDefault();

            showLoader();

            console.log("button submited");

            var carnumber = $("#carnumber").val();
            var phone = $("#phonenumber").val();
            var duration = $("#duration").val();



            var refernaceNumber = formatDateWithTimezone();
            console.log(refernaceNumber);

            var referanceCode = formatDateForTicketCode();
            console.log(referanceCode);


            var refernaceNumberATM = self.currentUser.FirstName + "-" + refernaceNumber;
            console.log(refernaceNumberATM);

            var referenceNumberCode = self.currentUser.FirstName + "_" + referanceCode;
            console.log(referenceNumberCode);

            const currentDate = new Date();

            var parkingPrice = self.dbParkingPrices.filter(x => x.ParkingPriceId == parseInt(duration))[0];
            console.log(parkingPrice);

            var minutes;

            switch (parkingPrice.Duration) {
                case "30 mins - $1.00":
                    minutes = 30;
                    break;
                case "60 mins (1hr) - $2.00":
                    minutes = 60;
                    break;
                case "90 mins (1.5hr) - $3.00":
                    minutes = 90;
                    break;
                case "120 mins (2hr) - $4.00":
                    minutes = 120;
                    break;
                case "180 mins (3hr) - $5.00":
                    minutes = 180;
                    break;
                case "240 mins (4hr) - $6.00":
                    minutes = 240;
                    break;
                case "Daily (24hr) - $20.00":
                    minutes = 1440; // 24 hours in minutes
                    break;
                case "Overnight (12hr) - $15.00":
                    minutes = 720; // 12 hours in minutes
                    break;
                case "Weekly - $100.00":
                    minutes = 10080; // 7 days in minutes
                    break;
                case "Monthly - $350.00":
                    minutes = 43200; // 30 days in minutes (approx)
                    break;
                case "15 mins - $0.50":
                    minutes = 15;
                    break;
                case "45 mins - $1.50":
                    minutes = 45;
                    break;
                case "2 hrs - $4.00":
                    minutes = 120; // 2 hours in minutes
                    break;
                case "3 hrs - $5.50":
                    minutes = 180; // 3 hours in minutes
                    break;
                case "4 hrs - $7.00":
                    minutes = 240; // 4 hours in minutes
                    break;
                case "5 hrs - $8.50":
                    minutes = 300; // 5 hours in minutes
                    break;
                case "6 hrs - $10.00":
                    minutes = 360; // 6 hours in minutes
                    break;
                case "7 hrs - $11.50":
                    minutes = 420; // 7 hours in minutes
                    break;
                case "8 hrs - $13.00":
                    minutes = 480; // 8 hours in minutes
                    break;
                case "10 hrs - $15.00":
                    minutes = 600; // 10 hours in minutes
                    break;
                default:
                    minutes = 0;
                    break;
            }



            var parkingDurationFrom = new Date(currentDate); // Ensure it's a Date object
            var parkingDurationToDate = new Date(currentDate); // Ensure it's a Date object

            // Create a new Date object for parkingDurationTo
            var parkingDurationTo = new Date(parkingDurationToDate);

            // Set the minutes only if it's not zero
            if (minutes !== 0) {
                parkingDurationTo.setMinutes(parkingDurationTo.getMinutes() + minutes);
            }

            console.log(parkingDurationTo); // This will log the correct Date object

            var totalDuration = Math.floor((parkingDurationTo - parkingDurationFrom) / 60000);

            var parkingTicket = {
                ParkingTicketId: 0,
                ATMId: self.currentAtm.ATMId,
                ParkingTicketCode: referenceNumberCode,
                ParkingTicketRefrence: refernaceNumberATM,
                ParkedOn: parkingDurationFrom, // Use parkingDurationFrom
                ParkingDurationFrom: parkingDurationFrom,
                ParkingDurationTo: parkingDurationTo, // Use the updated parkingDurationTo
                TotalDuration: totalDuration.toString(),
                ParkingPriceId: parkingPrice.ParkingPriceId,
                VehicleNumber: carnumber,
                PhoneNumber: phone,
                IsExtended: false,
                ExtendedOn: null,
                ExtendedDurationFrom: "",
                ExtendedDurationTo: "",
                ActualAmount: parkingPrice.Price,
                ExtendedAmount: 0,
                TotalAmount: parkingPrice.Price,
                Status: "Draft"
            };

            console.log("parkingTicket..." + JSON.stringify(parkingTicket));

            var parkingTicketInfo = addCommonProperties(parkingTicket);

            console.log("parkingTicketInfo..." + JSON.stringify(parkingTicketInfo));
            console.log(parkingTicket);

            makeAjaxRequest({
                url: "/ParkingTicket/InserOrUpdateParkingTicket",
                data: parkingTicket,
                type: 'POST',
                successCallback: handleSuccess,
                errorCallback: handleError
            });
        });

        function handleSuccess(response) {
            console.log(response);
            hideLoader();
            window.location.href = `/ParkingTicket/MakePayment?parkingTicketId=${response.data}`;
        }
        function handleError(error) {
            console.error(error);
            hideLoader();
        }

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