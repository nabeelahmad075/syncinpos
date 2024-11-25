using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Regions.Dto
{
    [AutoMapFrom(typeof(Region)), AutoMapTo(typeof(Region))]
    public class RegionDto : EntityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
