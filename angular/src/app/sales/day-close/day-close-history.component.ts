import {
  ChangeDetectorRef,
  Component,
  Injector,
  OnInit,
  ViewChild,
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/app-component-base";
import {
  DayCloseDto,
  DayCloseServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { TableModule, Table } from "primeng/table";
import { PrimengTableHelper } from "@shared/helpers/primengTableHelper";
import { LazyLoadEvent } from "primeng/api";
import { Router } from "@node_modules/@angular/router";

@Component({
  selector: "app-day-close-history",
  // standalone: true,
  // imports: [],
  templateUrl: "./day-close-history.component.html",
  styleUrl: "./day-close-history.component.css",
  animations: [appModuleAnimation()],
})
export class DayCloseHistoryComponent
  extends AppComponentBase
  implements OnInit
{
  dayCloseHistory: DayCloseDto[] = [];
  allSelected: boolean = false;
  eventClone: LazyLoadEvent;

  constructor(
    injector: Injector,
    private _dayCloseService: DayCloseServiceProxy,
    private router: Router,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.getHistory();
  }

  getHistory() {
    abp.ui.setBusy();
    this._dayCloseService
      .getOpenDaysForLocations()
      .subscribe((result) => {
        this.dayCloseHistory = result;
        this.cd.detectChanges();
      })
      .add(() => abp.ui.clearBusy());
  }

  dayCloseBtn() {
    let isAnyRowSelected = this.dayCloseHistory.some((item) => item.isMarked);
    if (!isAnyRowSelected) {
      abp.notify.error("Please select at least one row to close the day.");
      return;
    }

    this._dayCloseService.bulkCreate(this.dayCloseHistory).subscribe(() => {
      abp.notify.success("Day Closed on selected locations");
      this.getHistory();
      this.allSelected = false;
    });
  }

  selectAll($event: any) {
    this.allSelected = $event.target.checked;
    debugger;
    if (this.allSelected) {
      this.dayCloseHistory.forEach((item) => (item.isMarked = true));
    } else {
      this.dayCloseHistory.forEach((item) => (item.isMarked = false));
    }
  }

  selectRow(item: any, $event: any) {
    item.selected = $event.target.checked;
    this.allSelected = this.dayCloseHistory.every((item) => item.isMarked);
  }
}
