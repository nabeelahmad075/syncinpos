using Abp.Domain.Entities.Auditing;
using syncinpos.Entities.Accounts.DetailAccounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.Vouchers
{
    public class VoucherDetail : FullAuditedEntity<long>
    {
        public VoucherMaster VoucherMaster { get; set; }
        public long VoucherMasterId { get; set; }
        public DetailAccount DetailAccount { get; set; }
        public int DetailAccountId { get; set; }
        [MaxLength(1024)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal DebitAmount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal CreditAmount { get; set; }
    }
}
