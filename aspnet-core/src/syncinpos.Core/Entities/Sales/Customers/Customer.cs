using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using JetBrains.Annotations;
using syncinpos.Entities.Accounts.SubAccounts;
using syncinpos.Entities.Locations;
using syncinpos.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.Customers
{
    public class Customer : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public SubAccount SubAccount { get; set; }
        public int SubAccountId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string NIC { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Genders Gender { get; set; }
        public bool IsActive { get; set; }
    }
}
