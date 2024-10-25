using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Locations.Dto
{
    public class PagedLocationsHistoryResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
