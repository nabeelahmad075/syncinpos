using syncinpos.Entities.Accounts.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.SubAccounts.Dto
{
    public class SubAccountHistoryDto
    {
        public int Id { get; set; }
        public string SubCode { get; set; }
        public string SubTitle { get; set; }
        public string AccountType { get; set; }
        public string MainAccountTitle { get; set; }
        public bool IsActive { get; set; }
    }
}
