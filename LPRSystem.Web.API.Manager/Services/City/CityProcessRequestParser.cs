using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LPRSystem.Web.API.Manager.Services.City
{
    public class CityProcessRequestParser : IRequestParser<LPRSystem.Web.API.Manager.Models.City.City>
    {
        public async Task<Models.City.City> ParseAsync(HttpRequest request)
        {
            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.City.CityProcessRequest>(requestBody);
            var city = new Models.City.City
            {
                CityId = requestModel.CityId ?? 0,// Assuming 0 is a default value for Id
                StateId = requestModel.StateId,
                CountryId = requestModel.CountryId,
                Name = requestModel.Name,
                Description = requestModel.Description,
                CityCode = requestModel.CityCode,
                CreatedBy = requestModel.CreatedBy,
                CreatedOn = requestModel.CreatedOn,
                ModifiedBy = requestModel.ModifiedBy,
                ModifiedOn = requestModel.ModifiedOn,
                IsActive = requestModel.IsActive ?? true // Assuming true is the default value for IsActive
            };
            return city;
        }
    }
}
