using LPRSystem.Web.API.Manager.Models.State;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Location
{
    public class LocationProcessRequestParser : IRequestParser<LPRSystem.Web.API.Manager.Models.Location.Location>
    {
        public async Task<Models.Location.Location> ParseAsync(HttpRequest request)
        {
            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.Location.LocationProcessRequest>(requestBody);
            var location = new Models.Location.Location
            {
                LocationId = requestModel.LocationId ?? 0, // Assuming 0 is a default value for Id
                LocationName = requestModel.LocationName,
                Code = requestModel.Code,
                Address = requestModel.Address,
                CountryId = requestModel.CountryId,
                StateId = requestModel.StateId,
                CityId = requestModel.CityId,
                CreatedBy = requestModel.CreatedBy,
                CreatedOn = requestModel.CreatedOn,
                ModifiedBy = requestModel.ModifiedBy,
                ModifiedOn = requestModel.ModifiedOn,
                IsActive = requestModel.IsActive ?? true // Assuming true is the default value for IsActive
            };

            return location;
        }
    }
}
