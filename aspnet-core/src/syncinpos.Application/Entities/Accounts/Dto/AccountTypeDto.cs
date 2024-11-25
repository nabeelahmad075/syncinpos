using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using syncinpos.Entities.Accounts.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.Dto
{
    [AutoMapFrom(typeof(AccountType)), AutoMapTo(typeof(AccountType))]
    public class AccountTypeDto : EntityDto
    {
        public string Title { get; set; }
        public string Alias { get; set; }
        public bool IsControlAccount { get; set; }
    }
}
