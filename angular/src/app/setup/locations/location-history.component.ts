import {
  ChangeDetectorRef,
  Component,
  Injector,
  OnInit,
  ViewChild,
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { AddEditLocComponent } from "./create-edit-location/add-edit-loc.component";
import { AppComponentBase } from "@shared/app-component-base";
import {
  LocationServiceProxy,
  LocationHistoryDto,
} from "@shared/service-proxies/service-proxies";
import { Table } from "primeng/table";
import { PrimengTableHelper } from "@shared/helpers/primengTableHelper";
import { Paginator } from "primeng/paginator";
import { LazyLoadEvent } from "primeng/api";
import { AppConsts } from "@shared/AppConsts";

@Component({
  selector: "app-location-history",
  // standalone: true,
  // imports: [TableModule],
  templateUrl: "./location-history.component.html",
  styleUrl: "./location-history.component.css",
  animations: [appModuleAnimation()],
})
export class LocationHistoryComponent extends AppComponentBase {
  locationHistory: LocationHistoryDto[] = [];
  keyword = "";
  maxResultCount: number = 5;
  primengTableHelper: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  eventClone: LazyLoadEvent;

  createBtnPermission: boolean = false;
  isLoading: boolean = true;

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private _locationService: LocationServiceProxy,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
    this.skeletonLoading();
  }

  skeletonLoading() {
    this.isLoading = true;
    setTimeout(() => {
      this.isLoading = false;
      this.cd.detectChanges();
    }, 5000); // Simulate a 5-second loading period
  }

  getHistory(event?: LazyLoadEvent) {
    if (this.primengTableHelper.shouldResetPaging(event)) {
      this.paginator.changePage(0);
      return;
    }
    if (this.eventClone && !event.filters)
      event.filters = this.eventClone.filters;
    if (this.eventClone && this.eventClone.sortField && !event.sortField) {
      event.sortField = this.eventClone.sortField;
      event.sortOrder = this.eventClone.sortOrder;
    }
    abp.ui.setBusy();
    this._locationService
      .getHistory(
        event && event.filters && event.filters["global"]
          ? event.filters["global"].value
          : undefined,
        "",
        this.primengTableHelper.getSkipCount(this.paginator, event),
        this.primengTableHelper.getMaxResultCount(this.paginator, event)
      )
      .subscribe((result) => {
        this.primengTableHelper.records = result.items;
        this.primengTableHelper.totalRecordsCount = result.totalCount;
        this.cd.detectChanges();
      })
      .add(() => abp.ui.clearBusy());
  }

  createLocation(): void {
    if (!abp.auth.isGranted("Pages.Setup.Configuration.Location.Create")) {
      abp.notify.error(AppConsts.permissionDeniedMessage);
      return;
    }
    this.showCreateOrEditLocDialog();
  }

  editLocation(locationHistory: LocationHistoryDto): void {
    if (!abp.auth.isGranted("Pages.Setup.Configuration.Location.Update")) {
      abp.notify.error(AppConsts.permissionDeniedMessage);
      return;
    }
    this.showCreateOrEditLocDialog(locationHistory.id);
  }

  showCreateOrEditLocDialog(id?: number): void {
    let createOrEditLocDialog: BsModalRef;
    if (!id) {
      createOrEditLocDialog = this._modalService.show(AddEditLocComponent, {
        class: "modal-lg modal-dialog-centered",
        backdrop: "static",
        ignoreBackdropClick: true,
      });
    } else {
      createOrEditLocDialog = this._modalService.show(AddEditLocComponent, {
        class: "modal-lg modal-dialog-centered",
        backdrop: "static",
        ignoreBackdropClick: true,
        initialState: {
          id: id,
        },
      });
    }

    createOrEditLocDialog.content.onSave.subscribe((value) => {
      if (value) {
        this.getHistory({});
      }
    });
  }
}
