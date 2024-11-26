using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Abp.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Accounts.DetailAccounts;
using syncinpos.Entities.Sales.Customers.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using syncinpos.Entities.Accounts.DetailAccounts.Dto;
using syncinpos.Entities.Accounts.SubAccounts;

namespace syncinpos.Entities.Sales.Customers
{
    public class CustomerAppService : AsyncCrudAppService<Customer, CustomerDto>
    {
        private readonly IDetailAccountAppService iDetailAccountAppService;
        private readonly ISubAccountAppService iSubAccountAppService;
        public CustomerAppService(
            IRepository<Customer, int> repository,
            IDetailAccountAppService _iDetailAccountAppService,
            ISubAccountAppService _iSubAccountAppService
            ) : base(repository) 
        {
            iDetailAccountAppService = _iDetailAccountAppService;
            iSubAccountAppService = _iSubAccountAppService;
        }
        public async override Task<CustomerDto> CreateAsync(CustomerDto input)
        {
            await IsAlreadyTaken(input);
            await CreateDetailAccount(input);
            return await base.CreateAsync(input);
        }
        private async Task CreateDetailAccount(CustomerDto input)
        {
            var subAcc = await iSubAccountAppService.IsControlAccount(input.SubAccountId);
            if (subAcc == true && input.SubAccountId.HasValue && input.SubAccountId > 0 && input.DetailAccountId == null)
            {
                var detailAccount = ObjectMapper.Map<DetailAccountDto>(input);
                
                detailAccount.DetailCode = await iDetailAccountAppService.GetNewDetailAccountCode(input.SubAccountId);
                detailAccount.DetailTitle = input.Name;
                detailAccount.IsActive = input.IsActive;

                var savedDetailAccount = await iDetailAccountAppService.CreateAsync(detailAccount);
                
                input.DetailAccountId = savedDetailAccount.Id;
            }
        }
        private async Task UpdateDetailAccount(CustomerDto input)
        {
            var subAcc = await iSubAccountAppService.IsControlAccount(input.SubAccountId);

            var subAccountChanged = await Repository.GetAll()
                                                    .Where(a => a.Id == input.Id)
                                                    .AnyAsync(a => a.SubAccountId == input.SubAccountId);
            if (subAccountChanged == true)
            {
                await CreateDetailAccount(input);
            }
            else
            {
                if (subAcc == true && input.SubAccountId.HasValue && input.SubAccountId.Value > 0 && input.DetailAccountId != null)
                {
                    var detailAccount = ObjectMapper.Map<DetailAccountDto>(input);

                    detailAccount.DetailCode = await iDetailAccountAppService.GetDetailCodeById(input.DetailAccountId);
                    detailAccount.DetailTitle = input.Name;
                    detailAccount.IsActive = input.IsActive;

                    var updatedDetailAccount = await iDetailAccountAppService.UpdateAsync(detailAccount);

                    input.DetailAccountId = updatedDetailAccount.Id;
                }
            }
            
        }
        public async override Task<CustomerDto> UpdateAsync(CustomerDto input)
        {
            await UpdateDetailAccount(input);
            await IsAlreadyTaken(input);
            return await base.UpdateAsync(input);
        }
        public async Task IsAlreadyTaken(CustomerDto input)
        {
            if (await IsAlreadyCreated(input.Name, input.ContactNo, input.Address, input.Id))
            {
                throw new UserFriendlyException($"Customer {input.Name} already exists with same Contact# and Address.");
            }
        }
        public async Task<bool> IsAlreadyCreated(string customerName, string contactNo, string address, int? id = null)
        {
            return await Repository.GetAll()
                                   .WhereIf(id != null && id > 0, a => a.Id != id)
                                   .Where(a => a.Name == customerName && a.ContactNo == contactNo && a.Address == address)
                                   .AnyAsync();
        }
        public async Task<List<SelectItemDto>> GetCustomerDropdown()
        {
            var customers = await Repository.GetAll()
                                            .Where(a => a.IsActive == true)
                                            .Select(a => new SelectItemDto
                                            {
                                                Label = a.Name,
                                                Value = a.Id
                                            }).ToListAsync();
            return customers;
        }
        public async Task<PagedResultDto<CustomerHistoryDto>> GetCustomersHistoryAsync(CustomerHistoryPagedAndSortedResultRequestDto input)
        {
            var sqlQuery = CreateFilteredQuery(input)
                             .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                             a => a.Location.LocationName.ToLower().Contains(input.Keyword.ToLower()) ||
                             a.SubAccount.SubTitle.ToLower().Contains(input.Keyword.ToLower()) ||
                             a.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                             a.ContactNo.ToLower().Contains(input.Keyword.ToLower()) ||
                             a.Address.ToLower().Contains(input.Keyword.ToLower())
                             ).Select(x => new CustomerHistoryDto
                             { 
                                Id = x.Id,
                                Location = x.Location.LocationName,
                                SubAccount = x.SubAccount.SubTitle,
                                Name = x.Name,
                                ContactNo = x.ContactNo,
                                Address = x.Address,
                                Gender = x.Gender.ToString(),
                                IsActive = x.IsActive
                             });

            var sortedQuery = sqlQuery.OrderBy(a => input.Sorting);
            var pagedQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);

            return new PagedResultDto<CustomerHistoryDto>
            {
                Items = await pagedQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };

        }
    }
}
