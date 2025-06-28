using LPRSystem.Web.API.Manager.Models.ParkingTicket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public class GetAllParkingTicketByATMIdParkedOnManager : IGetAllParkingTicketByATMIdParkedOnManager
    {
        private readonly IGetAllParkingTicketByATMIdParkedOnRepository _repository;
        public GetAllParkingTicketByATMIdParkedOnManager(IGetAllParkingTicketByATMIdParkedOnRepository repository)
        {
            _repository = repository;
        }

        public async Task<Models.ParkingTicket.ParkingTicket> ExecuteAsync(GetAllParkingTicketByATMIdParkedOnRequest aTMIdRequest)
        {
            return await _repository.ExecuteAsync(aTMIdRequest);
        }
    }
}
