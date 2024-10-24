using Abp.Application.Services;
using Abp.Domain.Repositories;
using syncinpos.Entities.HR.Designations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.HR.Designations
{
    public class DesignationAppService : AsyncCrudAppService<Designation, DesignationsDto>
    {
        public DesignationAppService(
            IRepository<Designation, int> repository
            ) : base(repository) { }
    }
}
