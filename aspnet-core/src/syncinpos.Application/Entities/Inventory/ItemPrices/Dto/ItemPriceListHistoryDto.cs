using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.ItemPrices.Dto
{
    public class ItemPriceListHistoryDto
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public DateTime EffectedDate { get; set; }
    }
}
