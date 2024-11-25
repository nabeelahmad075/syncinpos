using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.HR.Designations
{
    public class Designation : Entity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
    }
}
