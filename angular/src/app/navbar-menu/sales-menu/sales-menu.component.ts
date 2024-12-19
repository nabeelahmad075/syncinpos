import {
  ChangeDetectorRef,
  Component,
  Injector
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/app-component-base";
import { Router } from "@angular/router";

@Component({
  selector: 'app-sales-menu',
  // standalone: true,
  // imports: [],
  templateUrl: './sales-menu.component.html',
  styleUrl: './sales-menu.component.css',
  animations: [appModuleAnimation()]
})
export class SalesMenuComponent extends AppComponentBase {

  constructor(
    injector: Injector,
    private router: Router,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  navigateToPosForm(): void {
    this.router.navigate(["/app/pos"]);
  }

  navigateToDayCloseForm(): void {
    this.router.navigate(["/app/day-close"]);
  }

  navigateToCustomerForm(): void {
    this.router.navigate(["/app/addcustomer"]);
  }
}
