using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace syncinpos.Authorization
{
    public class syncinposAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Utility, L("Utility"), multiTenancySides: MultiTenancySides.Host);

            //--------------------------------------------------------Setup permissions Start

            var setup = context.CreatePermission(PermissionNames.Pages_Setup, L("Setup"));

            //--------------------------------------------------------Configuration permissions Start

            var configuration = setup.CreateChildPermission(PermissionNames.Pages_Setup_Configuration, L("Configuration"));

            //Location permissions
            var location = configuration.CreateChildPermission(PermissionNames.Pages_Setup_Configuration_Location, L("Location"));
            location.CreateChildPermission(PermissionNames.Pages_Setup_Configuration_Location_Create, L("Create"));
            location.CreateChildPermission(PermissionNames.Pages_Setup_Configuration_Location_Update, L("Update"));
            location.CreateChildPermission(PermissionNames.Pages_Setup_Configuration_Location_View, L("View"));

            //Table permissions
            var table = configuration.CreateChildPermission(PermissionNames.Pages_Setup_Configuration_Table, L("Table"));
            table.CreateChildPermission(PermissionNames.Pages_Setup_Configuration_Table_Create, L("Create"));
            table.CreateChildPermission(PermissionNames.Pages_Setup_Configuration_Table_Update, L("Update"));
            table.CreateChildPermission(PermissionNames.Pages_Setup_Configuration_Table_View, L("View"));

            //Floor permissions
            var floor = configuration.CreateChildPermission(PermissionNames.Pages_Setup_Configuration_Floor, L("Floor"));
            floor.CreateChildPermission(PermissionNames.Pages_Setup_Configuration_Floor_Create, L("Create"));
            floor.CreateChildPermission(PermissionNames.Pages_Setup_Configuration_Floor_Update, L("Update"));
            floor.CreateChildPermission(PermissionNames.Pages_Setup_Configuration_Floor_View, L("View"));

            //--------------------------------------------------------Configuration permissions End
            //--------------------------------------------------------HR Management permissions Start

            var hrManagement = setup.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management, L("HR_Management"));

            //Employee permissions
            var employee = hrManagement.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Employee, L("Employee"));
            employee.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Employee_Create, L("Create"));
            employee.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Employee_Update, L("Update"));
            employee.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Employee_View, L("View"));
            employee.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Employee_User_Control, L("User_Control"));

            //Designation permissions
            var designation = hrManagement.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Designation, L("Designation"));
            designation.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Designation_Create, L("Create"));
            designation.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Designation_Update, L("Update"));
            designation.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Designation_View, L("View"));

            //Department permissions
            var department = hrManagement.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Department, L("Department"));
            department.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Department_Create, L("Create"));
            department.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Department_Update, L("Update"));
            department.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Department_View, L("View"));

            //Users permissions
            var users = hrManagement.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Users, L("Department"));
            users.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Users_Update, L("Update"));
            users.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Users_View, L("View"));
            users.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Users_Activation, L("Activation"));

            //Roles permissions
            var roles = hrManagement.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Roles, L("Roles"));
            roles.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Roles_Create, L("Create"));
            roles.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Roles_Update, L("Update"));
            roles.CreateChildPermission(PermissionNames.Pages_Setup_HR_Management_Roles_View, L("View"));

            //--------------------------------------------------------HR Management permissions End


            //--------------------------------------------------------Setup permissions End

            //var users = context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            //users.CreateChildPermission(PermissionNames.Pages_Users_Create, L("Create"));

            ////context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            //context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));





        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, syncinposConsts.LocalizationSourceName);
        }
    }
}
