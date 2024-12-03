using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using syncinpos.Entities.Inventory.ItemCategories;
using syncinpos.Entities.Inventory.Items;
using syncinpos.Entities.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.ItemPrices
{
    public class ItemPriceList : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public ItemCategory ItemCategory { get; set; }
        public int ItemCategoryId { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public DateTime EffectedDate { get; set; }
    }
}
