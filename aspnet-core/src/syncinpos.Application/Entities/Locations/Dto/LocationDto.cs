using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using syncinpos.Entities.LocationTypes;
using syncinpos.Entities.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Locations.Dto
{
    [AutoMapFrom(typeof(Location)), AutoMapTo(typeof(Location))]
    public class LocationDto : EntityDto
    {
        public int TenantId { get; set; }
        [NotMapped]
        public string TenantTitle { get; set; }
        public int RegionId { get; set; }
        public int LocationTypeId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
