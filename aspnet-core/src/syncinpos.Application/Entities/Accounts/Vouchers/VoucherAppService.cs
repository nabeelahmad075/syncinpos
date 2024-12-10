using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using syncinpos.Authorization;
using syncinpos.Entities.Accounts.Types;
using syncinpos.Entities.Accounts.Vouchers.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.Vouchers
{
    public class VoucherAppService : AsyncCrudAppService<VoucherMaster, VoucherMasterDto, long>
    {
        private IRepository<VoucherDetail, long> _detailRepository;
        private IRepository<VoucherType, int> _voucherTypeRepository;
        public VoucherAppService(
            IRepository<VoucherMaster, long> repository,
            IRepository<VoucherDetail, long> detailRepository,
            IRepository<VoucherType, int> voucherTypeRepository
            ) : base(repository) 
        {
            _detailRepository = detailRepository;
            _voucherTypeRepository = voucherTypeRepository;
        }
        public async Task<List<SelectItemDto>> GetVoucherTypeDropdownAsync()
        {
            var voucherTypes = await _voucherTypeRepository.GetAll()
                                                           .Select(a => new SelectItemDto
                                                           {
                                                               Label = a.Alias,
                                                               Value = a.Id
                                                           }).ToListAsync();
            return voucherTypes;
        }
        public async Task<string> GetNewDocNo(int voucherTypeId, string voucherMonth, int locationId)
        {
            string strDocNo = await Repository.GetAll()
                .Where(a => a.VoucherTypeId == voucherTypeId && a.LocationId == locationId &&
                a.VoucherDate.Year % 100 == int.Parse(voucherMonth.Substring(0, 2)) && 
                a.VoucherDate.Month == int.Parse(voucherMonth.Substring(2, 2)))
                .OrderByDescending(b => b.VoucherNo.Substring(5))
                .Select(a => a.VoucherNo.Substring(5))
                .FirstOrDefaultAsync();

            int newDocNumber = 1;

            if (!string.IsNullOrEmpty(strDocNo))
            {
                int maxNumber = Convert.ToInt32(strDocNo);
                newDocNumber = maxNumber + 1;
            }

            string newMainCode = $"{voucherMonth}-{newDocNumber:D4}";

            return newMainCode;
        }

        //[AbpAuthorize(PermissionNames.Pages_ProjectManagement_Transaction_BOQ_Create)]
        public async override Task<VoucherMasterDto> CreateAsync(VoucherMasterDto input)
        {
            await DeleteRemovedDetails(input);
            await GetNewCodeIfCodeAlreadyTaken(input);
            return await base.CreateAsync(input);
        }

        //[AbpAuthorize(PermissionNames.Pages_ProjectManagement_Transaction_BOQ_Update)]
        public override async Task<VoucherMasterDto> UpdateAsync(VoucherMasterDto input)
        {

            await DeleteRemovedDetails(input);
            return await base.UpdateAsync(input);
        }
        public override Task<VoucherMasterDto> GetAsync(EntityDto<long> input)
        {
            var voucherEntry =  Repository.GetAll().Where(a => a.Id == input.Id)
                .Select(a => new VoucherMasterDto
                {
                    Id = a.Id,
                    LocationId = a.LocationId,
                    VoucherDate = a.VoucherDate,
                    VoucherNo = a.VoucherNo,
                    Remarks = a.Remarks,
                    VoucherTypeId = a.VoucherTypeId,
                    VoucherDetails = a.VoucherDetails.Select(b => new VoucherDetailDto
                    {
                        Id = b.Id,
                        VoucherMasterId = b.VoucherMasterId,
                        DetailAccountId = b.DetailAccountId,
                        DebitAmount = b.DebitAmount,
                        CreditAmount = b.CreditAmount
                    }).ToList()
                }).FirstOrDefaultAsync();

            return voucherEntry;
        }
        private async Task DeleteRemovedDetails(VoucherMasterDto input)
        {
            await _detailRepository.DeleteAsync(a => !input.VoucherDetails.Select(b => b.Id).Contains(a.Id) && a.VoucherMasterId == input.Id);
        }
        private async Task GetNewCodeIfCodeAlreadyTaken(VoucherMasterDto input)
        {
            if (!IsAlreadyCreated(input.VoucherNo, input.Id))
            {
                input.VoucherNo = await GetNewDocNo(input.VoucherTypeId, input.VoucherDate.ToString("yyMM"), input.LocationId);
            }
        }
        public bool IsAlreadyCreated(string code, long? id = null)
        {
            var IsAlreadyCreated = Repository.GetAll()
                                    .WhereIf(id != null && id > 0, a => a.Id != id)
                                    .Where(a => a.VoucherNo == code).Any();
            return IsAlreadyCreated;
        }
        public async Task<PagedResultDto<VoucherHistoryDto>> GetVoucherHistoryAsync(VoucherHistoryPagedAndSortedResultRequestDto input)
        {
            var sqlQuery = CreateFilteredQuery(input)
                            .WhereIf(!string.IsNullOrEmpty(input.Keyword),
                            a => a.Location.LocationName.Contains(input.Keyword.ToString()) ||
                            a.VoucherType.Alias.Contains(input.Keyword.ToString()) ||
                            a.VoucherNo.Contains(input.Keyword.ToString()) ||
                            a.Remarks.Contains(input.Keyword.ToString()) ||
                            a.VoucherDetails.Sum(x => x.DebitAmount).ToString().Contains(input.Keyword.ToString()))
                            .Select(d => new VoucherHistoryDto
                            { 
                              Id = d.Id,
                              Location = d.Location.LocationName,
                              VoucherType = d.VoucherType.Alias,
                              VoucherNo = d.VoucherNo,
                              VoucherDate = d.VoucherDate,
                              VoucherAmount = d.VoucherDetails.Sum(x => x.DebitAmount),
                              Remarks = d.Remarks
                            });

            var sortedQuery = sqlQuery.OrderBy(x => input.Sorting);
            var pagedQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);

            return new PagedResultDto<VoucherHistoryDto>
            {
                Items = await pagedQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
    }
}
