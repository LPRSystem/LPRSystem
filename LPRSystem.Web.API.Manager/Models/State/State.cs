using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Models.State
{
    public class State
    {
        public long StateId { get; set; }
        public long? CountryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? StateCode { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
