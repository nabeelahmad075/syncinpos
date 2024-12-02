using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.ItemPrices.Dto
{
    public class ItemPriceHistoryDto
    {
        public int Id { get; set; }
        public int ItemPriceNo { get; set; }
        public DateTime ItemPriceDate { get; set; }
        public string Remarks { get; set; }
        public string Locations { get; set; }
    }
}
