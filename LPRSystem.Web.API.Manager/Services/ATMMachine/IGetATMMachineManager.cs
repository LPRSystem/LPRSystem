﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ATMMachine
{
    public interface IGetATMMachineManager
    {
        Task<IEnumerable<LPRSystem.Web.API.Manager.Models.ATMMachine.ATMMachinesData>> ExecuteAsync();

    }
}
