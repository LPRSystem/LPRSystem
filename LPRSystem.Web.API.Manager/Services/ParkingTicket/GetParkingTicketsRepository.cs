using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Models.ParkingTicket;
using Microsoft.Extensions.Configuration;

namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public class GetParkingTicketsRepository : BaseRepository, IGetParkingTicketsRepository
    {
        public GetParkingTicketsRepository(IConfiguration configuration ) : base(configuration)
        {
            
        }
        public async Task<IEnumerable<ParkingTicketModel>> ExecuteAsync()
        {
            return await base.QueryAsync<ParkingTicketModel>(CommonConstants.CommonDB, ParkingTicketConstants.GetAllParkingTickets, null, null);
        }
    }
}
