import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { AppComponent } from './app.component';
import { LocationHistoryComponent } from "./locations/location-history.component";
import { AddEditLocComponent } from "./locations/create-edit-location/add-edit-loc.component";
import { EmployeeHistoryComponent } from "./employees/employee-history.component";
import { AddEditEmpComponent } from "./employees/create-edit-employee/add-edit-emp.component";

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
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
                        path: 'home',
                        loadChildren: () => import('./home/home.module').then((m) => m.HomeModule),
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'about',
                        loadChildren: () => import('./about/about.module').then((m) => m.AboutModule),
                        canActivate: [AppRouteGuard]
                    },
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
                    {
                        path: 'update-password',
                        loadChildren: () => import('./users/users.module').then((m) => m.UsersModule),
                        canActivate: [AppRouteGuard]
                    },
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
