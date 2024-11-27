import { ChangeDetectorRef, Component, Injector, OnInit, Renderer2 } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { SignalRAspNetCoreHelper } from '@shared/helpers/SignalRAspNetCoreHelper';
import { LayoutStoreService } from '@shared/layout/layout-store.service';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  templateUrl: './app.component.html'
})
export class AppComponent extends AppComponentBase implements OnInit {
  sidebarExpanded: boolean;
  isBlankPage = false;

  constructor(
    injector: Injector,
    private renderer: Renderer2,
    private _layoutStore: LayoutStoreService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);

    // Update `isBlankPage` on route change
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.isBlankPage = this.checkIfBlankPage(this.router.url);
        this.cdr.detectChanges();
      }
    });
  }

  ngOnInit(): void {
    this.renderer.addClass(document.body, 'sidebar-mini');

    SignalRAspNetCoreHelper.initSignalR();

    abp.event.on('abp.notifications.received', (userNotification) => {
      abp.notifications.showUiNotifyForUserNotification(userNotification);

      // Desktop notification
      Push.create('AbpZeroTemplate', {
        body: userNotification.notification.data.message,
        icon: abp.appPath + 'assets/app-logo-small.png',
        timeout: 6000,
        onClick: function () {
          window.focus();
          this.close();
        }
      });
    });

    this._layoutStore.sidebarExpanded.subscribe((value) => {
      this.sidebarExpanded = value;
    });
  }

  toggleSidebar(): void {
    this._layoutStore.setSidebarExpanded(!this.sidebarExpanded);
  }

    // Define routes that should not include layouts
    private checkIfBlankPage(url: string): boolean {
      const blankRoutes = ['/app/pos']; // Add routes that should bypass the layout
      console.log('Current URL:', url, 'isBlankPage:', blankRoutes.includes(url));
      return blankRoutes.includes(url); // Use strict matching
    }
    
}
