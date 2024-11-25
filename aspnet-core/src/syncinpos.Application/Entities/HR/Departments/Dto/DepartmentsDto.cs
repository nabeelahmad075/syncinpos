using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.HR.Departments.Dto
{
    [AutoMapFrom(typeof(Department)), AutoMapTo(typeof(Department))]
    public class DepartmentsDto : EntityDto
    {
        public int TenantId { get; set; }
        public string Title { get; set; }
    }
}
