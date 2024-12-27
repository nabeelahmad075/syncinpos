import {
  ChangeDetectorRef,
  Component,
  Injector
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/app-component-base";
import { Router } from "@angular/router";

@Component({
  selector: "app-setup-menu",
  // standalone: true,
  // imports: [],
  templateUrl: "./setup-menu.component.html",
  styleUrl: "./setup-menu.component.css",
  animations: [appModuleAnimation()],
})
export class SetupMenuComponent extends AppComponentBase {
  
  constructor(
    injector: Injector,
    private router: Router,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
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
