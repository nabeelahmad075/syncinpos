using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.SubAccounts.Dto
{
    public class SubAccountHistoryPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
       public string Keyword { get; set; }
    }
}
