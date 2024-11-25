using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.ItemTypes
{
    public class ItemType : Entity
    {
        [MaxLength(32)]
        public string Title { get; set; }
        [MaxLength(8)]
        public string Alias { get; set; }
    }
}
