using Abp.Application.Services;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.HR.Designations.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.HR.Designations
{
    public class DesignationAppService : AsyncCrudAppService<Designation, DesignationsDto>
    {
        public DesignationAppService(
            IRepository<Designation, int> repository
            ) : base(repository) { }
        public async Task<List<SelectItemDto>> GetDesignationDropdown()
        {
            var designations = await Repository.GetAll()
                                                .Select(a => new SelectItemDto
                                                {
                                                    Label = a.Title,
                                                    Value = a.Id
                                                }).ToListAsync();
            return designations;
        }
    }
}
