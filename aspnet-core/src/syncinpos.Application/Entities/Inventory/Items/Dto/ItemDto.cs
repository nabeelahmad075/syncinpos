using Abp.Application.Services.Dto;
using Abp.AutoMapper;
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
    [AutoMapFrom(typeof(Item)), AutoMapTo(typeof(Item))]
    public class ItemDto : EntityDto
    {
        public int TenantId { get; set; }
        public int ItemTypeId { get; set; }
        public int ItemCategoryId { get; set; }
        public int SectionId { get; set; }
        public string ItemName { get; set; }
        public string Barcode { get; set; }
        public int UOMId { get; set; }
        public bool IsActive { get; set; }
    }
}
