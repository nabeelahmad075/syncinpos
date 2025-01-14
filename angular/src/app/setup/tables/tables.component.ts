import {
  ChangeDetectorRef,
  Component,
  Injector,
  OnInit,
  ViewChild,
  ViewEncapsulation,
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { AddEditTablesComponent } from "./add-edit-tables/add-edit-tables.component";
import { extend, sortBy } from "lodash-es";
import { AppComponentBase } from "@shared/app-component-base";
import {
  TableHistoryDto,
  TableEntityServiceProxy,
} from "@shared/service-proxies/service-proxies";
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from "@shared/paged-listing-component-base";
import { finalize } from "rxjs/operators";
import { TableModule, Table } from "primeng/table";
import { DropdownModule } from "primeng/dropdown";
import { PrimengTableHelper } from "@shared/helpers/primengTableHelper";
import { Paginator, PaginatorModule } from "primeng/paginator";
import { LazyLoadEvent } from "primeng/api";
import { FloorComponent } from "./floor/floor.component";
import { AppConsts } from "@shared/AppConsts";

@Component({
  selector: "app-tables",
  templateUrl: "./tables.component.html",
  styleUrls: ["./tables.component.css"],
  animations: [appModuleAnimation()],
})
export class TablesComponent extends AppComponentBase implements OnInit {
  tableHistory: TableHistoryDto[] = [];
  primengTableHelper: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  eventClone: LazyLoadEvent;

  floorBtnPermission: boolean = false;

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private _tableService: TableEntityServiceProxy,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.floorBtnPermission = abp.auth.isGranted("Pages.Setup.Configuration.Floor.View");
    this.cd.detectChanges();
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
    this._tableService
      .getTableHistory(
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

  showCreateOrEditDialog(id?: number): void {
    let createOrEditDialog: BsModalRef;
    if (!id) {
      createOrEditDialog = this._modalService.show(AddEditTablesComponent, {
        class: "modal-lg modal-dialog-centered",
        backdrop: "static",
        ignoreBackdropClick: true,
      });
    } else {
      createOrEditDialog = this._modalService.show(AddEditTablesComponent, {
        class: "modal-lg modal-dialog-centered",
        backdrop: "static",
        ignoreBackdropClick: true,
        initialState: {
         id : id,
        },
      });
    }
    createOrEditDialog.content.onSave.subscribe((value) => {
      if (value) {
        this.getHistory({});
      }
    });
  }

  create(): void {
    if (!abp.auth.isGranted('Pages.Setup.Configuration.Table.Create')) {
      abp.notify.error(AppConsts.permissionDeniedMessage);
      return;
    }
    this.showCreateOrEditDialog();
  }

  edit(tableHistory: TableHistoryDto): void {
    if (!abp.auth.isGranted('Pages.Setup.Configuration.Table.Update')) {
      abp.notify.error(AppConsts.permissionDeniedMessage);
      return;
    }
    this.showCreateOrEditDialog(tableHistory.id);
  }

  showFloorDialog(): void { 
    if (!abp.auth.isGranted('Pages.Setup.Configuration.Floor.Create')) {
      abp.notify.error(AppConsts.permissionDeniedMessage);
      return;
    }

    let createFloorDialog: BsModalRef;
    createFloorDialog = this._modalService.show(FloorComponent, {
      class: "modal-lg modal-dialog-centered",
      backdrop: "static",
      ignoreBackdropClick: true,
    });
    createFloorDialog.content.onSave.subscribe((value) => {
      if (value) {
        this.showFloorDialog();
        this.getHistory({});
      }
    });
  }
}
