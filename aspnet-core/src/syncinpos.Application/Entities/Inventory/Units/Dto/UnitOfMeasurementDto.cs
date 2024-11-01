using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.Units.Dto
{
    [AutoMapFrom(typeof(UnitOfMeasurement)), AutoMapTo(typeof(UnitOfMeasurement))]
    public class UnitOfMeasurementDto : EntityDto
    {
        public string Title { get; set; }
    }
}
