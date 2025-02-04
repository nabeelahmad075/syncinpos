using Abp.Application.Services;
using Abp.Domain.Repositories;
using syncinpos.Entities.Sales.POS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.POS
{
    public class POSAppService : AsyncCrudAppService<POSDetail, POSMasterDto, long>
    {
        public POSAppService(
            IRepository<POSDetail, long> repository
            ) : base(repository)
        {

        }

    }
}
