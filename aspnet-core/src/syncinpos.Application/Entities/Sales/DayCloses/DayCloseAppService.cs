﻿using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Inventory.ItemPrices.Dto;
using syncinpos.Entities.Inventory.ItemPrices;
using syncinpos.Entities.Sales.DayCloses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.DayCloses
{
    public class DayCloseAppService : AsyncCrudAppService<DayClose, DayCloseDto>
    {
        public DayCloseAppService(
            IRepository<DayClose, int> repository
            ) : base(repository) 
        {}
        public async override Task<DayCloseDto> CreateAsync(DayCloseDto input)
        {
            input.Status = "OPEN";
            return await base.CreateAsync(input);
        }
        public async Task<List<DayCloseDto>> BulkCreateAsync(List<DayCloseDto> input)
        {
            var daysToInsert = new List<DayClose>();

                foreach (var dayToOpen in input)
                {
                    if (dayToOpen.IsMarked == true)
                    {
                        daysToInsert.Add(new DayClose
                        {
                            //Id = dayToOpen.Id,
                            TenantId = dayToOpen.TenantId,
                            LocationId = dayToOpen.LocationId,
                            CurrentDate = dayToOpen.CurrentDate.AddDays(1),
                            CreatorUserId = dayToOpen.CreatorUserId,
                            Status = "OPEN"
                        });
                    }
                }
            foreach (var dayToClose in input)
            {
                if (dayToClose.IsMarked == true)
                {
                    await UpdateAsync(dayToClose);
                }
            }

            await Repository.InsertRangeAsync(daysToInsert);

            return input;
        }
        public async override Task<DayCloseDto> UpdateAsync(DayCloseDto input)
        {
            input.Status = "CLOSED";
            return await base.UpdateAsync(input);
        }
        public async Task<List<DayCloseDto>> GetOpenDaysForLocations()
        {
            var sqlQuery = await Repository.GetAll()
                                            .Where(a => a.Status == "OPEN")
                                            .Select(x => new DayCloseDto
                                            {
                                                Id = x.Id,
                                                TenantId = x.TenantId,
                                                LocationId = x.LocationId,
                                                Status = x.Status,
                                                LocationName = x.Location.LocationName,
                                                Address = x.Location.Address,
                                                Region = x.Location.Region.Title,
                                                LocationType = x.Location.LocationType.Title,
                                                CurrentDate = x.CurrentDate,
                                                LastDayClosed = x.CurrentDate.AddDays(-1),
                                                ClosedOn = x.CreationTime,
                                                ClosedBy = x.CreatorUser.FullName,
                                                CreatorUserId = x.CreatorUserId
                                            }).ToListAsync();
            return sqlQuery.ToList();
        }
    }
}