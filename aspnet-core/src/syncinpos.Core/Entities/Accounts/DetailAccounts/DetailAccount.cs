using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using syncinpos.Entities.Accounts.SubAccounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.DetailAccounts
{
    public class DetailAccount : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public SubAccount SubAccount { get; set; }
        public int SubAccountId { get; set; }
        [MaxLength(16)]
        public string DetailCode { get; set; }
        [MaxLength(128)]
        public string DetailTitle { get; set; }
        public bool IsActive { get; set; }
    }
}
