using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.LocationTypes.Dto
{
    [AutoMapFrom(typeof(LocationType)), AutoMapTo(typeof(LocationType))]
    public class LocationTypeDto : EntityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
