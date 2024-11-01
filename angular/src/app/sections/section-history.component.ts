import {
  ChangeDetectorRef,
  Component,
  Injector,
  ViewChild
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { AddEditSectionComponent } from "./create-edit-sections/add-edit-section.component";
import { extend, sortBy } from "lodash-es";
import { AppComponentBase } from "@shared/app-component-base";
import {
  SectionHistoryDto,
  SectionsServiceProxy,
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

@Component({
  selector: 'app-section-history',
  // standalone: true,
  // imports: [],
  templateUrl: './section-history.component.html',
  styleUrl: './section-history.component.css',
  animations: [appModuleAnimation()]

})
export class SectionHistoryComponent extends AppComponentBase {

  sectionHistory: SectionHistoryDto[] = [];
  primengTableHelper: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  eventClone: LazyLoadEvent;

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private _sectionService: SectionsServiceProxy,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
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
    this._sectionService
      .getSectionsHistory(
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
      createOrEditDialog = this._modalService.show(AddEditSectionComponent, {
        class: "modal-lg modal-dialog-centered",
        backdrop: "static",
        ignoreBackdropClick: true,
      });
    } else {
      createOrEditDialog = this._modalService.show(AddEditSectionComponent, {
        class: "modal-lg modal-dialog-centered",
        backdrop: "static",
        ignoreBackdropClick: true,
        initialState: {
          id: id,
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
    this.showCreateOrEditDialog();
  }

  edit(sectionHistory: SectionHistoryDto): void {
    this.showCreateOrEditDialog(sectionHistory.id);
  }

}
