using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using syncinpos.Authorization;
using syncinpos.Entities.Inventory.ItemPrices;
using syncinpos.Entities.Inventory.Items.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.Items
{
    public class ItemAppService : AsyncCrudAppService<Item, ItemDto>
    {
        private readonly IRepository<ItemPriceList, long> _itemPriceRepo;
        public ItemAppService(
            IRepository<Item, int> repository,
            IRepository<ItemPriceList, long> itemPriceRepo
            ) : base(repository) 
        {
            _itemPriceRepo = itemPriceRepo;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Menu_Operations_ItemInformation_Create)]
        public async override Task<ItemDto> CreateAsync(ItemDto input)
        {
            return await base.CreateAsync(input);
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Menu_Operations_ItemInformation_Update)]
        public async override Task<ItemDto> UpdateAsync(ItemDto input)
        {
            return await base.UpdateAsync(input);
        }
        public async Task<List<SelectItemDto>> GetItemDropdownAsync()
        {
            var items = await Repository.GetAll()
                                        .Where(a => a.IsActive == true)
                                        .Select(a => new SelectItemDto
                                        {
                                            Label = a.ItemName,
                                            Value = a.Id
                                        }).ToListAsync();
            return items;
        }

        public async Task<List<SelectItemDto>> GetCategoryWiseItemsListAsync(int? itemCategoryId, int? locationId, DateTime? effectedDate)
        {

            var itemsWithPrice = await _itemPriceRepo.GetAll()
                                                     .Where(a => a.LocationId == locationId && a.ItemCategoryId == itemCategoryId && a.Price > 0 && a.EffectedDate <= effectedDate)
                                                     .OrderByDescending(a => a.EffectedDate)
                                                     .Select(a => a.ItemId)
                                                     .ToListAsync();

            var items = await Repository.GetAll()
                                        .Where(a => a.IsActive == true && itemsWithPrice.Contains(a.Id))
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
                               ItemType = x.ItemType.Alias,
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
