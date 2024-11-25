using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.DetailAccounts.Dto
{
    [AutoMapFrom(typeof(DetailAccount)), AutoMapTo(typeof(DetailAccount))]
    public class DetailAccountDto : FullAuditedEntityDto
    {
        public int TenantId { get; set; }
        public int SubAccountId { get; set; }
        public string DetailCode { get; set; }
        public string DetailTitle { get; set; }
        public bool IsActive { get; set; }
    }
}
