using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.ItemPrices.Dto
{
    [AutoMapFrom(typeof(ItemPriceMaster)), AutoMapTo(typeof(ItemPriceMaster))]
    public class ItemPriceMasterDto : FullAuditedEntityDto
    {
        public int TenantId { get; set; }
        public int ItemPriceNo { get; set; }
        public DateTime ItemPriceDate { get; set; }
        public string Remarks { get; set; }
        public ICollection<ItemPriceDetailDto> ItemPriceDetails { get; set; }
    }
}
