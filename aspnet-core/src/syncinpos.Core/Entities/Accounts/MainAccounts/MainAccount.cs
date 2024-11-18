using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using syncinpos.Entities.Accounts.Types;
using syncinpos.Entities.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.MainAccounts
{
    public class MainAccount : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public MainType MainType { get; set; }
        public int MainTypeId { get; set; }
        [MaxLength(4)]
        public string MainCode { get; set; }
        [MaxLength(64)]
        public string MainTitle { get; set; }
        public bool IsActive { get; set; }
    }
}
