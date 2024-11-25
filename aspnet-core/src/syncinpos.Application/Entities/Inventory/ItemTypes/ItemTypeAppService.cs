using Abp.Application.Services;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Inventory.ItemTypes.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.ItemTypes
{
    public class ItemTypeAppService : AsyncCrudAppService<ItemType, ItemTypeDto>
    {
        public ItemTypeAppService(
            IRepository<ItemType, int> repository
            ) : base(repository) { }
        public async Task<List<SelectItemDto>> GetItemTypeDropdown()
        {
            var itemTypes = await Repository.GetAll()
                                            .Select(a => new SelectItemDto
                                            {
                                                Label = a.Title,
                                                Value = a.Id
                                            }).ToListAsync();
            return itemTypes;
        }
    }
}
