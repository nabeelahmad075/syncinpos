using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using syncinpos.Entities.Accounts.DetailAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.Vouchers.Dto
{
    [AutoMapFrom(typeof(VoucherDetail)), AutoMapTo(typeof(VoucherDetail))]
    public class VoucherDetailDto : FullAuditedEntityDto<long>
    {
        public long VoucherMasterId { get; set; }
        public int DetailAccountId { get; set; }
        public string Description { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
    }
}
