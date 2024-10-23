using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using syncinpos.Entities.Regions;
using syncinpos.Entities.LocationTypes;

namespace syncinpos.Entities.Locations
{
    public class Location : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Region Region { get; set; }
        public int RegionId { get; set; }
        public LocationType LocationType { get; set; }
        public int LocationTypeId { get; set; }
        [MaxLength(50)]
        public string LocationCode { get; set; }
        [MaxLength(100)]
        public string LocationName { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string ContactPerson { get; set; }
        [MaxLength(50)]
        public string ContactNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
