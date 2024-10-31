using Abp.Domain.Entities;
using syncinpos.Entities.Inventory.ItemTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.ItemCategories
{
    public class ItemCategory : Entity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public ItemType ItemType { get; set; }
        public int ItemTypeId { get; set; }
        [MaxLength(64)]
        public string Title { get; set; }
        public bool IsActive { get; set; }

    }
}
