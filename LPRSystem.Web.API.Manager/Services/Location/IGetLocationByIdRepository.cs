using LPRSystem.Web.API.Manager.Models.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Location
{
    public interface IGetLocationByIdRepository
    {
        Task<LPRSystem.Web.API.Manager.Models.Location.Location> ExecuteAsync(GetLocationByIdRequest request);

    }
}
