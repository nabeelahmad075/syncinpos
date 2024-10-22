using Abp.Application.Services;
using syncinpos.MultiTenancy.Dto;

namespace syncinpos.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

