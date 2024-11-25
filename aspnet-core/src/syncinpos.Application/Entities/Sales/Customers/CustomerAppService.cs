using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Sales.Customers.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.Customers
{
    public class CustomerAppService : AsyncCrudAppService<Customer, CustomerDto>
    {
        public CustomerAppService(
            IRepository<Customer, int> repository
            ) : base(repository) 
        { 
        
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
