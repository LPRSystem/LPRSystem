function MakePaymentParkingTicketController() {

    var self = this;

    self.currentUser = {};

    self.currentAtm = {};


    self.parkingTicketId = 0;

    // makeFormGeneric("#formParkingTicket", "#btnSubmit");

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


        //one ajax to bring the GetParkingTicketById


        //get all payment mthods from db
    }
};