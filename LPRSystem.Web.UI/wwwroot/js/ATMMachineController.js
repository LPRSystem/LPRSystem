function ATMMachineController() {

    var self = this;

    self.dbLocations = [];

    self.selectedRows = [];

    self.currectSelectedATM = {};

    self.init = function () {

        makeFormGeneric('#AddEditATMMachineForm', '#btnsubmit');

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
                {
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentATMMachineChkbox'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().ATMId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childATMMachineChkbox-${rowId}' class='childATMMachineChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
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
            ],
            rowSelectionChanged: function (data, rows) {
                var allSelected = rows.length && rows.every(row => row.isSelected());
                $('#parentATMMachineChkbox').prop('checked', allSelected);
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
                    var foundRow = rows.find(row => row.getData().ATMId === changedRow.ATMId);

                    if (foundRow) {
                        var rowId = foundRow.getData().ATMId;
                        var checkbox = document.querySelector(`#childATMMachineChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedATM = changedRow;
                        }
                        else {
                            self.currectSelectedATM = {};
                        }
                    }
                }
            }
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

        $(document).on("change", "#parentATMMachineChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childATMMachineChkbox').prop('checked', isChecked);
        });


        $(document).on('change', '.childATMMachineChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.ATMId === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentATMMachineChkbox').prop('checked', allSelected);
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


        $(document).on("click", "#editBtn", function () {
            console.log(self.currectSelectedATM);
            $("#LocationId").val(self.currectSelectedATM.LocationId);
            $("#ATMCode").val(self.currectSelectedATM.ATMCode);
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });
        $(document).on("click", "#deleteBtn", function () {
            console.log(self.currectSelectedATM);
            $("#confirmDeleteModal").modal("show");

        });
        
        $(document).on("click", "#confirmDeleteBtn", function () {
            console.log(self.currectSelectedATM);
            showLoader();
            $.ajax({
                type: "DELETE",
                url: "/ATMMachine/DeleteATMMachine",
                data: { atmId: self.currectSelectedATM.ATMId },
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
            console.log(self.currectSelectedATM);
            $("#confirmCopyModal").modal("show");
        });

        $(document).on("click", "#confirmCopyBtn", function () {
            console.log(self.currectSelectedATM);
            self.currectSelectedATM.ATMId = 0;
            self.currectSelectedATM.ATMCode = self.currectSelectedATM.ATMCode + "_Copy";
            showLoader();
            $.ajax({
                type: "POST",
                url: "/ATMMachine/InsertOrUpdateAtmMachine",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(self.currectSelectedATM),
                success: function (response) {
                    $("#confirmCopyModal").modal("hide");
                    table.setData();
                    hideLoader();
                }, error: function (error) {
                    console.error(error);
                }
            });
        });
        $('#AddEditATMMachineForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();
            var formData = getFormData('#AddEditATMMachineForm');
            var atm = {
                ATMId: self.currectSelectedATM && self.currectSelectedATM.ATMId ? self.currectSelectedATM.ATMId : 0,
                ATMCode: formData.ATMCode,
                LocationId: formData.LocationId,
                CreatedBy: 1,
                CreatedOn: new Date(),
                ModifiedBy: 1,
                ModifiedOn: new Date(),
                IsActive: true,
            };
            console.log(atm);
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
                    table.setData();
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