function MakePaymentParkingTicketController() {

    var self = this;

    self.currentUser = {};

    self.currentAtm = {};

    self.parkingTicketId = 0;

    self.dbPaymentMethod = [];
    self.parkingTicket = {};

    makeFormGeneric("#formMakePaymentParkingTicket", "#btnSubmit");

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

        var parkingTicketId = getQueryStringParameter("parkingTicketId");

        if (parkingTicketId) {
            self.parkingTicketId = parkingTicketId;
        }

        console.log("parkingTicketId....." + parkingTicketId);


        $.ajax({
            type: "GET",
            url: "/ParkingTicket/FetchParkingTicketById",
            data: { parkigTicketId: self.parkingTicketId },
            success: function (response) {

                console.log(response);

                self.parkingTicket = response.data;
                $("#TicketId").text(self.parkingTicket.ParkingTicketId);
                $("#TicketId").val(self.parkingTicket.ParkingTicketId);
                $("#VehicleNo").text(self.parkingTicket.vehicleNumber);
                $("#PhoneNo").text(self.parkingTicket.PhoneNumber);
                $("#DurationFrom").text(self.parkingTicket.ParkingDurationFrom);
                $("#DurationTo").text(self.parkingTicket.ParkingDurationTo);
                $("#TotalDuration").text(self.parkingTicket.TotalDuration);
                $("#Status").text(self.parkingTicket.Status);


                $("#amount").val(parseFloat(self.parkingTicket.TotalAmount).toFixed(2));

                //genarateDropdown("parkingMethod", self.dbPaymentMethod, "ParkingMethodId", "ParkingMethod");
            },
            error: function (error) {
                console.error(error);
            }
        });

        $.ajax({
            type: "GET",
            url:'/PaymentMethod/FetchPaymentMethods',
            success: function (response) {
                console.log(response);
                self.dbPaymentMethod = response.data;
                genarateDropdown("paymentType", self.dbPaymentMethod, "Id", "Name");
            },
            error: function (error) {
                console.error(error);
            }
        });
        //get all payment mthods from db
    }
};