using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.ItemCategories.Dto
{
    public class ItemCategoryHistoryDto
    {
        public int Id { get; set; }
        public string ItemType { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }
}
