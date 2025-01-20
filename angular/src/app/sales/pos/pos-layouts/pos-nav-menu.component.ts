import { Component, Injector } from '@angular/core';
import { Router } from '@node_modules/@angular/router';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'pos-nav-menu',
  // standalone: true,
  // imports: [],
  templateUrl: './pos-nav-menu.component.html'
})
export class PosNavMenuComponent extends AppComponentBase {

    constructor(
      injector: Injector,
      private router: Router,
    ) {
      super(injector);
    }
  
  navigateToSalesMenu(): void {
    this.router.navigate(["app/sales-menu"]);
  }

}
