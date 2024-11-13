using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using syncinpos.Entities.LocationTypes;
using syncinpos.Entities.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Locations.Dto
{
    [AutoMapFrom(typeof(Location)), AutoMapTo(typeof(Location))]
    public class LocationDto : EntityDto
    {
        public int TenantId { get; set; }
        [NotMapped]
        public string TenantTitle { get; set; }
        public int RegionId { get; set; }
        public int LocationTypeId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public bool IsActive { get; set; }
        public string TaxTitle { get; set; }
        public string TaxRegistrationNo { get; set; }
        public decimal TaxPercentOnCash { get; set; }
        public decimal TaxPercentOnCreditCard { get; set; }
        public decimal TaxPercentOnCredit { get; set; }
        public decimal TaxPercentOnBank { get; set; }
        public string ServiceChargesLabel { get; set; }
        public decimal ServiceCharges { get; set; }
        public bool EnableServicesCharges { get; set; }
        public bool FixedServicesCharges { get; set; }
        public decimal DeliveryCharges { get; set; }
        public bool EnableDeliveryCharges { get; set; }
        public bool FixedDeliveryCharges { get; set; }
        public string BankChargesLabel { get; set; }
        public decimal BankChargesPercent { get; set; }
        public bool EnableBankCharges { get; set; }
        public string SlipNotes { get; set; }
    }
}
