using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.Vouchers.Dto
{
    public class VoucherHistoryDto
    {
        public long Id { get; set; }
        public string Location { get; set; }
        public string VoucherType { get; set; }
        public string VoucherNo { get; set; }
        public DateTime VoucherDate { get; set; }
        public string Remarks { get; set; }
        public decimal VoucherAmount { get; set; }
    }
}
