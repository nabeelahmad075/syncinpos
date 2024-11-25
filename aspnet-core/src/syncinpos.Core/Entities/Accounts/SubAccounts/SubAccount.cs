using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using syncinpos.Entities.Accounts.MainAccounts;
using syncinpos.Entities.Accounts.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.SubAccounts
{
    public class SubAccount : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public MainAccount MainAccount { get; set; }
        public int MainAccountId { get; set; }
        public AccountType AccountType { get; set; }
        public int AccountTypeId { get; set; }
        [MaxLength(8)]
        public string SubCode { get; set; }
        [MaxLength(64)]
        public string SubTitle { get; set; }
        public bool IsActive { get; set; }
    }
}
