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
    public class GetAllParkingTicketByATMIdParkedOnRepository : BaseRepository, IGetAllParkingTicketByATMIdParkedOnRepository
    {
        public GetAllParkingTicketByATMIdParkedOnRepository(IConfiguration configuration) : base(configuration) 
        {
            
        }

        public async Task<Models.ParkingTicket.ParkingTicket> ExecuteAsync(Models.ParkingTicket.GetAllParkingTicketByATMIdParkedOnRequest aTMIdRequest)
        {
            return await base.QueryFirstOrDefaultAsync<Models.ParkingTicket.ParkingTicket>(CommonConstants.CommonDB, ParkingTicketConstants.GetAllParkingTicketByATMIdAndParkedOn, aTMIdRequest,null);
        }
    }
}
