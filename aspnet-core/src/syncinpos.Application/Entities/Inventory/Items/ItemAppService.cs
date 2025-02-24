using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using syncinpos.Authorization;
using syncinpos.Entities.Inventory.ItemCategories;
using syncinpos.Entities.Inventory.ItemPrices;
using syncinpos.Entities.Inventory.Items.Dto;
using syncinpos.Entities.Locations;
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

            var itemsResult = await _itemPriceRepo.GetAll()
                                                     .Where(a => a.LocationId == locationId && a.ItemCategoryId == itemCategoryId && a.Price > 0 && a.EffectedDate <= effectedDate)
                                                     .GroupBy(a => a.ItemId)
                                                     .Select(g => g.OrderByDescending(a => a.EffectedDate).FirstOrDefault())
                                                     .ToListAsync();

            var itemsWithPrice = itemsResult.Select(a => new
            {
                a.ItemId,
                a.Price
            }).ToList();

            var priceLookup = itemsWithPrice.ToDictionary(x => x.ItemId, x => x.Price);

            var items = await Repository.GetAll()
                                        .Where(a => a.IsActive == true && itemsWithPrice.Select(x => x.ItemId).Contains(a.Id))
                                        .Select(a => new SelectItemDto
                                        {
                                            Label = a.ItemName,
                                            Value = a.Id,
                                            Other = new
                                            {
                                                Price = priceLookup.ContainsKey(a.Id) ? priceLookup[a.Id] : 0
                                            }
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
        public async Task<PagedResultDto<SearchItemDto>> GetSearchedItemAsync(ItemSearchSortedAndResultRequestDto input)
        {
            var itemsResult = await _itemPriceRepo.GetAll()
            .Where(a => a.LocationId == input.LocationId && a.Price > 0 && a.EffectedDate <= input.EffectedDate)
            .WhereIf(input.Price > 0, a => a.Price.ToString().Contains(input.Price.ToString()))
            .GroupBy(a => a.ItemId)
                                                     .Select(g => g.OrderByDescending(a => a.EffectedDate).FirstOrDefault())
                                                     .ToListAsync();

            var itemsWithPrice = itemsResult.Select(a => new
            {
                a.ItemId,
                a.Price
            }).ToList();

            var priceLookup = itemsWithPrice.ToDictionary(x => x.ItemId, x => x.Price);

            var sqlQuery = CreateFilteredQuery(input)
                            .Where(a => itemsWithPrice.Select(p => p.ItemId).Contains(a.Id))
                            .WhereIf(!string.IsNullOrEmpty(input.Section), a => a.Section.Title.ToLower().Contains(input.Section.ToLower()))
                            .WhereIf(!string.IsNullOrEmpty(input.Category), a => a.ItemCategory.Title.ToLower().Contains(input.Category.ToLower()))
                            .WhereIf(!string.IsNullOrEmpty(input.ItemName), a => a.ItemName.ToLower().Contains(input.ItemName.ToLower()))
                            .WhereIf(!string.IsNullOrEmpty(input.Barcode), a => a.Barcode.ToString().ToLower().Contains(input.Barcode.ToString().ToLower()))
                            .WhereIf(!string.IsNullOrEmpty(input.UOM), a => a.UOM.Title.ToLower().Contains(input.UOM.ToLower()));

            var sortedQuery = ApplySorting(sqlQuery, input);
            var pagedQuery = ApplyPaging(sortedQuery, input);

            var resultQuery = pagedQuery.Select(a => new SearchItemDto
            {
                ItemId = a.Id,
                Section = a.Section.Title,
                Category = a.ItemCategory.Title,
                ItemName = a.ItemName,
                Barcode = a.Barcode.ToString(),
                UOM = a.UOM.Title,
                Price = priceLookup.ContainsKey(a.Id) ? priceLookup[a.Id] : 0
            });

            return new PagedResultDto<SearchItemDto>
            {
                Items = await resultQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
    }
}
