using Abp.Application.Services;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Regions.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Regions
{
    public class RegionAppService : AsyncCrudAppService<Region, RegionDto>
    {
        public RegionAppService(
            IRepository<Region, int> repository
            ) : base(repository)
        {    
        }
        public async Task<List<SelectItemDto>> GetRegionsDropDown()
        {
            var allRegions = await Repository.GetAll().Select(a => new SelectItemDto 
            { 
                Label = a.Title,
                Value = a.Id
            }).ToListAsync();
            
            return allRegions;
        }
    }
}
