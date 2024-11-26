using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using syncinpos.Entities.Accounts.SubAccounts;
using syncinpos.Entities.Locations;
using syncinpos.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.Customers.Dto
{
    [AutoMapFrom(typeof(Customer)), AutoMapTo(typeof(Customer))]
    public class CustomerDto : FullAuditedEntityDto
    {
        public int TenantId { get; set; }
        public int LocationId { get; set; }
        public int? SubAccountId { get; set; }
        public int? DetailAccountId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string NIC { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Genders Gender { get; set; }
        public bool IsActive { get; set; }
    }
}
