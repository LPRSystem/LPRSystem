﻿using Dapper;
using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Converters;
using LPRSystem.Web.API.Manager.Models.ATMMachine;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Location
{
    public class ProcessLocationRepository :BaseRepository, IProcessLocationRepository
    {
        public ProcessLocationRepository(IConfiguration configuration) : base(configuration)
        {
            
        }

        public async Task<Models.Location.Location> ExecuteAsync(Models.Location.Location location)
        {
            return await base.QueryFirstOrDefaultAsync<Models.Location.Location>(CommonConstants.CommonDB, LocationConstants.InsertOrUpdateLocation, new { Location = location.ToDataTable().AsTableValuedParameter("[api].[Location]") }, null);
        }
    }
}
