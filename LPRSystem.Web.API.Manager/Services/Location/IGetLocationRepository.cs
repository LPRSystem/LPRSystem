using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Location
{
    public interface IGetLocationRepository
    {
        Task<IEnumerable<LPRSystem.Web.API.Manager.Models.Location.Location>>ExecuteAsync();
    }
}
