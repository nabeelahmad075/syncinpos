using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.POS.Dto
{
    public class PendingOrdersDto
    {
        public long Id { get; set; }
        public long? InvoiceNo { get; set; }
        public int KOTNo { get; set; }
        public string Table { get; set; }
        public string Customer { get; set; }
        public DateTime? ReservedTime { get; set; }
        public decimal Amount { get; set; }
        public int DineInOrders { get; set; }
        public int TakeawayOrders { get; set; }
        public int DeliveryOrders { get; set; }
    }
}
