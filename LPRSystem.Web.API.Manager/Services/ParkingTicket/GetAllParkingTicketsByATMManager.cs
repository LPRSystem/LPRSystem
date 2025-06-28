using LPRSystem.Web.API.Manager.Models.ParkingTicket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public class GetAllParkingTicketsByATMManager : IGetAllParkingTicketsByATMManager
    {
        private readonly IGetAllParkingTicketsByATMRepository _repository;

        public GetAllParkingTicketsByATMManager(IGetAllParkingTicketsByATMRepository repository)
        {
            _repository = repository;
        }
        public async Task<Models.ParkingTicket.ParkingTicket> ExecuteAsync(GetParkingTicketByATMIdRequest atmId)
        {
            return await _repository.ExecuteAsync(atmId);
        }
    }
}
