using Abp.Application.Services;
using Abp.Domain.Repositories;
using syncinpos.Entities.Sales.DayCloses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.DayCloses
{
    public class ReversedDayHistoryAppService : AsyncCrudAppService<DayReversedHistory, DayReversedHistoryDto, long>
    {
        public ReversedDayHistoryAppService(
            IRepository<DayReversedHistory, long> repository
            ) : base(repository)
        { }
    }
}
