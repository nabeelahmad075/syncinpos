using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
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

            var itemsToInsert = new List<ItemPriceList>();

            foreach (var locationId in strLocationIds)
            {
                foreach (var item in input)
                {
                    itemsToInsert.Add(new ItemPriceList
                    {
                        Id = item.Id,
                        LocationId = locationId,
                        ItemCategoryId = item.ItemCategoryId,
                        ItemId = item.ItemId,
                        Price = item.Price,
                        EffectedDate = item.EffectedDate
                    });
                }
            }

            await Repository.InsertRangeAsync(itemsToInsert);

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
                                     a.ItemCategory.Title.ToLower().Contains(input.Keyword.ToLower()) ||
                                     a.Item.ItemName.ToLower().Contains(input.Keyword.ToLower())
                            ).Select(a=>new
                            {
                                a.Id,
                                a.ItemId,
                                a.LocationId,
                                a.Location.LocationName,
                                a.Item.ItemName,
                                a.EffectedDate,
                                a.Price,
                                a.ItemCategory.Title
                            }).ToList();

            var groupedQuery = SqlQuery
                .GroupBy(a => new { a.ItemId, a.LocationId })
                .Select(group => group.OrderByDescending(a => a.EffectedDate).FirstOrDefault());

            var sortedQuery = groupedQuery.OrderBy(x => input.Sorting);
            var pagedQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);

            var result = pagedQuery.Select(x => new ItemPriceListHistoryDto
            {
                Id = x.Id,
                Location = x.LocationName,
                Category = x.Title,
                ItemName = x.ItemName,
                Price = x.Price,
                EffectedDate = x.EffectedDate
            }).ToList();

            return new PagedResultDto<ItemPriceListHistoryDto>
            {
                Items = result,
                TotalCount = groupedQuery.Count()
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
        public async Task<List<ItemPriceListDto>> GetItemPriceListForReplicaAsync(int fromLocationId, int[] toLocationIds, int[] itemCatregoryIds, DateTime effectedDate)
        {
            var sqlQuery = Repository.GetAll()
                            .Where(a => a.LocationId == fromLocationId && a.EffectedDate.Date <= effectedDate.Date && itemCatregoryIds.Contains(a.ItemCategoryId))
                            .Select(a => new
                            {
                                a.ItemId,
                                a.LocationId,
                                a.EffectedDate,
                                a.Price,
                                a.ItemCategoryId
                            }).ToList();

            if (sqlQuery.Count == 0)
            {
                throw new UserFriendlyException("No ratelist found");
            }

            var groupedQuery = sqlQuery
                .GroupBy(a => new { a.ItemId })
                .Select(group => group.OrderByDescending(a => a.EffectedDate).FirstOrDefault());

            var resultQuery = groupedQuery.Select(x => new ItemPriceListDto
            {
                LocationId = x.LocationId,
                ItemCategoryId = x.ItemCategoryId,
                ItemId = x.ItemId,
                Price = x.Price,
                EffectedDate = x.EffectedDate,
                StrLocationIds = toLocationIds
            }).ToList();

            return await BulkCreateAsync(resultQuery);
        }
    }
}
