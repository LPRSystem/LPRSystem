function StateController() {
    var self = this;

    self.init = function () {

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
            height: "100%",
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
                            var rowId = row.getData().Id;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childStateChkbox-${rowId}' class='childStateChkbox' data-row-id='${rowId}'/></div>`;
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
                { title: "CreatedOn", field: "CreatedOn" },
                { title: "CreatedBy", field: "CreatedBy" },
                { title: "ModifiedOn", field: "ModifiedOn" },
                { title: "ModifiedBy", field: "ModifiedBy" },
                {
                    title: "Is Active",
                    field: "IsActive",
                    formatter: "tickCross",
                    align: "center"

                }
            ],
        });
    }
}