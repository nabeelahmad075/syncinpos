using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using syncinpos.Authorization;
using syncinpos.Entities.Inventory.ItemCategories;
using syncinpos.Entities.Inventory.ItemPrices.Dto;
using syncinpos.Entities.Inventory.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public override async Task<ItemPriceMasterDto> GetAsync(EntityDto<int> input)
        {
            return await _repository.GetAll()
                                   .Where(a => a.Id == input.Id)
                                   .Select(a => new ItemPriceMasterDto
                                   {
                                       Id = a.Id,
                                       ItemPriceNo = a.ItemPriceNo,
                                       ItemPriceDate = a.ItemPriceDate,
                                       Remarks = a.Remarks,
                                       ItemPriceDetails = a.ItemPriceDetails.Select(d => new ItemPriceDetailDto
                                       {
                                           Id = d.Id,
                                           ItemPriceMasterId = d.ItemPriceMasterId,
                                           LocationId = d.LocationId,
                                           ItemCategoryId = d.ItemCategoryId,
                                           ItemId = d.ItemId,
                                           Price = d.Price,
                                           EffectedDate = d.EffectedDate
                                       }).ToList()
                                   }).FirstOrDefaultAsync();
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
                                    Locations = string.Join(", ", x.ItemPriceDetails
                                                .Where(d => d.Location != null)
                                                .Select(d => d.Location.LocationName)
                                                .Distinct()
                                                .OrderBy(l => l)
                                            )
                                });

            var sortedQuery = SqlQuery.OrderBy(x => input.Sorting);
            var pagedQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);

            return new PagedResultDto<ItemPriceHistoryDto>
            {
                Items = await pagedQuery.ToListAsync(),
                TotalCount = SqlQuery.Count()
            };
        }
        //public async Task<PagedResultDto<CategoryWiseItemPriceDto>> GetCategoryWiseItemsLastPrice(ItemFilterByCategoryPagedAndSortedResultRequestDto input)
        //{
        //    var itemsQuery = _itemRepository.GetAll()
        //                                    .Where(item => item.ItemCategoryId == input.ItemCategoryId)
        //                                    .Select(item => new
        //                                    {
        //                                        item.Id,
        //                                        item.ItemCategoryId,
        //                                        item.ItemName
        //                                    });

        //    var priceDetailsQuery = Repository.GetAll()
        //                                    .Where(priceMaster => priceMaster.ItemPriceDetails
        //                                        .Any(detail => detail.ItemCategoryId == input.ItemCategoryId && detail.LocationId == input.LocationId))
        //                                    .SelectMany(priceMaster => priceMaster.ItemPriceDetails
        //                                        .Where(detail => detail.ItemCategoryId == input.ItemCategoryId && detail.LocationId == input.LocationId)
        //                                        .OrderByDescending(detail => detail.EffectedDate)
        //                                        .Take(1)
        //                                        .Select(detail => new
        //                                        {
        //                                            detail.ItemId,
        //                                            detail.Price
        //                                        }));

        //    var priceDetailsList = priceDetailsQuery.ToList();
        //    var resultList = new List<CategoryWiseItemPriceDto>();

        //    foreach (var item in itemsQuery)
        //    {
        //        var matchingPrice = priceDetailsList
        //            .FirstOrDefault(price => price.ItemId == item.Id);

        //        resultList.Add(new CategoryWiseItemPriceDto
        //        {
        //            ItemCategoryId = item.ItemCategoryId,
        //            ItemId = item.Id,
        //            ItemName = item.ItemName,
        //            ItemPrice = matchingPrice != null ? matchingPrice.Price : 0
        //        });
        //    }

        //    var sortedQuery = resultList.OrderBy(x => input.Sorting);
        //    var pagedQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);

        //    return new PagedResultDto<CategoryWiseItemPriceDto>
        //    {
        //        Items = pagedQuery.ToList(),
        //        TotalCount = resultList.Count()
        //    };
        //}
        public async Task<List<CategoryWiseItemPriceDto>> GetCategoryWiseItems(ItemFilterByCategoryPagedAndSortedResultRequestDto input)
        {
            var itemsQuery = await _itemRepository.GetAll()
                                            .Where(item => item.ItemCategoryId == input.ItemCategoryId)
                                            .Select(item => new CategoryWiseItemPriceDto
                                            {
                                                ItemId = item.Id,
                                                ItemCategoryId = item.ItemCategoryId,
                                                ItemName = item.ItemName,
                                                ItemPrice = 0
                                            }).ToListAsync();

            return itemsQuery;
        }
    }
}
