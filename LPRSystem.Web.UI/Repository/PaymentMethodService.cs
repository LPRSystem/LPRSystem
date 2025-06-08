using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Newtonsoft.Json;
using System.Text;

namespace LPRSystem.Web.UI.Repository
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private HttpClient _httpClient;

        public PaymentMethodService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7239/api/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout = new TimeSpan(0, 0, 120);
        }

        public async Task<bool> DeletePaymentmethodAsync(long id)
        {
            var url = Path.Combine("paymentMethod/deletepaymentmethod", id.ToString());

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<bool>(responseContent);
            }
            return false;
        }

        public async Task<PaymentMethod> GetPaymentMethodByIdAsync(long paymentmethodid)
        {
            PaymentMethod paymentMethod = new PaymentMethod();

            var url = Path.Combine("paymentmethod/getpaymentmethodbyid", paymentmethodid.ToString());

            var responseContent = await _httpClient.GetAsync(url);

            if (responseContent.IsSuccessStatusCode)
            {
                var response = await responseContent.Content.ReadAsStringAsync();

                paymentMethod = JsonConvert.DeserializeObject<PaymentMethod>(response);

            }
            return paymentMethod;
        }

        public async Task<List<PaymentMethod>> GetPaymentMethodsAsync()
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();

            var responseContent = await _httpClient.GetAsync("paymentmethod/getpaymentmethods");

            if (responseContent.IsSuccessStatusCode)
            {
                var response = await responseContent.Content.ReadAsStringAsync();

                paymentMethods = JsonConvert.DeserializeObject<List<PaymentMethod>>(response);

            }
            return paymentMethods;
        }

        public async Task<PaymentMethod> InsertOrUpdatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            var inputPaymentMethod = JsonConvert.SerializeObject(paymentMethod);

            var requestPaymentMethod = new StringContent(inputPaymentMethod, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("paymentmethod/savepaymentmethod", requestPaymentMethod);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responsePaymentMethod = JsonConvert.DeserializeObject<PaymentMethod>(content);

                return responsePaymentMethod;
            }
            return null;
        }

        public async Task<PaymentMethod> UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            var inputPaymentMethod = JsonConvert.SerializeObject(paymentMethod);

            var requestPaymentMethod = new StringContent(inputPaymentMethod, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("paymentmethod/updatepaymentmethod", requestPaymentMethod);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responsePaymentMethod = JsonConvert.DeserializeObject<PaymentMethod>(content);

                return responsePaymentMethod;
            }
            return null;
        }
    }
}

