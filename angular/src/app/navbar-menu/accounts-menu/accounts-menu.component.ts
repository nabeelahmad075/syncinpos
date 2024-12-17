import {
  ChangeDetectorRef,
  Component,
  Injector
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/app-component-base";
import { Router } from "@angular/router";

@Component({
  selector: 'app-accounts-menu',
  // standalone: true,
  // imports: [],
  templateUrl: './accounts-menu.component.html',
  styleUrl: './accounts-menu.component.css',
  animations: [appModuleAnimation()]
})
export class AccountsMenuComponent extends AppComponentBase {

  constructor(
    injector: Injector,
    private router: Router,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  navigateToChartOfAccountsForm(): void {
    this.router.navigate(["/app/coa-detail"]);
  }

  navigateToVoucherEntryForm(): void {
    this.router.navigate(["/app/voucher-history"]);
  }

}
