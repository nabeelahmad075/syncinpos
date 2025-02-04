using Abp.Domain.Entities.Auditing;
using syncinpos.Entities.Inventory.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.POS
{
    public class POSDetail : FullAuditedEntity<long>
    {
        public POSMaster POSMaster { get; set; }
        public long POSMasterId { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal Qty { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal Amount { get; set; }
        [MaxLength(512)]
        public string Remarks { get; set; }
        public bool? Cancelled { get; set; }
        public DateTime? CancelledOn { get; set; }

    }
}
