import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { AppComponent } from './app.component';
import { LocationHistoryComponent } from "./setup/locations/location-history.component";
import { AddEditLocComponent } from "./setup/locations/create-edit-location/add-edit-loc.component";
import { EmployeeHistoryComponent } from "./hr/employees/employee-history.component";
import { AddEditEmpComponent } from "./hr/employees/create-edit-employee/add-edit-emp.component";
import { CreateDesignationDepartmentComponent } from './hr/designation-department/create-designation-department.component';
import { SectionHistoryComponent} from './menu-operations/sections/section-history.component';
import { AddEditSectionComponent } from './menu-operations/sections/create-edit-sections/add-edit-section.component';
import { ItemCategoryHistoryComponent} from './menu-operations/item-category/item-category-history.component';
import { AddEditItemCategoryComponent} from './menu-operations/item-category/create-edit-item-category/add-edit-item-category.component';
import { ItemDefinitionHistoryComponent } from './menu-operations/item/item-definition/item-definition-history.component';
import { AddEditItemDefinitionComponent } from './menu-operations/item/item-definition/create-edit-item-definition/add-edit-item-definition.component';
import { TablesComponent} from './setup/tables/tables.component';
import { AddEditTablesComponent } from './setup/tables/add-edit-tables/add-edit-tables.component';
import { FloorComponent } from './setup/tables/floor/floor.component';
import { AddEditMainAccComponent} from './accounts/coa/mainAccounts/add-edit-main-acc.component';
import { AddEditSubAccComponent} from './accounts/coa/subAccounts/add-edit-sub-acc.component';
import { DetailAccHistoryComponent } from './accounts/coa/detailAccounts/detail-acc-history.component';
import { AddEditDetailAccComponent } from './accounts/coa/detailAccounts/createEditDetailAcc/add-edit-detail-acc.component';
import { CustomerHistoryComponent } from './sales/customer/customer-history.component';
import { AddEditCustomerComponent } from './sales/customer/create-edit-customer/add-edit-customer.component';
import { MainPosComponent } from './sales/pos/mainpos/main-pos.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { PriceHistoryComponent } from './menu-operations/item-price/price-history.component';
import { AddEditPriceComponent } from './menu-operations/item-price/create-edit-price/add-edit-price.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    {
                        path: 'home',
                        loadChildren: () => import('./home/home.module').then((m) => m.HomeModule),
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "locationhistory",
                        component: LocationHistoryComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "newlocation",
                        component: AddEditLocComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "floor",
                        component: FloorComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "tables",
                        component: TablesComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "edit-tables",
                        component: AddEditTablesComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "employeehistory",
                        component: EmployeeHistoryComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "createemp",
                        component: AddEditEmpComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "createdesignationdepartment",
                        component: CreateDesignationDepartmentComponent ,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "section-history",
                        component: SectionHistoryComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "newsection",
                        component: AddEditSectionComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "categoryhistory",
                        component: ItemCategoryHistoryComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "newcategory",
                        component: AddEditItemCategoryComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "price-list",
                        component: PriceHistoryComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "itemdefinitionhistory",
                        component: ItemDefinitionHistoryComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "pos",
                        component: MainPosComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "addcustomer",
                        component: CustomerHistoryComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "customer-history",
                        component: AddEditCustomerComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "coa-detail",
                        component: DetailAccHistoryComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "coa-add-detail",
                        component: AddEditDetailAccComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "coa-add-sub",
                        component: AddEditSubAccComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "coa-add-main",
                        component: AddEditMainAccComponent,
                        canActivate: [AppRouteGuard]
                    },
                    // {
                    //     path: 'about',
                    //     loadChildren: () => import('./about/about.module').then((m) => m.AboutModule),
                    //     canActivate: [AppRouteGuard]
                    // },
                    {
                        path: 'users',
                        loadChildren: () => import('./users/users.module').then((m) => m.UsersModule),
                        data: { permission: 'Pages.Users' },
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'roles',
                        loadChildren: () => import('./roles/roles.module').then((m) => m.RolesModule),
                        data: { permission: 'Pages.Roles' },
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'tenants',
                        loadChildren: () => import('./tenants/tenants.module').then((m) => m.TenantsModule),
                        data: { permission: 'Pages.Tenants' },
                        canActivate: [AppRouteGuard]
                    },
                    // {
                    //     path: 'update-password',
                    //     loadChildren: () => import('./users/users.module').then((m) => m.UsersModule),
                    //     canActivate: [AppRouteGuard]
                    // },
                    {
                        path: "update-password",
                        component: ChangePasswordComponent,
                        canActivate: [AppRouteGuard]
                    },
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
