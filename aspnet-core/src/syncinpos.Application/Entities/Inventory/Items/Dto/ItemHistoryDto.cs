using syncinpos.Entities.Inventory.ItemCategories;
using syncinpos.Entities.Inventory.ItemTypes;
using syncinpos.Entities.Inventory.Units;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.Items.Dto
{
    public class ItemHistoryDto 
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string ItemType { get; set; }
        public string ItemCategory { get; set; }
        public string Section { get; set; }
        public string ItemName { get; set; }
        public string Barcode { get; set; }
        public string UOM { get; set; }
        public bool IsActive { get; set; }
    }
}
