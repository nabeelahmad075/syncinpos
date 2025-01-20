import {
  Component,
  ChangeDetectionStrategy,
  OnInit,
  Injector,
} from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent extends AppComponentBase implements OnInit {
  shownLoginName = "";

  constructor(
    injector: Injector,
  )
  {
    super(injector);
  }

  ngOnInit(): void {
    this.shownLoginName = this.appSession.user.name;
  }
}
