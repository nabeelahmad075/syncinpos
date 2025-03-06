using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using syncinpos.Entities.HR.Employees;
using syncinpos.Entities.Locations;
using syncinpos.Entities.Sales.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.POS.Dto
{
    [AutoMapFrom(typeof(POSMaster)), AutoMapTo(typeof(POSMaster))]
    public class POSMasterDto : FullAuditedEntityDto<long>
    {
        public int TenantId { get; set; }
        public long InvoiceNo { get; set; }
        public int OrderNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int LocationId { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public bool? IsInvoiced { get; set; }
        public DateTime? PrintDate { get; set; }
        public int ServiceTypeId { get; set; }
        public int? PaymentMode { get; set; }
        public int? CoverTable { get; set; }
        public int? TableId { get; set; }
        public decimal DeliveryChargesPer { get; set; }
        public decimal DeliveryCharges { get; set; }
        public decimal ServiceChargesPer { get; set; }
        public decimal ServiceCharges { get; set; }
        public decimal BankChargesPer { get; set; }
        public decimal BankCharges { get; set; }
        public decimal SalesTaxPer { get; set; }
        public decimal SalesTaxAmount { get; set; }
        public decimal DiscountPer { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal PaymentIn { get; set; }
        public decimal Balance { get; set; }
        [NotMapped]
        public string TableName { get; set; }
        public ICollection<POSDetailDto> POSDetails { get; set; } = [];
    }
}
