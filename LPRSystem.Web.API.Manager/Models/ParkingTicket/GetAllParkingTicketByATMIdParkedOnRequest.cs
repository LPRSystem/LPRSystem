﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Models.ParkingTicket
{
    public class GetAllParkingTicketByATMIdParkedOnRequest
    {
        public long? ATMId { get; set; }
        public DateTimeOffset? ParkedOn { get; set; }
    }
}
