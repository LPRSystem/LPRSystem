Function ATMMachineController()
{
    var self = this;
    self.selectedRows = [];
    self.currectSelectedATMMachine = [];
    self.ATMMachine = {};
    self.todayDate = new Date();
    self.fileUploadModal = $("#fileUploadModal");
    self.ImportedATMMachine = [];
    var acstion = [];
    actions.push('/ATMMachine/FetchATMMachine');

    self.init = function () {
        var atmmachine = storageService.get("ATMMAchine");
        if (atmmachine) {
            self.ATMMAchine = atmmachine;
        }
        self.atmmachinegrid = new Tabulator("#atmmachinegrid", {
            ajaxURL: '/ATMMachine/FetchATMMachine',
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
            height: "600px",
            layout: "fitColumns",
            resizableColumnFit: true,
            columns: [
                { title: "ATM Code", field: "ATMCode" },
                { title: "Location Id", field: "LocationId" },
                {
                    title: "Is Active",
                    field: "IsActive",
                    formatter: "tickCross",
                    align: "center"
                }
            ]
        });
        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            return $.ajax(ajaxConfig);
        });
    }


}