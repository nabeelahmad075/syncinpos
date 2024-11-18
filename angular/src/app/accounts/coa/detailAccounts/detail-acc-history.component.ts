import {
  ChangeDetectorRef,
  Component,
  Injector,
  ViewChild,
  ViewEncapsulation,
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { AddEditDetailAccComponent} from "./createEditDetailAcc/add-edit-detail-acc.component";
import { extend, sortBy } from "lodash-es";
import { AppComponentBase } from "@shared/app-component-base";
import {
  EmployeeHistoryDto,
  EmployeeServiceProxy,
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
import { AddEditMainAccComponent } from "../mainAccounts/add-edit-main-acc.component";
import { AddEditSubAccComponent } from "../subAccounts/add-edit-sub-acc.component";

@Component({
  selector: 'app-detail-acc-history',
  // standalone: true,
  // imports: [],
  templateUrl: './detail-acc-history.component.html',
  styleUrl: './detail-acc-history.component.css',
  animations: [appModuleAnimation()]
})
export class DetailAccHistoryComponent extends AppComponentBase {

  // employeeHistory: EmployeeHistoryDto[] = [];
  primengTableHelper: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  eventClone: LazyLoadEvent;

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    // private _employeeService: EmployeeServiceProxy,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  getHistory(event?: LazyLoadEvent) {
    // if (this.primengTableHelper.shouldResetPaging(event)) {
    //   this.paginator.changePage(0);
    //   return;
    // }
    // if (this.eventClone && !event.filters)
    //   event.filters = this.eventClone.filters;
    // if (this.eventClone && this.eventClone.sortField && !event.sortField) {
    //   event.sortField = this.eventClone.sortField;
    //   event.sortOrder = this.eventClone.sortOrder;
    // }
    // abp.ui.setBusy();
    // this._employeeService
    //   .getEmployeesHistory(
    //     event && event.filters && event.filters["global"]
    //       ? event.filters["global"].value
    //       : undefined,
    //     "",
    //     this.primengTableHelper.getSkipCount(this.paginator, event),
    //     this.primengTableHelper.getMaxResultCount(this.paginator, event)
    //   )
    //   .subscribe((result) => {
    //     this.primengTableHelper.records = result.items;
    //     this.primengTableHelper.totalRecordsCount = result.totalCount;
    //     this.cd.detectChanges();
    //   })
    //   .add(() => abp.ui.clearBusy());
  }

  showMainAccDialog(): void {
    let createMainAccountDialog: BsModalRef;
    createMainAccountDialog = this._modalService.show(
      AddEditMainAccComponent,
      {
        class: "modal-lg modal-dialog-centered",
        backdrop: "static",
        ignoreBackdropClick: true,
      }
    );
    createMainAccountDialog.content.onSave.subscribe((value) => {
      if (value) {
        this.getHistory({});
      }
    });
  }

  showSubAccDialog(): void {
    let createSubAccountDialog: BsModalRef;
    createSubAccountDialog = this._modalService.show(
      AddEditSubAccComponent,
      {
        class: "modal-lg modal-dialog-centered",
        backdrop: "static",
        ignoreBackdropClick: true,
      }
    );
    createSubAccountDialog.content.onSave.subscribe((value) => {
      if (value) {
        this.getHistory({});
      }
    });
  }

  showCreateOrEditDetailAccDialog(id?: number): void {
    let createOrEditDetailAccDialog: BsModalRef;
    if (!id) {
      createOrEditDetailAccDialog = this._modalService.show(AddEditDetailAccComponent, {
        class: "modal-lg modal-dialog-centered",
        backdrop: "static",
        ignoreBackdropClick: true,
      });
    } else {
      createOrEditDetailAccDialog = this._modalService.show(AddEditDetailAccComponent, {
        class: "modal-lg modal-dialog-centered",
        backdrop: "static",
        ignoreBackdropClick: true,
        initialState: {
          id: id,
        },
      });
    }
    createOrEditDetailAccDialog.content.onSave.subscribe((value) => {
      if (value) {
        this.getHistory({});
      }
    });
  }

  createDetailAcc(): void {
    this.showCreateOrEditDetailAccDialog();
  }

  edit(empHistory: EmployeeHistoryDto): void {
    this.showCreateOrEditDetailAccDialog(empHistory.id);
  }



}
 