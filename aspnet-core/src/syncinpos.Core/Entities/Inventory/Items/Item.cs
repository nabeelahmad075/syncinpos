using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using syncinpos.Entities.Inventory.ItemCategories;
using syncinpos.Entities.Inventory.ItemTypes;
using syncinpos.Entities.Inventory.Sections;
using syncinpos.Entities.Inventory.Units;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.Items
{
    public class Item : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public ItemType ItemType { get; set; }
        public int ItemTypeId { get; set; }
        public ItemCategory ItemCategory { get; set; }
        public int ItemCategoryId { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
        [MaxLength(128)]
        public string ItemName { get; set; }
        [MaxLength(32)]
        public string Barcode { get; set; }
        public UnitOfMeasurement UOM { get; set; }
        public int UOMId { get; set; }
        public bool IsActive { get; set; }
    }
}
