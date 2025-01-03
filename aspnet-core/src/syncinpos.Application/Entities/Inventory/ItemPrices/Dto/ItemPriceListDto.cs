﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using syncinpos.Entities.Inventory.ItemCategories;
using syncinpos.Entities.Inventory.Items;
using syncinpos.Entities.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.ItemPrices.Dto
{
    [AutoMapFrom(typeof(ItemPriceList)), AutoMapTo(typeof(ItemPriceList))]
    public class ItemPriceListDto : FullAuditedEntityDto<long>
    {
        public int? TenantId { get; set; }
        public int LocationId { get; set; }
        public int ItemCategoryId { get; set; }
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public DateTime EffectedDate { get; set; }
        [NotMapped]
        public int[] StrLocationIds { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
    }
}
