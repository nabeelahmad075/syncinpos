using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Inventory.Items.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.Items
{
    public class ItemAppService : AsyncCrudAppService<Item, ItemDto>
    {
        public ItemAppService(
            IRepository<Item, int> repository
            ) : base(repository) { }

        public async Task<List<SelectItemDto>> GetItemDropdownAsync()
        {
            var items = await Repository.GetAll()
                                        .Select(a => new SelectItemDto
                                        {
                                            Label = a.ItemName,
                                            Value = a.Id
                                        }).ToListAsync();
            return items;
        }

        public async Task<PagedResultDto<ItemHistoryDto>> GetItemHistory(ItemHistoryPagedAndSortedResultRequestDto input)
        {
            var sqlQuery = CreateFilteredQuery(input)
                           .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                               a => a.ItemType.Title.Contains(input.Keyword) ||
                               a.ItemCategory.Title.Contains(input.Keyword) ||
                               a.Section.Title.Contains(input.Keyword) ||
                               a.ItemName.Contains(input.Keyword) ||
                               a.Barcode.Contains(input.Keyword))
                           .Select(x => new ItemHistoryDto
                           {
                               Id = x.Id,
                               ItemType = x.ItemType.Title,
                               ItemName = x.ItemName,
                               ItemCategory = x.ItemCategory.Title,
                               Section = x.Section.Title,
                               Barcode = x.Barcode,
                               UOM = x.UOM.Title,
                               IsActive = x.IsActive
                           });
            var sortedQuery = sqlQuery.OrderBy(x => input.Sorting);
            var pagedQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);
            return new PagedResultDto<ItemHistoryDto>()
            {
                Items = await pagedQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
    }
}
