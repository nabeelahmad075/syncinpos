using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.MainAccounts.Dto
{
    public class MainAccountHistoryDto
    {
        public int Id { get; set; }
        public string MainCode { get; set; }
        public string MainTitle { get; set; }
        public string MainType { get; set; }
        public bool IsActive { get; set; }
    }
}
