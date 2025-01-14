import { ChangeDetectorRef, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/app-component-base";
import { Router } from "@angular/router";
import { AppConsts } from "@shared/AppConsts";

@Component({
  selector: "app-setup-menu",
  // standalone: true,
  // imports: [],
  templateUrl: "./setup-menu.component.html",
  styleUrl: "./setup-menu.component.css",
  animations: [appModuleAnimation()],
})
export class SetupMenuComponent extends AppComponentBase implements OnInit {

  locationPermission: boolean = false;
  tablePermission: boolean = false;
  employeePermission: boolean = false;
  usersPermission: boolean = false;
  rolesPermission: boolean = false;
  sectionPermission: boolean = false;
  categoryPermission: boolean = false;
  itemPermission: boolean = false;
  itemPricePermission: boolean = false;

  constructor(
    injector: Injector,
    private router: Router,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.locationPermission = abp.auth.isGranted("Pages.Setup.Configuration.Location.View");
    this.tablePermission = abp.auth.isGranted("Pages.Setup.Configuration.Table.View");
    this.employeePermission = abp.auth.isGranted("Pages.Setup.HR_Management.Employee.View");
    this.usersPermission = abp.auth.isGranted("Pages.Setup.HR_Management.Users.View");
    this.rolesPermission = abp.auth.isGranted("Pages.Setup.HR_Management.Roles.View");
    this.sectionPermission = abp.auth.isGranted("Pages.Setup.Menu_Operations.Sections.View");
    this.categoryPermission = abp.auth.isGranted("Pages.Setup.Menu_Operations.Category.View");
    this.itemPermission = abp.auth.isGranted("Pages.Setup.Menu_Operations.ItemInformation.View");
    this.itemPricePermission = abp.auth.isGranted("Pages.Setup.Menu_Operations.PriceList.View");
    this.cd.detectChanges();
  }

  navigateToLocationForm(): void {
    this.router.navigate(["/app/locationhistory"]);
  }

  navigateToTableForm(): void {
    this.router.navigate(["/app/tables"]);
  }

  navigateToEmployeeForm(): void {
    this.router.navigate(["/app/employeehistory"]);
  }

  navigateToUsersForm(): void {
    this.router.navigate(["/app/users"]);
  }

  navigateToRolesForm(): void {
    this.router.navigate(["/app/roles"]);
  }

  navigateToSectionForm(): void {
    this.router.navigate(["/app/section-history"]);
  }

  navigateToCategoryForm(): void {
    this.router.navigate(["/app/categoryhistory"]);
  }

  navigateToItemForm(): void {
    this.router.navigate(["/app/itemdefinitionhistory"]);
  }

  navigateToItemPriceForm(): void {
    this.router.navigate(["/app/price-list"]);
  }
}
