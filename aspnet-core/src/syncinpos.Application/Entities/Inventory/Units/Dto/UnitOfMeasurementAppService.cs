using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Microsoft.EntityFrameworkCore;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.Units.Dto
{
    public class UnitOfMeasurementAppService : AsyncCrudAppService<UnitOfMeasurement, UnitOfMeasurementDto>
    {
        public UnitOfMeasurementAppService(
            IRepository<UnitOfMeasurement, int> repository
            ) : base(repository) { }

        public async Task<List<SelectItemDto>> GetUnitDropdownAsync()
        {
            var unit = await Repository.GetAll()
                                      .Select(a => new SelectItemDto
                                      {
                                          Label = a.Title,
                                          Value = a.Id
                                      }).ToListAsync();
            return unit;
        }
    }
}
