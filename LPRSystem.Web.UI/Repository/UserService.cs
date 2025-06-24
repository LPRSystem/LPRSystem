using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using LPRSystem.Web.Utility;
using Newtonsoft.Json;
using System.Text;

namespace LPRSystem.Web.UI.Repository
{
    public class UserService : IUserService
    {
        private HttpClient _httpClient;
        public UserService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7239/api/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout = new TimeSpan(0, 0, 120);
        }

        public async Task<ApplicationUser> AuthenticationAsync(Authenticate authenticate)
        {
            ApplicationUser applicationUser = new ApplicationUser();

            var _authenticate = JsonConvert.SerializeObject(authenticate);

            var requestContent = new StringContent(_authenticate, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("auth/authentication", requestContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                applicationUser = JsonConvert.DeserializeObject<ApplicationUser>(content);
            }
            return applicationUser;
        }

        public async Task<List<User>> FetchAllUser()
        {
            List<User> users = new List<User>();
            var response = await _httpClient.GetAsync("users");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<User>>(responseContent);

            }
            return users;
        }

        public async Task<string> InsertOrUpdateUser(UserRegistration user)
        {
            var inputUser = JsonConvert.SerializeObject(user);

            var requestUser = new StringContent(inputUser, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("User/saveuser", requestUser);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var responceUser = JsonConvert.DeserializeObject<string>(content);

                return responceUser;

            }

            return string.Empty;
        }
    }
}
