using LPRSystem.Web.API.Manager.Constants;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Country
{
    public class GetCountryRepository : BaseRepository, IGetCountryRepository
    {
        public GetCountryRepository(IConfiguration configuration) : base(configuration)  
        { 

        }

        public async Task<IEnumerable<Models.Country.Country>>ExecuteAsync()
        {
            return await base.QueryAsync<Models.Country.Country>(CommonConstants.CommonDB, CountryConstant.GetCountry, null, null);
        }
    }
}
