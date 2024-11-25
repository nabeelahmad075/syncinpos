using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.ItemTypes.Dto
{
    [AutoMapFrom(typeof(ItemType)), AutoMapTo(typeof(ItemType))]
    public class ItemTypeDto : EntityDto
    {
        public string Title { get; set; }
        public string Alias { get; set; }
    }
}
