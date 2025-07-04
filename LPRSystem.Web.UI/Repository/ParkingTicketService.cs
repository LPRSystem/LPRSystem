using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Newtonsoft.Json;
using System.Text;

namespace LPRSystem.Web.UI.Repository
{
    public class ParkingTicketService : IParkingTicketService
    {
        private HttpClient _httpClient;
        public ParkingTicketService() 
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7239/api/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout = new TimeSpan(0, 0, 120);
        }
        public async Task<bool> DeleteParkingTicketAsync(long parkingTicketId)
        {
            var url = Path.Combine("ParkingTicket/DeleteParkingTicket", parkingTicketId.ToString());

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContext= await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<bool>(responseContext);
            }
            return false;
        }

        public async Task<List<ParkingTicket>> GetParkingTicketAsync()
        {
            List<ParkingTicket> parkingTickets = new List<ParkingTicket>();

            var responseContent = await _httpClient.GetAsync("ParkingTicket/GetParkingTicketAsync");

            if (responseContent.IsSuccessStatusCode)
            {
                var response= await responseContent.Content.ReadAsStringAsync();

                parkingTickets = JsonConvert.DeserializeObject<List<ParkingTicket>>(response);
            }
            return parkingTickets;
        }

        public async Task<ParkingTicket> GetParkingTicketByIdAsync(long parkingTicketId)
        {
            ParkingTicket parkingTicket = new ParkingTicket();

            var url = Path.Combine("parkingticket/getallparkingticketbyid", parkingTicketId.ToString());

            var responseContent = await _httpClient.GetAsync(url);

            if (responseContent.IsSuccessStatusCode)
            {
                var response = await responseContent.Content.ReadAsStringAsync();

                parkingTicket = JsonConvert.DeserializeObject<ParkingTicket>(response);

            }
            return parkingTicket;
        }

        public async Task<long> InserOrUpdateParkingTicketAsync(ParkingTicket parkingTicket)
        {
            var inputParkingTicket =JsonConvert.SerializeObject(parkingTicket);

            var requestParkingTicket = new StringContent(inputParkingTicket,Encoding.UTF8,"application/json");

            var response = await _httpClient.PostAsync("parkingticket/saveparkingticket", requestParkingTicket);

            if (response.IsSuccessStatusCode)
            {
                var content= await response.Content.ReadAsStringAsync();

                var responseParkingTicket = JsonConvert.DeserializeObject<long>(content);

                return responseParkingTicket;
            }
            return 0;
        }

        public async Task<ParkingTicket> UpdateParkingticketAsync(ParkingTicket parkingTicket)
        {
            var inputParkingTicket = JsonConvert.SerializeObject(parkingTicket);

            var requestParkingTicket = new StringContent(inputParkingTicket, Encoding.UTF8,"application/json");

            var response = await _httpClient.PutAsync("ParkingTicket/UpdateParkingticket", requestParkingTicket);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responseParkingTicket = JsonConvert.DeserializeObject<ParkingTicket>(content);

                return responseParkingTicket;
            }
            return null;

        }
    }
}
