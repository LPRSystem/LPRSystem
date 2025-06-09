function CountryController() {
    var self = this;
    self.selectedRows = [];
    self.currectSelectedCountry = {};
    self.init = function () {
        var table = new Tabulator("#countrygrid", {
            ajaxURL: '/Country/FetchAllCountries',
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
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentCountryChkbox'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().CountryId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childCountryChkbox-${rowId}' class='childCountryChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "Id", field: "CountryId" },
                { title: "Name", field: "Name" },
                { title: "Code", field: "CountryCode" },
                { title: "Description", field: "Description" },
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
                $('#parentCountryChkbox').prop('checked', allSelected);
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
                    var foundRow = rows.find(row => row.getData().CountryId === changedRow.CountryId);

                    if (foundRow) {
                        var rowId = foundRow.getData().CountryId;
                        var checkbox = document.querySelector(`#childCountryChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedCountry = changedRow;
                        }
                        else {
                            self.currectSelectedCountry = {};
                        }
                    }
                }
            }
        });


        $(document).on("change", "#parentCountryChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childCountryChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childCountryChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.CountryId === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentCountryChkbox').prop('checked', allSelected);
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditCountryForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $(document).on("click", "#addBtn", function () {
            console.log("hiii");
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });

        $(document).on("click", "#editBtn", function () {
            console.log(self.currectSelectedCountry);
            $("#Name").val(self.currectSelectedCountry.Name);
            $("#CountryCode").val(self.currectSelectedCountry.CountryCode);
            $("#Description").val(self.currectSelectedCountry.Description);
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });
        $(document).on("click", "#deleteBtn", function () {
            console.log(self.currectSelectedCountry);
            $("#confirmDeleteModal").modal("show");

        });
        $(document).on("click", "#confirmDeleteBtn", function () {
            console.log(self.currectSelectedCountry);
            showLoader();
            $.ajax({
                type: "DELETE",
                url: "/Country/DeleteCountry",
                data: { atmId: self.currectSelectedCountry.ATMId },
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
            console.log(self.currectSelectedCountry);
            $("#confirmCopyModal").modal("show");
        });

        $(document).on("click", "#confirmCopyBtn", function () {
            console.log(self.currectSelectedCountry);
            self.currectSelectedCountry.CountryId = 0;
            self.currectSelectedCountry.Name = self.currectSelectedCountry.Name + "_Copy";
            self.currectSelectedCountry.Description = self.currectSelectedCountry.Description + "_Copy";
            self.currectSelectedCountry.CountryCode = self.currectSelectedCountry.CountryCode + "_Copy";

            showLoader();
            $.ajax({
                type: "POST",
                url: "/Country/InsertOrUpdateCountry",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(self.currectSelectedCountry),
                success: function (response) {
                    $("#confirmCopyModal").modal("hide");
                    table.setData();
                    hideLoader();
                }, error: function (error) {
                    console.error(error);
                }
            });
        });

        $('#AddEditCountryForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();
            var formData = getFormData('#AddEditCountryForm');
            var atm = {
                CountryId: self.currectSelectedCountry && self.currectSelectedCountry.CountryId ? self.currectSelectedCountry.CountryId : 0,
                Name: formData.Name,
                Description: formData.Description,
                CountryCode: formData.CountryCode,
                CreatedBy: 1,
                CreatedOn: new Date(),
                ModifiedBy: 1,
                ModifiedOn: new Date(),
                IsActive: true,
            };
            console.log(atm);
            $.ajax({
                type: "POST",
                url: "/Country/InsertOrUpdateCountry",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(atm),
                success: function (response) {
                    $('#AddEditCountryForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    table.setData();
                    hideLoader();
                }, error: function (error) {
                    console.error(error);
                }
            });

        });
    }

}