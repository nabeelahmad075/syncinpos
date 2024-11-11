using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Extensions;
using syncinpos.Entities.Inventory.ItemCategories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using syncinpos.Utility.SelectItemDto;

namespace syncinpos.Entities.Inventory.ItemCategories
{
    public class ItemCategoryAppService : AsyncCrudAppService<ItemCategory, ItemCategoryDto>
    {
        public ItemCategoryAppService(
            IRepository<ItemCategory, int> repository
            ) : base(repository) { }
        public async Task<PagedResultDto<ItemCategoryHistoryDto>> GetItemCategoryHistoryAsync(ItemCategoryHistoryPagedAndSortedResultRequestDto input)
        {
            var sqlQuery = CreateFilteredQuery(input).
                           WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                           a => a.ItemType.Title.Contains(input.Keyword) || a.Title.Contains(input.Keyword)
                           ).Select(x => new ItemCategoryHistoryDto
                           {
                               Id = x.Id,
                               ItemType = x.ItemType.Title,
                               Title = x.Title,
                               IsActive = x.IsActive
                           });
            var sortedQuery = sqlQuery.OrderBy(x => input.Sorting);
            var pageQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);
            return new PagedResultDto<ItemCategoryHistoryDto>()
            {
                Items = await pageQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
        public async Task<List<SelectItemDto>> GetItemCategoryDropdownAsync(int? ItemId, int? ItemCategoryId)
        {
            var Category = await Repository.GetAll()
                                      .Where(a => (a.IsActive == true && ItemId == 0) || ((a.IsActive == true && ItemId != 0) || a.Id == ItemCategoryId))
                                      .Select(a => new SelectItemDto
                                      {
                                          Label = a.Title,
                                          Value = a.Id
                                      }).ToListAsync();
            return Category;
        }
    }
}
