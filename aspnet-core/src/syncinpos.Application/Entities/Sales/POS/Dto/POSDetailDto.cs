using Abp.Application.Services.Dto;
using syncinpos.Entities.Inventory.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace syncinpos.Entities.Sales.POS.Dto
{
    [AutoMapFrom(typeof(POSDetail)), AutoMapTo(typeof(POSDetail))]
    public class POSDetailDto : FullAuditedEntityDto<long>
    {
        public long POSMasterId { get; set; }
        public int ItemId { get; set; }
        public decimal Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public bool? Cancelled { get; set; }
        public DateTime? CancelledOn { get; set; }
    }
}
