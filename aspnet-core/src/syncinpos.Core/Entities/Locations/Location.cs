using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using syncinpos.Entities.Regions;
using syncinpos.Entities.LocationTypes;

namespace syncinpos.Entities.Locations
{
    public class Location : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Region Region { get; set; }
        public int RegionId { get; set; }
        public LocationType LocationType { get; set; }
        public int LocationTypeId { get; set; }
        [MaxLength(50)]
        public string LocationCode { get; set; }
        [MaxLength(100)]
        public string LocationName { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string ContactPerson { get; set; }
        [MaxLength(50)]
        public string ContactNumber { get; set; }
        public bool IsActive { get; set; }
        [MaxLength(16)]
        public string TaxTitle { get; set; }
        [MaxLength(32)]
        public string TaxRegistrationNo { get; set; }
        [Column(TypeName = "decimal(5,4)")]
        public decimal TaxPercentOnCash { get; set; }
        [Column(TypeName = "decimal(5,4)")]
        public decimal TaxPercentOnCreditCard { get; set; }
        [Column(TypeName = "decimal(5,4)")]
        public decimal TaxPercentOnCredit { get; set; }
        [Column(TypeName = "decimal(5,4)")]
        public decimal TaxPercentOnBank { get; set; }
        [MaxLength(32)]
        public string ServiceChargesLabel { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal ServiceCharges { get; set; }
        public bool EnableServicesCharges { get; set; }
        public bool FixedServicesCharges { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal DeliveryCharges { get; set; }
        public bool EnableDeliveryCharges { get; set; }
        public bool FixedDeliveryCharges { get; set; }
        [MaxLength(32)]
        public string BankChargesLabel { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal BankChargesPercent { get; set; }
        public bool EnableBankCharges { get; set; }
        [MaxLength(256)]
        public string SlipNotes { get; set; }
    }
}
