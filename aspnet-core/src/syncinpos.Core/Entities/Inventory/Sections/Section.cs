using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.Sections
{
    public class Section : Entity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        [MaxLength(64)]
        public string Title { get; set; }
    }
}
