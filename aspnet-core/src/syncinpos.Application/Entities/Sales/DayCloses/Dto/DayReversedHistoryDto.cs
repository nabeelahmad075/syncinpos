using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.DayCloses.Dto
{
    [AutoMapFrom(typeof(DayReversedHistory)), AutoMapTo(typeof(DayReversedHistory))]
    public class DayReversedHistoryDto : FullAuditedEntityDto<long>
    {
        public int TenantId { get; set; }
        public DayClose DayClose { get; set; }
        public int DayCloseId { get; set; }
    }
}
