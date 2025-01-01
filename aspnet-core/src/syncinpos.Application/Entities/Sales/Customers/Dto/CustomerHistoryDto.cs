using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.Customers.Dto
{
    public class CustomerHistoryDto
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string SubAccount { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
    }
}
