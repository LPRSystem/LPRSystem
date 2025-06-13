function CityController() {
    var self = this;
    self.dbCountrys = [];
    self.dbState = [];
    self.dbCity = [];
    self.selectedRows = [];
    self.currentSelectedCity = {};
    self.init = function () {
        makeFormGeneric('#AddEditCityForm', '#btnsubmit');
        var table = new Tabulator("#citygrid", {
            ajaxURL: '/City/GetCity',
            ajaxParams: {},
            ajaxConfig: {
                method: 'GET',
                headers: {
                    'Content-Type': 'application-json'
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
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentCityChkbox'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().CityId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childCityChkbox-${rowId}' class='childCityChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "Id", field: "CityId" },
                { title: "State", field: "StateId" },
                { title: "Country", field: "CountryId" },
                { title: "Name", field: "Name" },
                { title: "Description", field: "Description" },
                { title: "Code", field: "CityCode" },
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
                $('#parentCityChkbox').prop('checked', allSelected);
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
                    var foundRow = rows.find(row => row.getData().CityId === changedRow.CityId);

                    if (foundRow) {
                        var rowId = foundRow.getData().CityId;
                        var checkbox = document.querySelector(`#childCityChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedCity = changedRow;
                        }
                        else {
                            self.currentSelectedCity = {};
                        }
                    }
                }
            }

        });

        $.ajax({
            type: "GET",
            url: "/Country/FetchAllCountries",
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
            url: "/State/Index",
            success: function (response) {
                self.dbState = response && response.data ? response.data : [];
                console.log(self.dbState);
                populateSelect(self.dbState);
            }, error: function (error) {
                console.error(error);
            }
        }); 
        $(document).on("change", "#parentCityChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childCityChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childCityChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.CityId === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentCityChkbox').prop('checked', allSelected);
        });

        $(document).on("click", "#addBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("am getting from add button click");
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditCityForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $(document).on("click", "#editBtn", function () {
            console.log(self.currentSelectedCity);
            $("#Country").val(self.currentSelectedCity.Country);
            $("#State").val(self.currentSelectedCity.State);
            $("#Name").val(self.currentSelectedCity.Name);
            $("#Description").val(self.currentSelectedCity.Description);
            $("#Code").val(self.currentSelectedCity.CityCode);
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });

        $(document).on("click", "#deleteBtn", function () {
            console.log(self.currentSelectedCity);
            $("#confirmDeleteModal").modal("show");

        });

        $(document).on("click", "#confirmDeleteBtn", function () {
            console.log(self.currentSelectedCity);
            showLoader();
            $.ajax({
                type: "DELETE",
                url: "/City/DeleteCity",
                data: { cityId: self.currentSelectedCity.CityId },
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
            console.log(self.currentSelectedCity);
            $("#confirmCopyModal").modal("show");
        });

        $(document).on("click", "#confirmCopyBtn", function () {
            console.log(self.currentSelectedCity);
            self.currentSelectedCity.CityId = 0;
            self.currentSelectedCity.State = self.currentSelectedCity.State + "_Copy";
            self.currentSelectedCity.Country = self.currentSelectedCity.Country + "_Copy";
            self.currentSelectedCity.Name = self.currentSelectedCity.Name + "_Copy";
            self.currentSelectedCity.Description = self.currentSelectedCity.Description + "_Copy";
            self.currentSelectedCity.CityCode = self.currentSelectedCity.CityCode + "_Copy";
            showLoader();
            $.ajax({
                type: "POST",
                url: "/City/InsertOrUpdateCity",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(self.currentSelectedCity),
                success: function (response) {
                    $("#confirmCopyModal").modal("hide");
                    table.setData();
                    hideLoader();
                }, error: function (error) {
                    console.error(error);
                }
            });
        });

        $(document).on("change", "#inputSearch", function () {
            var searchValue = $('#inputSearch').val().toLowerCase();

            if (searchValue) {
                table.setFilter(function (data) {
                    return (
                        String(data.CityId).toLowerCase().includes(searchValue) ||
                        String(data.StateId).toLowerCase().includes(searchValue) ||
                        String(data.CountryId).toLowerCase().includes(searchValue) ||
                        String(data.Name).toLowerCase().includes(searchValue) ||
                        String(data.Description).toLowerCase().includes(searchValue) ||
                        String(data.CityCode).toLowerCase().includes(searchValue) ||
                        String(data.CreatedBy).toLowerCase().includes(searchValue) ||
                        String(data.ModifiedBy).toLowerCase().includes(searchValue) ||
                        (data.IsActive ? "yes" : "no").includes(searchValue)
                    );
                });
            } else {
                table.clearFilter();
            }
        });

        $('#AddEditCityForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();
            var formData = getFormData('#AddEditCityForm');
            var city = {
                CityId: self.currectSelectedCity && self.currectSelectedCity.CityId ? self.currectSelectedCity.CityId : 0,
                StateId: fromData.StateId,
                CountryId: fromData.CountryId,
                Name: formatDate.Name,
                Description: fromData.Description,
                CityCode: fromData.CityCode,
                CreatedBy: -1,
                CreatedOn: new Date(),
                ModifiedBy: -1,
                ModifiedOn: new Date(),
                IsActive: true
            };

            console.log(city);

            $.ajax({
                type: "POST",
                url: "/City/InsertOrUpdateCity",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(city),
                success: function (response) {
                    $('#AddEditCityForm')[0].reset();
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



    }
}