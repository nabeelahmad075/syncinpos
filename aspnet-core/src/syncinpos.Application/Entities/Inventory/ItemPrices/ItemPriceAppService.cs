using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using syncinpos.Authorization;
using syncinpos.Entities.Inventory.ItemCategories;
using syncinpos.Entities.Inventory.ItemPrices.Dto;
using syncinpos.Entities.Inventory.Items;
using syncinpos.Entities.Locations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.ItemPrices
{
    public class ItemPriceAppService : AsyncCrudAppService<ItemPriceList, ItemPriceListDto, long>
    {
        public IRepository<Item, int> _itemRepository;
        public ItemPriceAppService(
            IRepository<ItemPriceList, long> repository,
            IRepository<Item, int> itemRepository
            ) : base(repository) 
        {
            _itemRepository = itemRepository;
        }
        public async Task<List<ItemPriceListDto>> BulkCreateAsync(List<ItemPriceListDto> input)
        {
            var strLocationIds = input.Select(a => a.StrLocationIds).FirstOrDefault();
            foreach (var locationId in strLocationIds)
            {
                var itemsWithLocationId = input.Select(item =>
                {
                    item.LocationId = locationId;
                    return item;
                }).ToList();

                var itemsToInsert = itemsWithLocationId
                    .Select(item => new ItemPriceList
                    {
                        Id = item.Id,
                        LocationId = item.LocationId, 
                        ItemCategoryId = item.ItemCategoryId,
                        ItemId = item.ItemId,
                        Price = item.Price,
                        EffectedDate = item.EffectedDate
                    })
                    .ToArray();
                await Repository.InsertRangeAsync(itemsToInsert);
            }

            return input;
        }
        public async override Task<ItemPriceListDto> CreateAsync(ItemPriceListDto input)
        {

            foreach (var locationId in input.StrLocationIds)
            {
                input.LocationId = locationId;
                await base.CreateAsync(input);
            }

            return input;
        }
        public async Task<PagedResultDto<ItemPriceListHistoryDto>> GetItemPriceHistoryAsync(ItemPriceListHistoryPagedAndSortedResultRequestDto input)
        {
            var SqlQuery = CreateFilteredQuery(input)
                                .Where(a => a.ItemCategoryId == input.CategoryId && input.LocationIds.Contains(a.LocationId))
                                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                                a => a.Location.LocationName.ToString().Contains(input.Keyword.ToString()) ||
                                     a.ItemCategory.Title.ToLower().Contains(input.Keyword.ToLower().ToString()) ||
                                     a.Item.ItemName.ToLower().Contains(input.Keyword.ToLower().ToString())
                                     )
                                .Select(x => new ItemPriceListHistoryDto
                                {
                                    Location = x.Location.LocationName,
                                    Category = x.ItemCategory.Title,
                                    ItemName = x.Item.ItemName,
                                    Price = x.Price,
                                    EffectedDate = x.EffectedDate
                                });

            var sortedQuery = SqlQuery.OrderBy(x => input.Sorting);
            var pagedQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);

            return new PagedResultDto<ItemPriceListHistoryDto>
            {
                Items = await pagedQuery.ToListAsync(),
                TotalCount = SqlQuery.Count()
            };
        }
        public async Task<List<ItemPriceListDto>> GetCategoryWiseItems(int itemCategoryId)
        {
            var itemsQuery = await _itemRepository.GetAll()
                                            .Where(item => item.ItemCategoryId == itemCategoryId)
                                            .Select(item => new ItemPriceListDto
                                            {
                                                TenantId = AbpSession.TenantId,
                                                LocationId = 0,
                                                ItemId = item.Id,
                                                ItemCategoryId = item.ItemCategoryId,
                                                ItemName = item.ItemName,
                                                Price = 0,
                                                EffectedDate = DateTime.Now
                                            }).ToListAsync();

            return itemsQuery;
        }
    }
}
