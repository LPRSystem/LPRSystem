function PaymentMethodController() {
    var self = this;

    self.selectedRows = [];

    self.currectSelectedPaymentMethod = {};

    self.init = function () {

        makeFormGeneric('#AddEditPaymentMethodForm', '#btnsubmit');



        var table = new Tabulator("#paymentMethodgrid", {
            ajaxURL: '/PaymentMethod/FetchPaymentMethods',
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
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentPaymentMethodChkbox'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().Id;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childPaymentMethodChkbox-${rowId}' class='childPaymentMethodChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "Id", field: "Id" },
                { title: "Name", field: "Name" },
                { title: "Code", field: "Code" },
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
                $('#parentPaymentMethodChkbox').prop('checked', allSelected);
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
                    var foundRow = rows.find(row => row.getData().Id === changedRow.Id);

                    if (foundRow) {
                        var rowId = foundRow.getData().Id;
                        var checkbox = document.querySelector(`#childPaymentMethodChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedPaymentMethod = changedRow;
                        }
                        else {
                            self.currectSelectedPaymentMethod = {};
                        }
                    }
                }
            }

        });
        $(document).on("change", "#parentPaymentMethodChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childPaymentMethodChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childPaymentMethodChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.Id === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentPaymentMethodChkbox').prop('checked', allSelected);
        });


        $(document).on("click", "#addBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("am getting from add button click");
        });


        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditPaymentMethodForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $(document).on("change", "#inputSearch", function () {
            var searchValue = $('#inputSearch').val().toLowerCase();

            if (searchValue) {
                table.setFilter(function (data) {
                    return (
                        String(data.Id).toLowerCase().includes(searchValue) ||
                        String(data.Name).toLowerCase().includes(searchValue) ||
                        String(data.Code).toLowerCase().includes(searchValue) ||
                        String(data.CreatedBy).toLowerCase().includes(searchValue) ||
                        String(data.ModifiedBy).toLowerCase().includes(searchValue) ||
                        (data.IsActive ? "yes" : "no").includes(searchValue)
                    );
                });
            } else {
                table.clearFilter();
            }
        });

        $('#AddEditPaymentMethodForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();
            var name = $("#Name").val();
            var code = $("#Code").val();

            var paymentMethod = {
                Id: self.currectSelectedPaymentMethod && self.currectSelectedPaymentMethod.Id ? self.currectSelectedPaymentMethod.Id : 0,
                Name: name,
                Code: code,
                CreatedBy: -1,
                CreatedOn: new Date(),
                ModifiedBy: -1,
                ModifiedOn: new Date(),
                IsActive: true
            };

            console.log(paymentMethod);

            $.ajax({
                type: "POST",
                url: "/PaymentMethod/InsertOrUpdatepaymentMethod",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(paymentMethod),
                success: function (response) {
                    $('#AddEditPaymentMethodForm')[0].reset();
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
            console.log(self.currectSelectedPaymentMethod);
            $("#Name").val(self.currectSelectedPaymentMethod.Name);
            $("#Code").val(self.currectSelectedPaymentMethod.Code);
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });

        $(document).on("click", "#deleteBtn", function () {
            console.log(self.currectSelectedPaymentMethod);
            $("#confirmDeleteModal").modal("show");
        });

        $(document).on("click", "#confirmDeleteBtn", function () {
            console.log(self.currectSelectedPaymentMethod);
            showLoader();
            $.ajax({
                type: "DELETE",
                url: "/PaymentMethod/DeletePaymentMethod",
                data: { Id: self.currectSelectedPaymentMethod.Id },
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
            console.log(self.currectSelectedPaymentMethod);
            $("#confirmCopyModal").modal("show");
        });

        $(document).on("click", "#confirmCopyBtn", function () {
            console.log(self.currectSelectedPaymentMethod);
            self.currectSelectedPaymentMethod.Id = 0;
            self.currectSelectedPaymentMethod.Code = self.currectSelectedPaymentMethod.Code + "_Copy";
            showLoader();
            $.ajax({
                type: "POST",
                url: "/PaymentMethod/InsertOrUpdatepaymentMethod",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(self.currectSelectedPaymentMethod),
                success: function (response) {
                    $("#confirmCopyModal").modal("hide");
                    table.setData();
                    hideLoader();
                }, error: function (error) {
                    console.error(error);
                }
            });
        });

    }
}