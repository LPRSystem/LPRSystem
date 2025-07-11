﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Constants
{
    public class ATMMachineConstants
    {
        public static string GetATMMachinesData => new string("[api].[uspGetATMMachinesData]");

        public static string GetATMMachine => new string("[api].[uspGetATMMachine]");

        public static string InsertOrUpdateATMMAchine => new string("[api].[uspInsertOrUpdateATMMAchine]");
    }
}
