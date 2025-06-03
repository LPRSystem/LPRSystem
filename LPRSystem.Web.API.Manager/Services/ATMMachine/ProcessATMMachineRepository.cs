using Dapper;
using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Converters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ATMMachine
{
    public class ProcessATMMachineRepository : BaseRepository, IProcessATMMachineRepository
    {

        public ProcessATMMachineRepository(IConfiguration configuration) : base(configuration) 
        { 

        }

        public async Task<Models.ATMMachine.ATMMachine> ExecuteAsync(Models.ATMMachine.ATMMachine atmmachine)
        {
            string userId = "1111";
            return await base.ExecuteScalarAsync<Models.ATMMachine.ATMMachine>(CommonConstants.CommonDB, ATMMachineConstants.InsertOrUpdateATMMAchine, new { ATMMachine = atmmachine.ToDataTable(userId).AsTableValuedParameter("[api].[ATMMachine]") }, null);
        }
    }
}
