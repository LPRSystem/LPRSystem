using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Newtonsoft.Json;
using System.Text;

namespace LPRSystem.Web.UI.Repository
{
    public class ParkingTicketPaymentService : IParkingTicketPaymentService
    {
        private HttpClient _httpClient;

        public ParkingTicketPaymentService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7239/api/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout = new TimeSpan(0, 0, 120);
        }

       

        public async Task<ParkingTicketPayment> GetATMByIdAsync(long aTMId)
        {
            ParkingTicketPayment parkingTicketPayment = new ParkingTicketPayment();

            var url = Path.Combine("ParkingTicketPayment/GetATMById", aTMId.ToString());

            var responseContent = await _httpClient.GetAsync(url);

            if (responseContent.IsSuccessStatusCode)
            {
                var response = await responseContent.Content.ReadAsStringAsync();

                parkingTicketPayment = JsonConvert.DeserializeObject<ParkingTicketPayment>(response);

            }
            return parkingTicketPayment;
        }

      

        public async Task<List<ParkingTicketPayment>> GetParkingTicketPaymentAsync()
        {
            List<ParkingTicketPayment> parkingTicketPayments = new List<ParkingTicketPayment>();

            var responseContent = await _httpClient.GetAsync("ParkingTicketPayment/GetParkingTicketPayment");

            if (responseContent.IsSuccessStatusCode)
            {
                var response = await responseContent.Content.ReadAsStringAsync();

                parkingTicketPayments = JsonConvert.DeserializeObject<List<ParkingTicketPayment>>(response);
            }
            return parkingTicketPayments;
        }

        public async Task<ParkingTicketPayment> GetParkingTicketPaymentByIdAsync(long parkingTicketPaymentId)
        {
            ParkingTicketPayment parkingTicketPayment = new ParkingTicketPayment();

            var url = Path.Combine("ParkingTicketPayment/GetParkingTicketPaymentById", parkingTicketPaymentId.ToString());

            var responseContent = await _httpClient.GetAsync(url);

            if (responseContent.IsSuccessStatusCode)
            {
                var response = await responseContent.Content.ReadAsStringAsync();

                parkingTicketPayment = JsonConvert.DeserializeObject<ParkingTicketPayment>(response);

            }
            return parkingTicketPayment;
        }

        public async Task<long> InsertParkingTicketPaymentAsync(ParkingTicketPayment parkingTicketPayment)
        {
            var inputParkingTicketPayment = JsonConvert.SerializeObject(parkingTicketPayment);

            var requestParkingTicketPayment = new StringContent(inputParkingTicketPayment, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("ParkingTicketPayment/SaveParkingTicketPayment",  requestParkingTicketPayment);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responseParkingTicketPayment = JsonConvert.DeserializeObject<long>(content);

                return responseParkingTicketPayment;
            }
            return 0;
        }

        public async Task<ParkingTicketPayment> UpdateParkingTicketPaymentAsync(ParkingTicketPayment parkingTicketPayment)
        {
            var inputParkingTicketPayment = JsonConvert.SerializeObject(parkingTicketPayment);

            var requestParkingTicketPayment = new StringContent(inputParkingTicketPayment, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("ParkingTicket/UpdateParkingticket", requestParkingTicketPayment);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responseParkingTicketPayment = JsonConvert.DeserializeObject<ParkingTicketPayment>(content);

                return responseParkingTicketPayment;
            }
            return null;
        }
    }
}
