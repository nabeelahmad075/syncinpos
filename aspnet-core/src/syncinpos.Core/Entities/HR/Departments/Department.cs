using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.HR.Departments
{
    public class Department : Entity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
    }
}
