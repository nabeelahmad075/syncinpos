using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using syncinpos.Authorization;
using syncinpos.Entities.Inventory.ItemCategories;
using syncinpos.Entities.Inventory.ItemPrices.Dto;
using syncinpos.Entities.Inventory.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.ItemPrices
{
    public class ItemPriceAppService : AsyncCrudAppService<ItemPriceMaster, ItemPriceMasterDto>
    {
        public IRepository<ItemPriceMaster, int> _repository;
        public IRepository<ItemPriceDetail, long> _detailRepository;
        public IRepository<Item, int> _itemRepository;
        public ItemPriceAppService(
            IRepository<ItemPriceMaster, int> repository,
            IRepository<ItemPriceDetail, long> detailRepository,
            IRepository<Item, int> itemRepository
            ) : base(repository) 
        {
            _repository = repository;
            _detailRepository = detailRepository;
            _itemRepository = itemRepository;
        }
        public async override Task<ItemPriceMasterDto> CreateAsync(ItemPriceMasterDto input)
        {
            await DeleteRemovedDetails(input);
            await GetNewCodeIfCodeAlreadyTaken(input);
            return await base.CreateAsync(input);
        }

        public override async Task<ItemPriceMasterDto> UpdateAsync(ItemPriceMasterDto input)
        {
            await DeleteRemovedDetails(input);
            return await base.UpdateAsync(input);
        }
        private async Task DeleteRemovedDetails(ItemPriceMasterDto input)
        {
            await _detailRepository.DeleteAsync(a => !input.ItemPriceDetails.Select(b => b.Id).Contains(a.Id) && a.ItemPriceMasterId == input.Id);
        }
        public async Task<int> GetNewDocNo()
        {
            int doc = await Repository.GetAll()
                .OrderByDescending(b => b.ItemPriceNo)
                .Select(a => a.ItemPriceNo)
                .FirstOrDefaultAsync();

            int newDoc = doc + 1;

            return newDoc;
        }
        private async Task GetNewCodeIfCodeAlreadyTaken(ItemPriceMasterDto input)
        {
            if (!IsAlreadyCreated(input.ItemPriceNo, input.Id))
            {
                input.ItemPriceNo = await GetNewDocNo();
            }
        }
        public bool IsAlreadyCreated(int itemPriceNo, int? id = null)
        {
            var IsAlreadyCreated = Repository.GetAll()
                                    .WhereIf(id != null && id > 0, a => a.Id != id)
                                    .Where(a => a.ItemPriceNo == itemPriceNo).Any();
            return IsAlreadyCreated;
        }
        public async Task<PagedResultDto<ItemPriceHistoryDto>> GetItemPriceHistoryAsync(ItemPriceHistoryPagedAndSortedResultRequestDto input)
        {
            var SqlQuery = CreateFilteredQuery(input)
                                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                                a => a.ItemPriceNo.ToString().Contains(input.Keyword.ToString()) ||
                                     a.Remarks.ToLower().Contains(input.Keyword.ToLower().ToString()))
                                .Select(x => new ItemPriceHistoryDto
                                {
                                    Id = x.Id,
                                    ItemPriceNo = x.ItemPriceNo,
                                    ItemPriceDate = x.ItemPriceDate,
                                    Remarks = x.Remarks,
                                    Locations = string.Join(", ", x.ItemPriceDetails.Select(d => d.Location.LocationName))
                                });

            var sortedQuery = SqlQuery.OrderBy(x => input.Sorting);
            var pagedQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);

            return new PagedResultDto<ItemPriceHistoryDto>
            {
                Items = await pagedQuery.ToListAsync(),
                TotalCount = SqlQuery.Count()
            };
        }
        public async Task<PagedResultDto<CategoryWiseItemPriceDto>> GetCategoryWiseItemsLastPrice(ItemFilterByCategoryPagedAndSortedResultRequestDto input)
        {
            var itemsQuery = _itemRepository.GetAll()
                                            .Where(item => item.ItemCategoryId == input.ItemCategoryId)
                                            .Select(item => new
                                            {
                                                item.Id,
                                                item.ItemCategoryId,
                                                item.ItemName
                                            });

            var priceDetailsQuery = Repository.GetAll()
                                        .Where(priceMaster => priceMaster.ItemPriceDetails
                                            .Any(detail => detail.ItemCategoryId == input.ItemCategoryId && detail.LocationId == input.LocationId))
                                        .SelectMany(priceMaster => priceMaster.ItemPriceDetails
                                            .Where(detail => detail.ItemCategoryId == input.ItemCategoryId && detail.LocationId == input.LocationId)
                                            .OrderByDescending(detail => detail.EffectedDate)
                                            .Take(1)
                                            .Select(detail => new
                                            {
                                                detail.ItemId,
                                                detail.Price
                                            }));

            var sqlQuery = from item in itemsQuery
                              join price in priceDetailsQuery
                              on item.Id equals price.ItemId into priceGroup
                              from priceDetails in priceGroup.DefaultIfEmpty()
                              select new CategoryWiseItemPriceDto
                              {
                                  ItemCategoryId = item.ItemCategoryId,
                                  ItemId = item.Id,
                                  ItemName = item.ItemName,
                                  ItemPrice = priceDetails != null ? priceDetails.Price : 0
                              };

            var sortedQuery = sqlQuery.OrderBy(x => input.Sorting);
            var pagedQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);

            return new PagedResultDto<CategoryWiseItemPriceDto>
            {
                Items = await pagedQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
    }
}
