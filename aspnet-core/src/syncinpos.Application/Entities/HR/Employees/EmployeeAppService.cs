using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using syncinpos.Entities.HR.Employees.Dto;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Abp.UI;
using syncinpos.Entities.Setups.Floor.Dto;
using syncinpos.Authorization.Users;
using syncinpos.Users.Dto;
using syncinpos.Authorization.Accounts.Dto;
using syncinpos.Authorization.Accounts;
using syncinpos.Users;

namespace syncinpos.Entities.HR.Employees
{
    public class EmployeeAppService : AsyncCrudAppService<Employee, EmployeeDto>
    {
        private readonly UserAppService _userAppService;
        public EmployeeAppService(
            IRepository<Employee, int> repository,
            UserAppService userAppService
            ) : base(repository) 
        {
            _userAppService = userAppService;
        }
        public async override Task<EmployeeDto> CreateAsync(EmployeeDto input)
        {
            input.EmployeeCode = await GetNewEmployeeNoAsync(input.LocationId);
            await IsAlreadyTaken(input);
            await CreateUpdateUserByEmp(input);
            var create = await base.CreateAsync(input);
            return create;
        }
        public async override Task<EmployeeDto> UpdateAsync(EmployeeDto input)
        {
            await IsAlreadyTaken(input);
            await CreateUpdateUserByEmp(input);
            var updated = await base.UpdateAsync(input);
            return updated;
        }
        private async Task<EmployeeDto> CreateUpdateUserByEmp(EmployeeDto input)
        {
            if (input.IsUser)
            {
                if (input.UserId == 0)
                {
                    var userCreateInput = new CreateUserDto
                    {
                        Name = input.EmployeeName,
                        Surname = input.EmployeeCode,
                        EmailAddress = input.EmailAddress,
                        UserName = input.Username,
                        Password = input.Password,
                        IsActive = input.IsActive,
                        RoleNames = input.RolesNames
                    };

                    var user = await _userAppService.CreateAsync(userCreateInput);
                    input.UserId = user.Id;
                }
                else
                {
                    var userUpdateInput = new UserDto
                    {
                        Id = input.UserId.Value,
                        Name = input.EmployeeName,
                        Surname = input.EmployeeCode,
                        EmailAddress = input.EmailAddress,
                        UserName = input.Username,
                        IsActive = input.IsActive,
                        RoleNames = input.RolesNames 
                    };

                    var user = await _userAppService.UpdateAsync(userUpdateInput);
                    input.UserId = user.Id;
                }
            }
            return input;
        }
        private async Task IsAlreadyTaken(EmployeeDto input)
        {
            if (await IsAlreadyCreated(input.EmployeeCode, input.LocationId, input.Id))
            {
                throw new UserFriendlyException($"Employee Code {input.EmployeeCode}, already exists!");
            }
        }
        public async Task<bool> IsAlreadyCreated(string EmployeeCode, int? LocationId = null, int? id = null)
        {
            return await Repository.GetAll()
                                    .WhereIf(id != null && id > 0, a => a.Id != id)
                                    .Where(a => a.EmployeeCode == EmployeeCode && a.LocationId == LocationId).AnyAsync();
        }
        public async Task<string> GetNewEmployeeNoAsync(int? LocationId = null)
        {
            string strDocNo = await Repository.GetAll()
                .Where(a => a.LocationId == LocationId)
                .OrderByDescending(b => b.EmployeeCode)
                .Select(a => a.EmployeeCode)
                .FirstOrDefaultAsync();

            int newDocNumber = 1;

            if (!string.IsNullOrEmpty(strDocNo))
            {
                int maxNumber = Convert.ToInt32(strDocNo.Substring(4));
                    newDocNumber = maxNumber + 1;
            }

            string newEmployeeCode = $"Emp-{newDocNumber:D3}";

            return newEmployeeCode;
        }
        public async Task<PagedResultDto<EmployeeHistoryDto>> GetEmployeesHistory(EmployeeHistoryPagedAndSortedResultRequestDto input)
        {
            var sqlQuery = CreateFilteredQuery(input)
                            .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                            a => a.Location.LocationName.Contains(input.Keyword) || 
                            a.EmployeeCode.Contains(input.Keyword) ||
                            a.EmployeeName.Contains(input.Keyword) ||
                            a.Department.Title.Contains(input.Keyword) ||
                            a.Designation.Title.Contains(input.Keyword) ||
                            a.MobileNo.Contains(input.Keyword)
                            ).Select(x => new EmployeeHistoryDto
                            {
                                Id = x.Id,
                                LocationName = x.Location.LocationName,
                                EmployeeCode = x.EmployeeCode,
                                EmployeeName = x.EmployeeName,
                                Designation = x.Designation.Title,
                                Department = x.Department.Title,
                                MobileNo = x.MobileNo,
                                Address = x.Address,
                                IsActive = x.IsActive,
                                CreationTime = x.CreationTime,
                                LastModificationTime = x.LastModificationTime,
                                JoiningDate = x.JoiningDate
                            });
            var sortedQuery = sqlQuery.OrderBy(x => input.Sorting);
            var pageQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);
            return new PagedResultDto<EmployeeHistoryDto>()
            {
                Items = await pageQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
    }
}
