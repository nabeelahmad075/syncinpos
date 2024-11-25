using Abp.Application.Services;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.LocationTypes.Dto;
using syncinpos.Entities.Regions.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.LocationTypes
{
    public class LocationTypeAppService : AsyncCrudAppService<LocationType, LocationTypeDto>
    {
        public LocationTypeAppService(
            IRepository<LocationType, int> repository
            ) : base(repository)
        {
        }
        public async Task<List<SelectItemDto>> GetLocationTypesDropDown()
        {
            var allLocationTypes = await Repository.GetAll().Select(a => new SelectItemDto
            {
                Label = a.Title,
                Value = a.Id
            }).ToListAsync();

            return allLocationTypes;
        }
    }
}
