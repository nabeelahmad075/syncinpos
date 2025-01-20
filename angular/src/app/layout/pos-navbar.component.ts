import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '../../shared/app-component-base';
import { AppAuthService } from '../../shared/auth/app-auth.service';

@Component({
  selector: 'pos-navbar',
  // standalone: true,
  // imports: [],
  templateUrl: './pos-navbar.component.html'
})
export class PosNavbarComponent extends AppComponentBase implements OnInit {


  shownLoginName = '';

  constructor(
    injector: Injector,
    private _authService: AppAuthService,
  ) {super(injector);}

  ngOnInit(): void {
    this.shownLoginName = this.appSession.user.userName;
  }

  logout(): void {
    this._authService.logout();
  }
}
