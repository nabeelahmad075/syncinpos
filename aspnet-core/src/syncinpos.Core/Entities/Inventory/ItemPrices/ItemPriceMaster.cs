using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.ItemPrices
{
    public class ItemPriceMaster : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public int ItemPriceNo { get; set; }
        public DateTime ItemPriceDate { get; set; }
        public string Remarks { get; set; }
        public ICollection<ItemPriceDetail> ItemPriceDetails { get; set; } = [];
    }
}
