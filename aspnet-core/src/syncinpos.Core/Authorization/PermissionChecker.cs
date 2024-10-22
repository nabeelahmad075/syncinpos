using Abp.Authorization;
using syncinpos.Authorization.Roles;
using syncinpos.Authorization.Users;

namespace syncinpos.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
