function RoleController() {
    var self = this;
    self.selectedRows = [];
    self.currectSelectedRole = {};
    self.todayDate = new Date();
    self.fileUploadModal = $("#fileUploadModal");
    self.ImportedRoles = [];
    self.init = function () {
        $("#permissionBtn").removeClass("permission-hidden");
        var table = new Tabulator("#rolesGrid", {
            height: "600px",
            layout: "fitColumns",
            resizableColumnFit: true,
            ajaxURL: '/Role/FetchUserRoles',
            ajaxParams: {},
            ajaxConfig: {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                },
            },
            ajaxResponse: function (url, params, response) {
                return response.data;
            },
            columns: [
                {
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentRoleChkbox' style='margin-top: 22px;'></div>",
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
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childRoleChkbox-${rowId}' class='childRoleChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "Name", field: "Name", headerFilter: "input" },
                { title: "Code", field: "Code", headerFilter: "input" },
                { title: "Created By", field: "CreatedBy", headerFilter: "input" },
                { title: "Created On", field: "CreatedOn", headerFilter: "input" },
                { title: "Modified By", field: "ModifiedBy", headerFilter: "input" },
                { title: "Modified On", field: "ModifiedOn", headerFilter: "input" },
                { title: "IsActive", field: "IsActive", headerFilter: "input" }

            ],
            rowSelectionChanged: function (data, rows) {
                var allSelected = rows.length && rows.every(row => row.isSelected());
                $('#parentRoleChkbox').prop('checked', allSelected);
                disableAllButtons();

                // Enable buttons based on selection
                if (rows.length > 0) {
                    enableButtons(table);
                }

                // Find the most recently changed row
                let currentSelectedRows = rows.map(row => row.getData());
                let changedRow = null;

                if (self.selectedRows.length > currentSelectedRows.length) {
                    // A row was deselected
                    changedRow = self.selectedRows.find(row => !currentSelectedRows.includes(row));
                } else if (self.selectedRows.length < currentSelectedRows.length) {
                    // A row was selected
                    changedRow = currentSelectedRows.find(row => !self.selectedRows.includes(row));
                }


                // Update the previous selected rows state
                self.selectedRows = currentSelectedRows;
                // Handle the changed row data
                if (changedRow) {
                    var rows = table.getRows();
                    var foundRow = rows.find(row => row.getData().Id === changedRow.Id);

                    if (foundRow) {
                        var rowId = foundRow.getData().Id;
                        var checkbox = document.querySelector(`#childRoleChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedRole = changedRow;
                        }
                        else {
                            self.currectSelectedRole = {};
                        }
                    }

                }


            }
        });
    }
}