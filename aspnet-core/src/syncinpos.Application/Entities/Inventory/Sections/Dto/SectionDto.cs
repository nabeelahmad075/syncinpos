using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.Sections.Dto
{
    [AutoMapFrom(typeof(Section)), AutoMapTo(typeof(Section))]
    public class SectionDto : EntityDto
    {
        public int TenantId { get; set; }
        public string Title { get; set; }
    }
}
