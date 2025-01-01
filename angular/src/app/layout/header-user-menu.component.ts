import { Component, ChangeDetectionStrategy } from '@angular/core';
import { ChangePasswordComponent } from '@app/users/change-password/change-password.component';
import { AppAuthService } from '@shared/auth/app-auth.service';

@Component({
  selector: 'header-user-menu',
  templateUrl: './header-user-menu.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HeaderUserMenuComponent {
  constructor(
    private _authService: AppAuthService,
    // private _modalService: BsModalService,
  ) {}

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
