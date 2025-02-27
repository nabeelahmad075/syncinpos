using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.Items.Dto
{
    public class ItemSearchSortedAndResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Section { get; set; }
        public string Category { get; set; }
        public string ItemName { get; set; }
        public string Barcode { get; set; }
        public string UOM { get; set; }
        public decimal? Price { get; set; }
        public int LocationId { get; set; }
        public DateTime? EffectedDate { get; set; }
    }
}
