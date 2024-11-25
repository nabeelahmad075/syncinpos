using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.DetailAccounts.Dto
{
    public class DetailAccountHistoryDto
    {
        public long Id { get; set; }
        public string DetailCode { get; set; }
        public string DetailTitle { get; set; }
        public string AccountType { get; set; }
        public string SubAccount { get; set; }
        public string MainType { get; set; }
        public string MainAccount { get; set; }
        public bool IsActive { get; set; }
    }
}
