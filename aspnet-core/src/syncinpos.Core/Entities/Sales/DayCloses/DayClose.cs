using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using syncinpos.Authorization.Users;
using syncinpos.Entities.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.DayCloses
{
    public class DayClose : FullAuditedEntity<int, User>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public DateTime CurrentDate { get; set; }
        [MaxLength(10)]
        public string Status { get; set; }
    }
}
