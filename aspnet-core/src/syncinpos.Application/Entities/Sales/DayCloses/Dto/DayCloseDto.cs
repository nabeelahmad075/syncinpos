using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using syncinpos.Entities.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.DayCloses.Dto
{
    [AutoMapFrom(typeof(DayClose)), AutoMapTo(typeof(DayClose))]
    public class DayCloseDto : FullAuditedEntityDto
    {
        public int TenantId { get; set; }
        public int LocationId { get; set; }
        public DateTime CurrentDate { get; set; }
        public string Status { get; set; }
        [NotMapped]
        public string LocationName { get; set; }
        [NotMapped]
        public string Address { get; set; }
        [NotMapped]
        public string Region { get; set; }
        [NotMapped]
        public string LocationType { get; set; }
        [NotMapped]
        public DateTime LastDayClosed { get; set; }
        [NotMapped]
        public DateTime? ClosedOn { get; set; }
        [NotMapped]
        public string ClosedBy { get; set; }
        [NotMapped]
        public bool IsMarked { get; set; }
    }
}
