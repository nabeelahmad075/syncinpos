using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.ItemPrices.Dto
{
    public class ItemFilterByCategoryPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        public int ItemCategoryId { get; set; }
        public int LocationId { get; set; }
    }
}
