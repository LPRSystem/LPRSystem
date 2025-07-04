function ParkingTicketController() {

    var self = this;

    self.dbParkingPrices = [];

    self.currentUser = {};

    self.formatDateForTicketCode = {};

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

            e.preventDefault();

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

            var minutes = 0;

            if (parkingPrice.Duration === "60 mins - $1.00") {
                minutes = 60;
            } else if (parkingPrice.Duration === "30 mins - $1.00") {
                minutes = 30;
            }

            
            var parkingDurationFrom = currentDate;   
            
            var parkingDurationTo = currentDate.setMinutes(currentDate.getMinutes() + minutes);
            var totalDuration = Math.floor((parkingDurationTo - parkingDurationFrom) / 60000);
           
            var parkingTicket = {
                ParkingTicketId: 0,
                ATMId: 0,
                ParkingTicketCode: referenceNumberCode,
                ParkingTicketRefrence: refernaceNumberATM,
                ParkedOn: currentDate,
                ParkingDurationFrom: currentDate, 
                ParkingDurationTo: parkingDurationTo /*currentDate.setMinutes(currentDate.getMinutes() + miniutes)*/,
                TotalDuration: totalDuration.toString(),
                ParkingPriceId: parkingPrice.ParkingPriceId,
                vehicleNumber: carnumber,
                PhoneNumber: phone,
                IsExtended: false,
                ExtendedOn: null,
                ExtendedDurationFrom: "",
                ExtendedDurationTo: "",
                ActualAmount: parkingPrice.Price,
                ExtendeAmount: 0,
                TotalAmount: parkingPrice.Price,
                Status: "Draft"
            };
            console.log("parkingTicket..." + JSON.stringify(parkingTicket));

            var parkingTicketInfo = addCommonProperties(parkingTicket);

            console.log("parkingTicketInfo..." + JSON.stringify(parkingTicketInfo));
            console.log(parkingTicket);
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