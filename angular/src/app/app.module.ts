import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxPaginationModule } from 'ngx-pagination';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from '@shared/shared.module';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { DropdownModule} from 'primeng/dropdown';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { CalendarModule } from 'primeng/calendar';
import { MultiSelectModule } from 'primeng/multiselect';
// layout
import { HeaderComponent } from './layout/header.component';
import { HeaderLeftNavbarComponent } from './layout/header-left-navbar.component';
import { HeaderLanguageMenuComponent } from './layout/header-language-menu.component';
import { HeaderUserMenuComponent } from './layout/header-user-menu.component';
import { FooterComponent } from './layout/footer.component';
import { SidebarComponent } from './layout/sidebar.component';
import { SidebarLogoComponent } from './layout/sidebar-logo.component';
import { SidebarUserPanelComponent } from './layout/sidebar-user-panel.component';
import { SidebarMenuComponent } from './layout/sidebar-menu.component';
import { LocationHistoryComponent } from './setup/locations/location-history.component';  
import { AddEditLocComponent } from './setup/locations/create-edit-location/add-edit-loc.component';
import { EmployeeHistoryComponent } from './hr/employees/employee-history.component';
import { AddEditEmpComponent } from './hr/employees/create-edit-employee/add-edit-emp.component';
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
import { PriceHistoryComponent} from './menu-operations/item-price/price-history.component';
import { AddEditPriceComponent} from './menu-operations/item-price/create-edit-price/add-edit-price.component';
import { PriceReplicaComponent } from './menu-operations/item-price/price-replica/price-replica.component';
import { VoucherHistoryComponent } from './accounts/voucher/voucher-history.component';
import { AddEditVoucherComponent} from './accounts/voucher/create-edit-voucher/add-edit-voucher.component';
import { SetupMenuComponent } from './navbar-menu/setup-menu/setup-menu.component';
import { SalesMenuComponent} from './navbar-menu/sales-menu/sales-menu.component';
import { AccountsMenuComponent } from './navbar-menu/accounts-menu/accounts-menu.component';
import { HeaderDateComponent } from './layout/header-date.component';
import { DayCloseHistoryComponent } from './sales/day-close/day-close-history.component';
@NgModule({
    declarations: [
        AppComponent,
        // layout
        HeaderComponent,
        HeaderLeftNavbarComponent,
        HeaderLanguageMenuComponent,
        HeaderUserMenuComponent,
        FooterComponent,
        SidebarComponent,
        SidebarLogoComponent,
        SidebarUserPanelComponent,
        SidebarMenuComponent,
        LocationHistoryComponent,
        AddEditLocComponent,
        EmployeeHistoryComponent,
        AddEditEmpComponent,
        CreateDesignationDepartmentComponent,
        SectionHistoryComponent,
        AddEditSectionComponent,
        ItemCategoryHistoryComponent,
        AddEditItemCategoryComponent,
        ItemDefinitionHistoryComponent,
        AddEditItemDefinitionComponent,
        TablesComponent,
        AddEditTablesComponent,
        FloorComponent,
        AddEditMainAccComponent,
        AddEditSubAccComponent,
        DetailAccHistoryComponent,
        AddEditDetailAccComponent,
        CustomerHistoryComponent,
        AddEditCustomerComponent,
        MainPosComponent,
        PriceHistoryComponent,
        AddEditPriceComponent,
        PriceReplicaComponent,
        VoucherHistoryComponent,
        AddEditVoucherComponent,
        SetupMenuComponent,
        SalesMenuComponent,
        AccountsMenuComponent,
        HeaderDateComponent,
        DayCloseHistoryComponent
    ],
    imports: [
        AppRoutingModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        HttpClientJsonpModule,
        ModalModule.forChild(),
        BsDropdownModule,
        CollapseModule,
        TabsModule,
        ServiceProxyModule,
        NgxPaginationModule,
        SharedModule,
        DropdownModule,
        TableModule,
        PaginatorModule,
        CalendarModule,
        MultiSelectModule
    ],
    providers: []
})
export class AppModule {}
