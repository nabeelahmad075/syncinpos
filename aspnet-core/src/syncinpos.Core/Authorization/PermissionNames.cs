using Microsoft.AspNetCore.Components.Rendering;

namespace syncinpos.Authorization
{
    public static class PermissionNames
    {
        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Utility = "Pages.Utility";



        //public const string Pages_Roles = "Pages.Roles";
        //public const string Pages_Users = "Pages.Users";
        //public const string Pages_Users_Create = "Pages.Users.Create";
        //public const string Pages_Users_Edit = "Pages.Users.Edit";
        //public const string Pages_Users_Delete = "Pages.Users.Delete";
        //public const string Pages_Users_Activation = "Pages.Users.Activation";

        //--------------------------------------------------------Setup permissions Start

        //Setup permissions
        public const string Pages_Setup = "Pages.Setup";

        //--------------------------------------------------------Configuration permissions Start

        //Configuration permissions
        public const string Pages_Setup_Configuration = "Pages.Setup.Configuration";

        //Location permissions
        public const string Pages_Setup_Configuration_Location = "Pages.Setup.Configuration.Location";
        public const string Pages_Setup_Configuration_Location_Create = "Pages.Setup.Configuration.Location.Create";
        public const string Pages_Setup_Configuration_Location_Update = "Pages.Setup.Configuration.Location.Update";
        public const string Pages_Setup_Configuration_Location_View = "Pages.Setup.Configuration.Location.View";

        //Table permissions
        public const string Pages_Setup_Configuration_Table = "Pages.Setup.Configuration.Table";
        public const string Pages_Setup_Configuration_Table_Create = "Pages.Setup.Configuration.Table.Create";
        public const string Pages_Setup_Configuration_Table_Update = "Pages.Setup.Configuration.Table.Update";
        public const string Pages_Setup_Configuration_Table_View = "Pages.Setup.Configuration.Table.View";

        //Floor permissions
        public const string Pages_Setup_Configuration_Floor = "Pages.Setup.Configuration.Floor";
        public const string Pages_Setup_Configuration_Floor_Create = "Pages.Setup.Configuration.Floor.Create";
        public const string Pages_Setup_Configuration_Floor_Update = "Pages.Setup.Configuration.Floor.Update";
        public const string Pages_Setup_Configuration_Floor_View = "Pages.Setup.Configuration.Floor.View";

        //--------------------------------------------------------Configuration permissions End

        //--------------------------------------------------------HR permissions Start
        //HR Management permissions
        public const string Pages_Setup_HR_Management = "Pages.Setup.HR_Management";

        //Employee permissions
        public const string Pages_Setup_HR_Management_Employee = "Pages.Setup.Configuration.Employee";
        public const string Pages_Setup_HR_Management_Employee_Create = "Pages.Setup.Configuration.Employee.Create";
        public const string Pages_Setup_HR_Management_Employee_Update = "Pages.Setup.Configuration.Employee.Update";
        public const string Pages_Setup_HR_Management_Employee_View = "Pages.Setup.Configuration.Employee.View";
        public const string Pages_Setup_HR_Management_Employee_User_Control = "Pages.Setup.Configuration.Employee.User_Control";

        //Designation permissions
        public const string Pages_Setup_HR_Management_Designation = "Pages.Setup.Configuration.Designation";
        public const string Pages_Setup_HR_Management_Designation_Create = "Pages.Setup.Configuration.Designation.Create";
        public const string Pages_Setup_HR_Management_Designation_Update = "Pages.Setup.Configuration.Designation.Update";
        public const string Pages_Setup_HR_Management_Designation_View = "Pages.Setup.Configuration.Designation.View";

        //Department permissions
        public const string Pages_Setup_HR_Management_Department = "Pages.Setup.Configuration.Department";
        public const string Pages_Setup_HR_Management_Department_Create = "Pages.Setup.Configuration.Department.Create";
        public const string Pages_Setup_HR_Management_Department_Update = "Pages.Setup.Configuration.Department.Update";
        public const string Pages_Setup_HR_Management_Department_View = "Pages.Setup.Configuration.FloDepartmentor.View";

        //Users permissions
        public const string Pages_Setup_HR_Management_Users = "Pages.Setup.Configuration.Users";
        public const string Pages_Setup_HR_Management_Users_Update = "Pages.Setup.Configuration.Users.Update";
        public const string Pages_Setup_HR_Management_Users_View = "Pages.Setup.Configuration.Users.View";
        public const string Pages_Setup_HR_Management_Users_Activation = "Pages.Setup.Configuration.Users.Activation";

        //Roles permissions
        public const string Pages_Setup_HR_Management_Roles = "Pages.Setup.Configuration.Roles";
        public const string Pages_Setup_HR_Management_Roles_Create = "Pages.Setup.Configuration.Roles.Create";
        public const string Pages_Setup_HR_Management_Roles_Update = "Pages.Setup.Configuration.Roles.Update";
        public const string Pages_Setup_HR_Management_Roles_View = "Pages.Setup.Configuration.Roles.View";

        //--------------------------------------------------------HR permissions End

        //--------------------------------------------------------Setup permissions End
    }
}
