using Abp.MultiTenancy;
using syncinpos.Utility;
using System;


namespace syncinpos.Sessions
{
    public class NullSyncInPosSession : ISyncInPosSession
    {
        public static NullSyncInPosSession Instance { get; } = new NullSyncInPosSession();

        public DateTime? OpenedDay => null; // Default value for CompanyId
        public long? UserId => null; // Default value for UserId
        public int? TenantId => null; // Default value for TenantId
        public MultiTenancySides MultiTenancySide => MultiTenancySides.Host; // Default value for multi-tenancy
        public long? ImpersonatorUserId => null;
        public int? ImpersonatorTenantId => null;
        private NullSyncInPosSession() { } // Private constructor to enforce singleton pattern

        public IDisposable Use(int? tenantId, long? userId)
        {
            return NullDisposable.Instance; // Return a disposable that does nothing
        }
    }
}
