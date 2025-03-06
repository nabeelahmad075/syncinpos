using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using syncinpos.Entities.HR.Employees;
using syncinpos.Entities.Locations;
using syncinpos.Entities.Sales.Customers;
using syncinpos.Entities.Setups.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.POS
{
    public class POSMaster : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long InvoiceNo { get; set; }
        public int OrderNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public bool? IsInvoiced { get; set; }
        public DateTime? PrintDate { get; set; }
        public int ServiceTypeId { get; set; }
        public int? PaymentMode { get; set; }
        public int? CoverTable { get; set; }
        public TableEntity Table { get; set; }
        public int? TableId { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal DeliveryChargesPer { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal DeliveryCharges { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal ServiceChargesPer { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal ServiceCharges { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal BankChargesPer { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal BankCharges { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal SalesTaxPer { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal SalesTaxAmount { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal DiscountPer { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal DiscountAmount { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal GrossAmount { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal NetAmount { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal PaymentIn { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal Balance { get; set; }
        public ICollection<POSDetail> POSDetails { get; set; } = [];
    }
}
