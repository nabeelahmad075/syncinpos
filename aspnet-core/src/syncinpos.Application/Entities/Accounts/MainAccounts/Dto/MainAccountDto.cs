using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using syncinpos.Entities.Accounts.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.MainAccounts.Dto
{
    [AutoMapFrom(typeof(MainAccount)), AutoMapTo(typeof(MainAccount))]
    public class MainAccountDto : FullAuditedEntityDto
    {
        public int TenantId { get; set; }
        public int LocationId { get; set; }
        public int MainTypeId { get; set; }
        public string MainCode { get; set; }
        public string MainTitle { get; set; }
        public bool IsActive { get; set; }
    }
}
