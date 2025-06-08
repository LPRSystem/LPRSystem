function LocationController() {

    var self = this;

    self.dbCountrys = [];
    self.dbState = [];
    self.dbCity = [];
    self.selectedRows = [];
    self.currectSelectedLocation = {};
    self.init = function () {
        makeFormGeneric('#AddEditLocationForm', '#btnsubmit');

        var table = new Tabulator("#locationgrid", {
            ajaxURL: '/Location/GetLocations',
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
                {
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentLocationChkbox'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().LocationId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childLocationChkbox-${rowId}' class='childLocationChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "Id", field: "LocationId" },
                { title: "LocationName", field: "LocationName" },
                { title: "Code", field: "Code" },
                { title: "Address", field: "Address" },
                { title: "Country", field: "Name" },
                { title: "State", field: "Name" },
                { title: "City", field: "Name" },
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
            ],
            rowSelectionChanged: function (data, rows) {
                var allSelected = rows.length && rows.every(row => row.isSelected());
                $('#parentLocationChkbox').prop('checked', allSelected);
                disableAllButtons();

                if (rows.length > 0) {
                    enableButtons(table);
                }

                let currentSelectedRows = rows.map(row => row.getData());
                let changedRow = null;

                if (self.selectedRows.length > currentSelectedRows.length) {
                    changedRow = self.selectedRows.find(row => !currentSelectedRows.includes(row));
                } else if (self.selectedRows.length < currentSelectedRows.length) {
                    changedRow = currentSelectedRows.find(row => !self.selectedRows.includes(row));
                }

                self.selectedRows = currentSelectedRows;
                if (changedRow) {
                    var rows = table.getRows();
                    var foundRow = rows.find(row => row.getData().LocationId === changedRow.LocationId);

                    if (foundRow) {
                        var rowId = foundRow.getData().LocationId;
                        var checkbox = document.querySelector(`#childLocationChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedLocation = changedRow;
                        }
                        else {
                            self.currectSelectedLocation = {};
                        }
                    }
                }
            }
        });

        $.ajax({
            type: "GET",
            url: "/Country/GeCountries",
            success: function (response) {
                self.dbCountrys = response && response.data ? response.data : [];
                console.log(self.dbCountrys);
                populateSelect(self.dbCountrys);
            }, error: function (error) {
                console.error(error);
            }
        });

        $.ajax({
            type: "GET",
            url: "/State/GetStates",
            success: function (response) {
                self.dbState = response && response.data ? response.data : [];
                console.log(self.dbState);
                populateSelect(self.dbState);
            }, error: function (error) {
                console.error(error);
            }
        });


        $.ajax({
            type: "GET",
            url: "/City/GetCityes",
            success: function (response) {
                self.dbCity = response && response.data ? response.data : [];
                console.log(self.dbCity);
                populateSelect(self.dbCity);
            }, error: function (error) {
                console.error(error);
            }
        });

        $(document).on("change", "#parentLocationChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childLocationChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childLocationChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.LocationId === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentLocationChkbox').prop('checked', allSelected);
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditLocationForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $(document).on("click", "#addBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });

        $(document).on("click", "#editBtn", function () {
            console.log(self.currectSelectedLocation);
            $("#LocationName").val(self.currectSelectedLocation.LocationName);
            $("#Code").val(self.currectSelectedLocation.Code);
            $("#Address").val(self.currectSelectedLocation.Address);
            $("#Country").val(self.currectSelectedLocation.Country);
            $("#State").val(self.currectSelectedLocation.State);
            $("#City").val(self.currectSelectedLocation.City);
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });
        $(document).on("click", "#deleteBtn", function () {
            console.log(self.currectSelectedLocation);
            $("#confirmDeleteModal").modal("show");

        });

        $(document).on("click", "#confirmDeleteBtn", function () {
            console.log(self.currectSelectedLocation);
            showLoader();
            $.ajax({
                type: "DELETE",
                url: "/Location/DeleteLocation",
                data: { atmId: self.currectSelectedLocation.LocationId },
                success: function (response) {
                    $("#confirmDeleteModal").modal("hide");
                    table.setData();
                    hideLoader();
                }, error: function (error) {
                    console.error(error);
                }
            });
        });

        $(document).on("click", "#copyBtn", function () {
            console.log(self.currectSelectedLocation);
            $("#confirmCopyModal").modal("show");
        });

        $(document).on("click", "#confirmCopyBtn", function () {
            console.log(self.currectSelectedLocation);
            self.currectSelectedLocation.LocationId = 0;
            self.currectSelectedLocation.LocationName = self.currectSelectedLocation.LocationName + "_Copy";
            self.currectSelectedLocation.Code = self.currectSelectedLocation.Code + "_Copy";
            self.currectSelectedLocation.Address = self.currectSelectedLocation.Address + "_Copy";
            self.currectSelectedLocation.Country = self.currectSelectedLocation.Country + "_Copy";
            self.currectSelectedLocation.State = self.currectSelectedLocation.State + "_Copy";
            self.currectSelectedLocation.City = self.currectSelectedLocation.City + "_Copy";
            showLoader();
            $.ajax({
                type: "POST",
                url: "/Location/InsertOrUpdateLocation",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(self.currectSelectedLocation),
                success: function (response) {
                    $("#confirmCopyModal").modal("hide");
                    table.setData();
                    hideLoader();
                }, error: function (error) {
                    console.error(error);
                }
            });
        });


        $('#AddEditLocationForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();
            var formData = getFormData('#AddEditLocationForm');
            var loc = {
                LocationId: currectSelectedLocation && self.currectSelectedLocation.LocationId ? self.currectSelectedLocation.LocationId : 0,
                LocationName: formData.LocationName,
                Code: formData.Code,
                Address: fromData.Address,
                CountryId: fromData.CountryId,
                StateId: formData.StateId,
                City: fromData.CityId,
                CreatedBy: 1,
                CreatedOn: new Date(),
                ModifiedBy: 1,
                ModifiedOn: new Date(),
                IsActive: true,
            };
            console.log(loc);
            $.ajax({
                type: "POST",
                url: "/Location/InsertOrUpdateLocation",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(atm),
                success: function (response) {
                    $('#AddEditLocationForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    table.setData();
                    hideLoader();
                }, error: function (error) {
                    console.error(error);
                }
            });

        });

        function populateSelect(data) {
            const $countryId = $('#CountryId');
            $countryId.empty(); // Clear existing options
            // Add default placeholder option
            $countryId.append('<option value="">-- Select an option --</option>');
            // Loop through data and append options
            data.forEach(function (item) {
                // For example, assuming item has id and name fields
                $countryId.append(
                    $('<option></option>').val(item.CountryId).text(item.Name)
                );
            });
        }

        function populateSelect(data) {
            const $StateId = $('#StateId');
            $StateId.empty(); // Clear existing options
            // Add default placeholder option
            $StateId.append('<option value="">-- Select an option --</option>');
            // Loop through data and append options
            data.forEach(function (item) {
                // For example, assuming item has id and name fields
                $StateId.append(
                    $('<option></option>').val(item.StateId).text(item.Name)
                );
            });
        }

        function populateSelect(data) {
            const $CityId = $('#CityId');
            $CityId.empty(); // Clear existing options
            // Add default placeholder option
            $CityId.append('<option value="">-- Select an option --</option>');
            // Loop through data and append options
            data.forEach(function (item) {
                // For example, assuming item has id and name fields
                $CityId.append(
                    $('<option></option>').val(item.CityId).text(item.Name)
                );
            });
        }
    }
}