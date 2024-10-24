using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.HR.Designations.Dto
{
    [AutoMapFrom(typeof(Designation)), AutoMapTo(typeof(Designation))]
    public class DesignationsDto : EntityDto
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Title { get; set; }
    }
}
