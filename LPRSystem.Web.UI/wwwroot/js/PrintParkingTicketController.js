function PrintParkingTicketController() {
    var self = this;
    self.parkingTicketId = 0;

    self.init = function () {
        var parkingTicketId = getQueryStringParameter("parkingTicketId");

        if (parkingTicketId) {
            self.parkingTicketId = parkingTicketId;
        }

        console.log("parkingTicketId....." + parkingTicketId);

        if (self.parkingTicketId) {
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
                },
                error: function (error) {
                    console.error("error");
                }
            });
        }
    };
}