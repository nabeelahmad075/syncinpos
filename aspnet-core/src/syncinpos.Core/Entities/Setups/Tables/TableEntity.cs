using Abp.Domain.Entities;
using syncinpos.Entities.Locations;
using syncinpos.Entities.Setups.Floors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Setups.Tables
{
    public class TableEntity : Entity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public FloorEntity Floor { get; set; }
        public int FloorId { get; set; }
        [MaxLength(32)]
        public string Title { get; set; }
        public int Sort { get; set; }
        [MaxLength(512)]
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
