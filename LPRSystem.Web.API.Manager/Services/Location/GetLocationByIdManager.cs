using LPRSystem.Web.API.Manager.Models.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Location
{
    public class GetLocationByIdManager : IGetLocationByIdManager
    {
        private readonly IGetLocationByIdRepository _repository;
        public GetLocationByIdManager(IGetLocationByIdRepository repository)
        {
            _repository = repository;
        }

        public async Task<Models.Location.Location> ExecuteAsync(GetLocationByIdRequest request)
        {
            return await _repository.ExecuteAsync(request);
        }
    }
}
