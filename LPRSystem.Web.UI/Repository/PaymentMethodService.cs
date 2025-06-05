using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Newtonsoft.Json;

namespace LPRSystem.Web.UI.Repository
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private HttpClient _httpClient;

        public PaymentMethodService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7165/api/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout=new TimeSpan(0,0,120);
        }

        public async Task<List<PaymentMethod>> GetPaymentMethodsAsync()
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
            
            var responseContent = await _httpClient.GetAsync("paymentmethods");

            if (responseContent.IsSuccessStatusCode)
            {
                var response = await responseContent.Content.ReadAsStringAsync();

                paymentMethods = JsonConvert.DeserializeObject<List<PaymentMethod>>(response);

            }
            return paymentMethods;
        }
    }
}

