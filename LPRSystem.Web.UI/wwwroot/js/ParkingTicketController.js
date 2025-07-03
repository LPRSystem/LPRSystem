function ParkingTicketController() {

    var self = this;

    self.dbParkingPrices = [];

    self.currentUser = {};
    const pad = n => n.toString().padStart(2, "0");
    

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

    self.getTzOffset = function (date = new Date()) {
        const offsetMin = -date.getTimezoneOffset();
        const sign = offsetMin >= 0 ? "+" : "-";
        const absMin = Math.abs(offsetMin);
        const hours = pad(Math.floor(absMin / 60));
        const minutes = pad(absMin % 60);
        return `${sign}${hours}${minutes}`;
    }

    self.generateParkingTicketIds = function (firstName, date = new Date()) {
        if (!firstName || typeof firstName !== "string") {
            throw new Error("First name is required.");
        }

        const yyyy = date.getFullYear();
        const MM = pad(date.getMonth() + 1);
        const dd = pad(date.getDate());
        const HH = pad(date.getHours());
        const mm = pad(date.getMinutes());
        const ss = pad(date.getSeconds());

        const timestamp = `${yyyy}${MM}${dd}${HH}${mm}${ss}`;
        const baseCode = `${firstName}PT${timestamp}`;
        const tz = getTzOffset(date);

        return {
            Code: baseCode,
            reference: '${baseCode}${tz}'
        };
    }
}