using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Models.ParkingTicket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public class GetAllParkingTicketsByATMRepository : BaseRepository, IGetAllParkingTicketsByATMRepository
    {
        public GetAllParkingTicketsByATMRepository(IConfiguration configuration) : base(configuration)
        {
            
        }

        public async Task<Models.ParkingTicket.ParkingTicket> ExecuteAsync(GetParkingTicketByATMIdRequest atmId)
        {
            return await base.QueryFirstOrDefaultAsync<Models.ParkingTicket.ParkingTicket>(CommonConstants.CommonDB, ParkingTicketConstants.GetAllParkingTicketsByATM, atmId, null);
        }
    }
}
