﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Setups.Tables.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.UI;
using syncinpos.Entities.Locations.Dto;
using System.Configuration;
using Microsoft.AspNetCore.Components.Forms;

namespace syncinpos.Entities.Setups.Tables
{
    public class TableEntityAppService : AsyncCrudAppService<TableEntity, TableEntityDto>
    {
        public TableEntityAppService(
            IRepository<TableEntity, int> repository
            ) : base(repository) { }
        public async override Task<TableEntityDto> CreateAsync(TableEntityDto input)
        {
            await IsAlreadyTaken(input);
            var created = await base.CreateAsync(input);
            return created;
        }
        public async override Task<TableEntityDto> UpdateAsync(TableEntityDto input)
        {
            await IsAlreadyTaken(input);
            var updated = await base.UpdateAsync(input);
            return updated;
        }
        private async Task IsAlreadyTaken(TableEntityDto input)
        {
            if (await IsAlreadyCreated(input.Title, input.LocationId, input.FloorId, input.Id))
            {
                throw new UserFriendlyException($"Table {input.Title}, already exists!");
            }
        }
        public async Task<bool> IsAlreadyCreated(string TableName, int? LocationId = null, int? FloorId = null, int? id = null)
        {
            return await Repository.GetAll()
                                    .WhereIf(id != null && id > 0, a => a.Id != id)
                                    .Where(a => a.Title == TableName && a.LocationId == LocationId && a.FloorId == FloorId).AnyAsync();
        }
        public async Task<List<SelectItemDto>> GetTableDropdownAsync(int? LocationId)
        {
            var floors = await Repository.GetAll()
                                         .Where(a => a.IsActive == true && a.LocationId == LocationId)
                                         .Select(a => new SelectItemDto
                                         {
                                             Label = a.Title,
                                             Value = a.Id
                                         }).ToListAsync();
            return floors;
        }

        public async Task<PagedResultDto<TableHistoryDto>> GetTableHistory(TableHistoryPagedAndSortedResultRequestDto input)
        {
            var sqlQuery = CreateFilteredQuery(input)
                           .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                               a => a.Location.LocationName.Contains(input.Keyword) ||
                               a.Floor.Title.Contains(input.Keyword) ||
                               a.Title.Contains(input.Keyword) ||
                               a.Remarks.Contains(input.Keyword)
                               )
                           .Select(x => new TableHistoryDto
                           {
                               Id = x.Id,
                               LocationName = x.Location.LocationName,
                               FloorTitle = x.Floor.Title,
                               TableTitle = x.Title,
                               Remarks = x.Remarks,
                               IsActive = x.IsActive
                           });
            var sortedQuery = sqlQuery.OrderBy(x => input.Sorting);
            var pagedQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);
            return new PagedResultDto<TableHistoryDto>()
            {
                Items = await pagedQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
    }
}
