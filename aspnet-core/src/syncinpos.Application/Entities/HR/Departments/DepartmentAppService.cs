using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.HR.Departments.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.HR.Departments
{
    public class DepartmentAppService : AsyncCrudAppService<Department, DepartmentsDto>
    {
        public DepartmentAppService(
            IRepository<Department, int> repository
            ) : base(repository) { }
        public async Task<List<SelectItemDto>> GetDepartmentsDropDown()
        {
            var departments = await Repository.GetAll()
                                              .Select(a => new SelectItemDto
                                              {
                                                  Label = a.Title,
                                                  Value = a.Id
                                              }).ToListAsync();
            return departments;
        }
    }
}
