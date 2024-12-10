using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using syncinpos.Entities.Accounts.Types;
using syncinpos.Entities.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.Vouchers.Dto
{
    [AutoMapFrom(typeof(VoucherMaster)), AutoMapTo(typeof(VoucherMaster))]
    public class VoucherMasterDto : FullAuditedEntityDto<long>
    {
        public int TenantId { get; set; }
        public int LocationId { get; set; }
        public string VoucherNo { get; set; }
        public DateTime VoucherDate { get; set; }
        public int VoucherTypeId { get; set; }
        public string Remarks { get; set; }
        public ICollection<VoucherDetailDto> VoucherDetails { get; set; } = [];
    }
}
