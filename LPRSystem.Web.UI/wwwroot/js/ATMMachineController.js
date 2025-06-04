function ATMMachineController() {

    var self = this;

    self.dbLocations = [];

    self.init = function () {

        var table = new Tabulator("#atmmachinegrid", {
            ajaxURL: '/ATMMachine/FetchAtmMachine',
            ajaxParams: {},
            ajaxConfig: {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                },
            },
            ajaxResponse: function (url, params, response) {
                console.log(response);
                return response.data;
            },
            height: "100%",
            layout: "fitColumns",
            columns: [
                { title: "Id", field: "ATMId" },
                { title: "Code", field: "ATMCode" },
                { title: "Location", field: "LocationName" },
                { title: "CreatedBy", field: "CreatedBy" },
                { title: "CreatedOn", field: "CreatedOn" },
                { title: "ModifiedBy", field: "ModifiedBy" },
                { title: "ModifiedOn", field: "ModifiedOn" },
                {
                    title: "Is Active",
                    field: "IsActive",
                    formatter: "tickCross",
                    align: "center"
                }
            ]
        });

        $.ajax({
            type: "GET",
            url: "/Location/GetLocations",
            success: function (response) {
                self.dbLocations = response && response.data ? response.data : [];
                console.log(self.dbLocations);
                populateSelect(self.dbLocations);
            }, error: function (error) {
                console.error(error);
            }
        });
        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditATMMachineForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $(document).on("click", "#addBtn", function () {
            console.log("hiii");
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });
        $('#AddEditATMMachineForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();
            var formData = getFormData('#AddEditATMMachineForm');
            var atm = {
                ATMId: 0,
                ATMCode: formData.ATMCode,
                LocationId: formData.LocationId,
                CreatedBy: 1,
                CreatedOn: new Date(),
                ModifiedBy: 1,
                ModifiedOn: new Date(),
                IsActive: true,
            };
            console.log(formData);
            $.ajax({
                type: "POST",
                url: "/ATMMachine/InsertOrUpdateAtmMachine",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(atm),
                success: function (response) {
                    $('#AddEditATMMachineForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    hideLoader();
                }, error: function (error) {
                    console.error(error);
                }
            });

        });
        function populateSelect(data) {
            const $locationId = $('#LocationId');
            $locationId.empty(); // Clear existing options
            // Add default placeholder option
            $locationId.append('<option value="">-- Select an option --</option>');
            // Loop through data and append options
            data.forEach(function (item) {
                // For example, assuming item has id and name fields
                $locationId.append(
                    $('<option></option>').val(item.LocationId).text(item.LocationName)
                );
            });
        }
    }
}