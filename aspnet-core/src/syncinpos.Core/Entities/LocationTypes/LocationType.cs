using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.LocationTypes
{
    public class LocationType : Entity
    {
        [MaxLength(50)]
        public string Title { get; set; }
    }
}
