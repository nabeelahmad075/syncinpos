using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.DayCloses
{
    public class DayReversedHistory : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public DayClose DayClose { get; set; }
        public int DayCloseId { get; set; }
    }
}
