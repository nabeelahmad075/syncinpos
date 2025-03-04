using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.POS.Dto
{
    public class PendingOrdersCountDto
    {
        public int DineInOrders { get; set; }
        public int TakeawayOrders { get; set; }
        public int DeliveryOrders { get; set; }
        public int TotalPendingOrders { get; set; }
    }
}
