import { Component, ChangeDetectionStrategy, OnInit, Injector } from '@angular/core';
import { ChangePasswordComponent } from '@app/users/change-password/change-password.component';
import { AppComponentBase } from '@shared/app-component-base';
import { AppAuthService } from '@shared/auth/app-auth.service';

@Component({
  selector: 'header-user-menu',
  templateUrl: './header-user-menu.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HeaderUserMenuComponent extends AppComponentBase implements OnInit {

  shownLoginName = '';

  constructor(
    injector: Injector,
    private _authService: AppAuthService,
    // private _modalService: BsModalService,
  ) {super(injector);}

  ngOnInit(): void {
    this.shownLoginName = this.appSession.getShownLoginName();
  }

  logout(): void {
    this._authService.logout();
  }

  // showUpdatePasswordDialog(): void {
  //   let createDesignationDepartmentDialog: BsModalRef;
  //   createDesignationDepartmentDialog = this._modalService.show(
  //     ChangePasswordComponent,
  //     {
  //       class: "modal-lg modal-dialog-centered",
  //       backdrop: "static",
  //       ignoreBackdropClick: true,
  //     }
  //   );
  // }
}
