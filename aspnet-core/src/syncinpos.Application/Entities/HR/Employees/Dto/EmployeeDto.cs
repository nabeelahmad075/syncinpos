using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.HR.Employees.Dto
{
    [AutoMapFrom(typeof(Employee)), AutoMapTo(typeof(Employee))]
    public class EmployeeDto : EntityDto
    {
        public int TenantId { get; set; }
        public int LocationId { get; set; }
        public string EmployeeName { get; set; }
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}
