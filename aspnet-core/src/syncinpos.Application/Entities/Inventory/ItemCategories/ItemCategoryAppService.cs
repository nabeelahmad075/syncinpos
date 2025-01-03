﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using syncinpos.Entities.Inventory.ItemCategories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using syncinpos.Utility.SelectItemDto;
using Abp.UI;
using syncinpos.Entities.Setups.Floor.Dto;

namespace syncinpos.Entities.Inventory.ItemCategories
{
    public class ItemCategoryAppService : AsyncCrudAppService<ItemCategory, ItemCategoryDto>
    {
        public ItemCategoryAppService(
            IRepository<ItemCategory, int> repository
            ) : base(repository) { }
        public async override Task<ItemCategoryDto> CreateAsync(ItemCategoryDto input)
        {
            await IsAlreadyTaken(input);
            var created = await base.CreateAsync(input);
            return created;
        }
        public async override Task<ItemCategoryDto> UpdateAsync(ItemCategoryDto input)
        {
            await IsAlreadyTaken(input);
            var updated = await base.UpdateAsync(input);
            return updated;
        }
        private async Task IsAlreadyTaken(ItemCategoryDto input)
        {
            if (await IsAlreadyCreated(input.Title, input.Id))
            {
                throw new UserFriendlyException($"Category {input.Title}, already exists!");
            }
        }
        public async Task<bool> IsAlreadyCreated(string CategoryTitle, int? id = null)
        {
            return await Repository.GetAll()
                                    .WhereIf(id != null && id > 0, a => a.Id != id)
                                    .Where(a => a.Title == CategoryTitle).AnyAsync();
        }
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
        public async Task<List<SelectItemDto>> GetItemCategoryDropdownAsync(int? ItemId, int? ItemCategoryId, int? ItemTypeId)
        {
            var Category = await Repository.GetAll()
                                      .Where(a => ((a.IsActive == true && ItemId == 0) || ((a.IsActive == true && ItemId != 0) || a.Id == ItemCategoryId)) && a.ItemTypeId == ItemTypeId)
                                      .Select(a => new SelectItemDto
                                      {
                                          Label = a.Title,
                                          Value = a.Id
                                      }).ToListAsync();
            return Category;
        }
    }
}
