using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using syncinpos.Entities.HR.Departments;
using syncinpos.Entities.HR.Designations;
using syncinpos.Entities.Locations;
using System;
using System.ComponentModel.DataAnnotations;

namespace syncinpos.Entities.HR.Employees
{
    public class Employee : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        [MaxLength(8)]
        public string EmployeeCode { get; set; }
        [MaxLength(64)]
        public string EmployeeName { get; set; }
        public Designation Designation { get; set; }
        public int DesignationId { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        [MaxLength(32)]
        public string MobileNo { get; set; }
        [MaxLength(256)]
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}
