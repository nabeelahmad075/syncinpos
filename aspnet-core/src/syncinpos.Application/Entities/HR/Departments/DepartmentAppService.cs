using Abp.Application.Services;
using Abp.Domain.Repositories;
using syncinpos.Entities.HR.Departments.Dto;
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

    }
}
