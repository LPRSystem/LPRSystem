using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using LPRSystem.Web.Utility;
using Newtonsoft.Json;
using System.Text;

namespace LPRSystem.Web.UI.Repository
{
    public class RoleService : IRoleService
    {
        private HttpClient _httpClient;
        public RoleService() 
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7165/api/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout = new TimeSpan(0, 0, 120);
        }

        public Task<bool> DeleteRoleAsync(Guid roleId)
        {
            throw new NotImplementedException();
        }

       

        public async Task<List<Role>> GetRolesAsync()
        {
            List<Role> roles = new List<Role>();
            var response = await _httpClient.GetAsync("roles");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                roles = JsonConvert.DeserializeObject<List<Role>>(responseContent);

            }
            return roles;
        }

        public async Task<string> InsertOrUpdateRole(RoleManagement role)
        {
            var inputRole = JsonConvert.SerializeObject(role);

            var requestRole = new StringContent(inputRole, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("RoleProcess", requestRole);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var responceRole = JsonConvert.DeserializeObject<string>(content);

                return responceRole;

            }

            return string.Empty;
        }
    }
}
