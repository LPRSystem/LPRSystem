
namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public class GetAllParkingTicketByATMIdParkedOnManager : IGetAllParkingTicketByATMIdParkedOnManager
    {
        private readonly IGetAllParkingTicketByATMIdParkedOnRepository _repository;
        public GetAllParkingTicketByATMIdParkedOnManager(IGetAllParkingTicketByATMIdParkedOnRepository repository)
        {
            _repository = repository;
        }

        public async Task<Models.ParkingTicket.ParkingTicket> ExecuteAsync(Models.ParkingTicket.GetAllParkingTicketByATMIdParkedOnRequest aTMIdRequest)
        {
            return await _repository.ExecuteAsync(aTMIdRequest);
        }
    }
}
