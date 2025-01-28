using Abp.Application.Services;
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
using syncinpos.Entities.HR.Employees;
using Microsoft.Extensions.Primitives;

namespace syncinpos.Entities.Sales.DayCloses
{
    public class DayCloseAppService : AsyncCrudAppService<DayClose, DayCloseDto>
    {
        string ClosedStatus = "CLOSED";
        string OpenedStatus = "OPEN";
        private readonly IRepository<Employee, int> _employeeRepo;
        private readonly IRepository<DayReversedHistory, long> _dayReversedHistory;
        private readonly ReversedDayHistoryAppService _reversedDayHistoryAppService;
        public DayCloseAppService(
            IRepository<DayClose, int> repository,
            IRepository<Employee, int> employeeRepo,
            IRepository<DayReversedHistory, long> dayReversedHistory,
            ReversedDayHistoryAppService reversedDayHistoryAppService
            ) : base(repository) 
        {
            _employeeRepo = employeeRepo;
            _dayReversedHistory = dayReversedHistory;
            _reversedDayHistoryAppService = reversedDayHistoryAppService;
            ClosedStatus = "CLOSED";
            OpenedStatus = "OPEN";
        }
        public async override Task<DayCloseDto> CreateAsync(DayCloseDto input)
        {
            input.Status = OpenedStatus;
            return await base.CreateAsync(input);
        }
        public async Task<List<DayCloseDto>> BulkCreateAsync(List<DayCloseDto> input)
        {
            var daysToInsert = new List<DayClose>();

                foreach (var dayToOpen in input)
                {
                    if (dayToOpen.IsMarked == true && dayToOpen.IsReversed == false)
                    {
                        daysToInsert.Add(new DayClose
                        {
                            //Id = dayToOpen.Id,
                            TenantId = dayToOpen.TenantId,
                            LocationId = dayToOpen.LocationId,
                            CurrentDate = dayToOpen.CurrentDate.AddDays(1),
                            CreatorUserId = dayToOpen.CreatorUserId,
                            Status = OpenedStatus
                        });
                    }
                }
            foreach (var dayToClose in input)
            {
                if (dayToClose.IsMarked == true && dayToClose.IsReversed == false)
                {
                    await UpdateAsync(dayToClose);
                }
            }

            await Repository.InsertRangeAsync(daysToInsert);

            return input;
        }
        public async override Task<DayCloseDto> UpdateAsync(DayCloseDto input)
        {
            input.Status = ClosedStatus;
            return await base.UpdateAsync(input);
        }
        public async Task<List<DayCloseDto>> GetOpenDaysForLocations()
        {
            var sqlQuery = await Repository.GetAll()
                                            .Where(a => a.Status == OpenedStatus)
                                            .OrderByDescending(a => a.CurrentDate)
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
                                                CreatorUserId = x.CreatorUserId,
                                                IsReversed = x.IsReversed
                                            }).ToListAsync();
            var result = sqlQuery
                        .GroupBy(a => a.LocationId)  // Group by LocationId
                        .Select(group => group.FirstOrDefault())  // Select the first item per group
                        .Where(a => a != null)  // Ensure no nulls in the result
                        .ToList();

            return result;
        }
        public async Task<DateTime> GetOpenedDay()
        {
            var locationId = await _employeeRepo.GetAll()
                                                .Where(e => e.UserId == AbpSession.UserId)
                                                .Select(e => e.LocationId)
                                                .FirstOrDefaultAsync();

            if (locationId == 0)
            {
                return DateTime.Now;
            }

            var dayOpened = await Repository.GetAll()
                                            .Where(a => a.LocationId == locationId && a.Status == OpenedStatus && a.IsReversed == false)
                                            .Select(a => a.CurrentDate)
                                            .FirstOrDefaultAsync();
            return dayOpened;
        }
        public async Task ReverseDayAsync(List<DayCloseDto> input)
        {
            var locationIds = input.Where(a => a.IsMarked == true).Select(a => a.LocationId).Distinct().ToList();
            
            foreach (var dayToReverse in input)
            {
                if (dayToReverse.IsMarked == true && dayToReverse.IsReversed == false)
                {
                    await UpdateDayAsReversedAsync(dayToReverse);
                }
            }

            var openReversedDay = await Repository.GetAll()
                                      .Where(a => locationIds.Contains(a.LocationId) && a.IsReversed == false && a.Status == ClosedStatus)
                                      .OrderByDescending(a => a.CurrentDate)
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
                                           CreatorUserId = x.CreatorUserId,
                                           IsReversed = x.IsReversed
                                       }).ToListAsync();

            var result = openReversedDay
                        .GroupBy(a => a.LocationId)
                        .Select(group => group.FirstOrDefault())
                        .Where(a => a != null)
                        .ToList();


            foreach (var reverseDayToOpen in result)
            {
                await OpenReversedDayAsync(reverseDayToOpen);
            }

        }
        private async Task<DayCloseDto> UpdateDayAsReversedAsync(DayCloseDto input)
        {
            input.IsReversed = true;
            await MaintainDayReversedHistoryAsync(input);
            return await base.UpdateAsync(input);
        }
        private async Task<DayCloseDto> OpenReversedDayAsync(DayCloseDto input)
        {
            input.Status = OpenedStatus;
            return await base.UpdateAsync(input);
        }
        private async Task MaintainDayReversedHistoryAsync(DayCloseDto input)
        {
            var history = new DayReversedHistoryDto();
            history.TenantId = input.TenantId;
            history.DayCloseId = input.Id;

            await _reversedDayHistoryAppService.CreateAsync(history);
        }
        public async Task CloseReversedDayAsync(List<DayCloseDto> input)
        {
            //var locationIds = input.Where(a => a.IsMarked == true).Select(a => a.LocationId).Distinct().ToList();
            var reversedLocationIds = input.Where(a => a.IsMarked == true && a.IsReversed == true).Select(a => a.LocationId).Distinct().ToList();

            var preOpenedDays = await Repository.GetAll()
                                             .Where(a => reversedLocationIds.Contains(a.LocationId) && a.IsReversed == false && a.Status == OpenedStatus)
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
                                                 CreatorUserId = x.CreatorUserId,
                                                 IsReversed = x.IsReversed
                                             }).ToListAsync();

            foreach (var closeReversedDay in preOpenedDays)
            {
                await UpdateAsync(closeReversedDay);
            }

            var openedDays = await Repository.GetAll()
                                             .Where(a => reversedLocationIds.Contains(a.LocationId) && a.IsReversed == true && a.Status == OpenedStatus)
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
                                                 CreatorUserId = x.CreatorUserId,
                                                 IsReversed = x.IsReversed
                                             }).ToListAsync();

            foreach (var closeReversed in openedDays)
            {
                if (closeReversed.IsReversed == true)
                {
                    await CloseReversedAsync(closeReversed);
                }
            }
        }
        private async Task<DayCloseDto> CloseReversedAsync(DayCloseDto input)
        {
            input.IsReversed = false;
            return await base.UpdateAsync(input);
        }
    }
}
