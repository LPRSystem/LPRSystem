function StateController() {

    var self = this;

    self.selectedRows = [];

    self.CurrentSelectedState = {};

    self.dbCountries = [];

    self.init = function () {

        makeFormGeneric('#AddEditStateForm', '#btnsubmit');

        var table = new Tabulator("#stategrid", {
            ajaxURL: '/State/FetchStates',
            ajaxParams: {},
            ajaxConfig: {
                method: 'GET',
                headers: {
                    'Conent-Type': 'application/json',
                },
            },
            ajaxResponse: function (url, params, response) {
                console.log(response);
                return response.data;
            },
            height: "700px",
            layout: "fitColumns",
            columns: [
                {
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentStateChkbox'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().StateId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childStateChkbox-${rowId}' class='childStateChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "StateId", field: "StateId" },
                { title: "Name", field: "Name" },
                { title: "Code", field: "StateCode" },
                { title: "Description", field: "Description" },
                { title: "Country", field: "CountryName" },
                {
                    title: "Is Active",
                    field: "IsActive",
                    formatter: "tickCross",
                    align: "center"

                }
            ],
            rowSelectionChanged: function (data, rows) {
                var allSelected = rows.length && rows.every(row => row.isSelected());
                $('#parentStateChkbox').prop('Checked', allSelected)

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
                    var foundRow = rows.find(row => row.getData().StateId === changedRow.StateId);

                    if (foundRow) {
                        var rowId = foundRow.getData().StateId;
                        var checkbox = document.querySelector(`#childStateChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedState = changedRow;
                        }
                        else {
                            self.currectSelectedState = {};
                        }
                    }
                }
            }
        });

        $(document).on("change", "#parentStateChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childStateChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childStateChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.Id === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentStateChkbox').prop('checked', allSelected);
        });


        $(document).on("click", "#addBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("Iam getting from add button click");
        });


        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditStateForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $(document).on("change", "#inputSearch", function () {
            var searchValue = $('#inputSearch').val().toLowerCase();

                if (searchValue) {
                    table.setFilter(function (data) {
                        return (
                            String(data.StateId).toLowerCase().includes(searchValue) ||
                            String(data.Name).toLowerCase().includes(searchValue) ||
                            String(data.StateCode).toLowerCase().includes(searchValue) ||
                            String(data.Description).toLowerCase().includes(searchValue) ||
                            String(data.CountryName).toLowerCase().includes(searchValue) ||
                            String(data.CountryCode).toLowerCase().includes(searchValue) ||
                            (data.IsActive ? "yes" : "no").includes(searchValue)
                        );
                    });
                } else {
                    table.clearFilter();
                }
        });

        $('#AddEditStateForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();

            var countryId = $("#CountryId").val();
            var name = $("#Name").val();
            var code = $("#StateCode").val();
            var description = $("#Description").val();

            var stateObject = {
                StateId: self.currectSelectedState && self.currectSelectedState.StateId ? self.currectSelectedState.StateId : 0,
                CountryId: countryId,
                Name: name,
                Description: description,
                StateCode: code,
            };
            var state = addCommonProperties(stateObject);

            console.log(state);

            $.ajax({
                type: "POST",
                url: "/State/InsertOrUpdatestate",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(state),
                success: function (response) {
                    $('#AddEditStateForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    table.setData();
                    hideLoader();
                }, error: function (error) {
                    console.error(error);
                }
            });
        });

        $(document).on("click", "#editBtn", function () {
            console.log(self.currectSelectedState);
            $("#CountryId").val(self.currectSelectedState.CountryId);
            $("#Name").val(self.currectSelectedState.Name);
            $("#StateCode").val(self.currectSelectedState.StateCode);
            $("#Description").val(self.currectSelectedState.Description);
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });

        $(document).on("click", "#deleteBtn", function () {
            console.log(self.currectSelectedState);
            $("#confirmDeleteModal").modal("show");
        });

        $(document).on("click", "#confirmDeleteBtn", function () {
            console.log(self.currectSelectedState);
            showLoader();
            $.ajax({
                type: "DELETE",
                url: "/State/DeleteState",
                data: { stateId: self.currectSelectedState.StateId },
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
            console.log(self.currectSelectedState);
            $("#confirmCopyModal").modal("show");
        });

        $(document).on("click", "#confirmCopyBtn", function () {
            console.log(self.currectSelectedState);
            self.currectSelectedState.StateId = 0;
            self.currectSelectedState.Name = self.currectSelectedState.Name + "_Copy";
            self.currectSelectedState.Description = self.currectSelectedState.Description + "_Copy";
            self.currectSelectedState.StateCode = self.currectSelectedState.StateCode + "_Copy";
            showLoader();
            $.ajax({
                type: "POST",
                url: "/State/InsertOrUpdateState",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(self.currectSelectedState),
                success: function (response) {
                    $("#confirmCopyModal").modal("hide");
                    table.setData();
                    hideLoader();
                }, error: function (error) {
                    console.error(error);
                }
            });
        });

        $.ajax({
            type: "GET",
            url: "/Country/FetchAllCountries",
            success: function (response) {
                self.dbCountries = response.data;
                genarateDropdown("CountryId", self.dbCountries, "CountryId", "Name");
            }, error: function (error) {
                console.error(error);
            }
        });
    }
}