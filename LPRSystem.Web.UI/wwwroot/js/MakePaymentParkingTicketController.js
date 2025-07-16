function MakePaymentParkingTicketController() {

    var self = this;

    self.currentUser = {};

    self.currentAtm = {};

    self.parkingTicketId = 0;

    self.dbPaymentMethod = [];

    self.parkingTicket = {};

    self.currectSelectedParkingTicketPayment = {};

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


                $("#Amount").val(parseFloat(self.parkingTicket.TotalAmount).toFixed(2));

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
                genarateDropdown("PaymentType", self.dbPaymentMethod, "Id", "Name");
            },
            error: function (error) {
                console.error(error);
            }
        });

        $('#formMakePaymentParkingTicket').on('submit', function (e) {
            e.preventDefault();
            showLoader();

            console.log("button submited");

            var amount = $('#Amount').val();
            var paymentMethod = $('#PaymentType').val();
            var paymentReference = $('#PaymentReference').val();

            var parkingTicketPayment = {
                ParkingTicketPaymentId: 0,
                ATMId: self.currentAtm.ATMId,
                PaymentMethodId: paymentMethod,
                ParkingTicketId: self.parkingTicketId,
                PaymentReference: paymentReference,
                TotalAmount: self.parkingTicket.TotalAmount, 
                PaidAmount: amount,
                DueAmount: 0,
                Status: "Paid"
            }

            console.log("parkingTicketPayment..." + JSON.stringify(parkingTicketPayment));
            var parkingTicketPaymentInfo = addCommonProperties(parkingTicketPayment);
            console.log("parkingTicketPaymentInfo..." + JSON.stringify(parkingTicketPaymentInfo));
            console.log(parkingTicketPayment);



            $.ajax({
                type: "POST",
                url: "/ParkingTicketPayment/InsertParkingTicketPayment",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(parkingTicketPayment),
                success: function (response) {
                    console.log(response);
                    //$('#formParkingTicketPaymentDetails')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    //table.setData();
                    hideLoader();
                }, error: function (error) {
                    console.error(error);
                }
            });
        });
    }
};