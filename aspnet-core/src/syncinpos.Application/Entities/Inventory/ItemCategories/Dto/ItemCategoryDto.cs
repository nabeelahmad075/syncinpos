using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Events.Bus.Entities;
using syncinpos.Entities.Inventory.ItemTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.ItemCategories.Dto
{
    [AutoMapFrom(typeof(ItemCategory)), AutoMapTo(typeof(ItemCategory))]
    public class ItemCategoryDto : EntityDto
    {
        public int TenantId { get; set; }
        public int ItemTypeId { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }
}
