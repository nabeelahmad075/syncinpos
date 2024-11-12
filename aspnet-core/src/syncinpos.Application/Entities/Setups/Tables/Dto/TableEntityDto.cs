using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using syncinpos.Entities.Locations;
using syncinpos.Entities.Setups.Floors;
using syncinpos.Entities.Setups.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Setups.Tables.Dto
{
    [AutoMapFrom(typeof(TableEntity)), AutoMapTo(typeof(TableEntity))]
    public class TableEntityDto : EntityDto
    {
        public int TenantId { get; set; }
        public int LocationId { get; set; }
        public int FloorId { get; set; }
        public string Title { get; set; }
        public int Sort { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
