using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Threading;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Sales.POS.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.POS
{
    public class POSAppService : AsyncCrudAppService<POSMaster, POSMasterDto, long>
    {
        private readonly IRepository<POSDetail, long> _detailRepo;
        public POSAppService(
            IRepository<POSMaster, long> repository,
            IRepository<POSDetail, long> detailRepo
            ) : base(repository)
        {
            _detailRepo = detailRepo;
        }
        public async override Task<POSMasterDto> CreateAsync(POSMasterDto input)
        {
            await DeleteRemovedDetails(input);
            return await base.CreateAsync(input);
        }
        public async override Task<POSMasterDto> UpdateAsync(POSMasterDto input)
        {
            await DeleteRemovedDetails(input);
            return await base.UpdateAsync(input);
        }
        public async Task DeleteRemovedDetails(POSMasterDto input)
        {
            await _detailRepo.DeleteAsync(a => !input.POSDetails.Select(b => b.Id).Contains(a.Id) && a.POSMasterId == input.Id);
        }
        public async override Task<POSMasterDto> GetAsync(EntityDto<long> input)
        {
            var posData = await Repository.GetAll()
                                          .Where(a => a.Id == input.Id)
                                          .Select(a => new POSMasterDto
                                          {
                                              Id = a.Id,
                                              TenantId = a.TenantId,
                                              InvoiceNo = a.InvoiceNo,
                                              OrderNo = a.OrderNo,
                                              InvoiceDate = a.InvoiceDate,
                                              LocationId = a.LocationId,
                                              CustomerId = a.CustomerId,
                                              EmployeeId = a.EmployeeId,
                                              IsInvoiced = a.IsInvoiced,
                                              PrintDate = a.PrintDate,
                                              ServiceTypeId = a.ServiceTypeId,
                                              PaymentMode = a.PaymentMode,
                                              CoverTable = a.CoverTable,
                                              TableId = a.TableId,
                                              DeliveryChargesPer = a.DeliveryChargesPer,
                                              DeliveryCharges = a.DeliveryCharges,
                                              ServiceChargesPer = a.ServiceChargesPer,
                                              ServiceCharges = a.ServiceCharges,
                                              BankChargesPer = a.BankChargesPer,
                                              BankCharges = a.BankCharges,
                                              SalesTaxPer = a.SalesTaxPer,
                                              SalesTaxAmount = a.SalesTaxAmount,
                                              DiscountPer = a.DiscountPer,
                                              DiscountAmount = a.DiscountAmount,
                                              GrossAmount = a.GrossAmount,
                                              NetAmount = a.NetAmount,
                                              PaymentIn = a.PaymentIn,
                                              Balance = a.Balance,
                                              POSDetails = a.POSDetails.Select(x => new POSDetailDto
                                              {
                                                  Id = x.Id,
                                                  POSMasterId = x.POSMasterId,
                                                  ItemId = x.ItemId,
                                                  Qty = x.Qty,
                                                  Price = x.Price,
                                                  Amount = x.Amount,
                                                  Remarks = x.Remarks,
                                                  Cancelled = x.Cancelled,
                                                  CancelledOn = x.CancelledOn
                                              }).ToList()
                                          }).FirstOrDefaultAsync();

            return posData;
        }
        public async Task<List<PendingOrdersDto>> GetPendingOrders(int serviceTypeId, int locationId)
        {
            var dineInOrders = await Repository.GetAll().CountAsync(a => a.IsInvoiced == false && a.ServiceTypeId == 1 && a.LocationId == locationId);
            var takeawayOrders = await Repository.GetAll().CountAsync(a => a.IsInvoiced == false && a.ServiceTypeId == 2 && a.LocationId == locationId);
            var deliveryOrders = await Repository.GetAll().CountAsync(a => a.IsInvoiced == false && a.ServiceTypeId == 3 && a.LocationId == locationId);
            var pendingOrders = await Repository.GetAll()
                                          .Where(a => a.IsInvoiced == false && a.ServiceTypeId == serviceTypeId && a.LocationId == locationId)
                                          .Select(a => new PendingOrdersDto
                                          {
                                              Id = a.Id,
                                              InvoiceNo = a.InvoiceNo,
                                              KOTNo = a.OrderNo,
                                              Table = a.TableId.ToString(),
                                              Customer = a.Customer.Name,
                                              ReservedTime = a.InvoiceDate,
                                              Amount = a.NetAmount,
                                              DineInOrders = dineInOrders,
                                              TakeawayOrders = takeawayOrders,
                                              DeliveryOrders = deliveryOrders
                                          }).ToListAsync();
            return pendingOrders;
        }
    }
}
