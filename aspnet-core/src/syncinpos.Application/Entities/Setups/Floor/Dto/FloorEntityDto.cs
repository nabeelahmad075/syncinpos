using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using syncinpos.Entities.Setups.Floors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace syncinpos.Entities.Setups.Floor.Dto
{
    [AutoMapFrom(typeof(FloorEntity)), AutoMapTo(typeof(FloorEntity))]
    public class FloorEntityDto : EntityDto
    {
        public int TenantId { get; set; }
        public int LocationId { get; set; }
        public string Title { get; set; }
        public int Sort { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
