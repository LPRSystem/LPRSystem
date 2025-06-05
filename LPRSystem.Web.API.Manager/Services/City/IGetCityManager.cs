using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.City
{
    public interface IGetCityManager
    {
        Task<IEnumerable<LPRSystem.Web.API.Manager.Models.City.City>> ExecuteAsync();

    }
}
