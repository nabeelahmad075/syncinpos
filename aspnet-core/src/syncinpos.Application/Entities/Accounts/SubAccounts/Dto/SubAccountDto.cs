using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using syncinpos.Entities.Accounts.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.SubAccounts.Dto
{
    [AutoMapFrom(typeof(SubAccount)), AutoMapTo(typeof(SubAccount))]
    public class SubAccountDto : FullAuditedEntityDto
    {
        public int TenantId { get; set; }
        public int MainAccountId { get; set; }
        public int AccountTypeId { get; set; }
        public string SubCode { get; set; }
        public string SubTitle { get; set; }
        public bool IsActive { get; set; }
        public bool IsControlAccount { get; set; }
    }
}
