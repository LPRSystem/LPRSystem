
namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public class GetParkingTicketsManager : IGetParkingTicketsManager
    {
        private readonly IGetParkingTicketsRepository _repository;
        public GetParkingTicketsManager(IGetParkingTicketsRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Models.ParkingTicket.ParkingTicketModel>> ExecuteAsync()

        {
            return await _repository.ExecuteAsync();
        }
    }
}
