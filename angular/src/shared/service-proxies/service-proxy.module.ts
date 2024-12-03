import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AbpHttpInterceptor } from 'abp-ng2-module';

import * as ApiServiceProxies from './service-proxies';

@NgModule({
    providers: [
        ApiServiceProxies.RoleServiceProxy,
        ApiServiceProxies.SessionServiceProxy,
        ApiServiceProxies.TenantServiceProxy,
        ApiServiceProxies.UserServiceProxy,
        ApiServiceProxies.TokenAuthServiceProxy,
        ApiServiceProxies.AccountServiceProxy,
        ApiServiceProxies.ConfigurationServiceProxy,
        ApiServiceProxies.LocationServiceProxy,
        ApiServiceProxies.LocationTypeServiceProxy,
        ApiServiceProxies.RegionServiceProxy,
        ApiServiceProxies.EmployeeServiceProxy,
        ApiServiceProxies.DepartmentServiceProxy,
        ApiServiceProxies.DesignationServiceProxy,
        ApiServiceProxies.SectionsServiceProxy,
        ApiServiceProxies.ItemCategoryServiceProxy,
        ApiServiceProxies.ItemTypeServiceProxy,
        ApiServiceProxies.ItemServiceProxy,
        ApiServiceProxies.UnitOfMeasurementServiceProxy,
        ApiServiceProxies.TableEntityServiceProxy,
        ApiServiceProxies.FloorEntityServiceProxy,
        ApiServiceProxies.MainAccountServiceProxy,
        ApiServiceProxies.SubAccountServiceProxy,
        ApiServiceProxies.DetailAccountServiceProxy,
        ApiServiceProxies.CustomerServiceProxy,
        ApiServiceProxies.ItemPriceServiceProxy,
        { provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true }
    ]
})
export class ServiceProxyModule { }
