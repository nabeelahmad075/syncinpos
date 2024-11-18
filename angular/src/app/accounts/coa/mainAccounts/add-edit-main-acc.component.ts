import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef,
  ViewChild,
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/app-component-base";
import {
  MainAccountDto, MainAccountServiceProxy,
  FloorEntityDto,
  FloorEntityServiceProxy,
  LocationServiceProxy,
  LocationDto,
} from "@shared/service-proxies/service-proxies";
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from "@shared/paged-listing-component-base";
import { result } from "lodash-es";
import * as moment from "moment";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { LazyLoadEvent, SelectItem } from "primeng/api";
import { Dropdown } from "primeng/dropdown";
import { PrimengTableHelper } from "@shared/helpers/primengTableHelper";
import { Table } from "primeng/table";
import { Paginator } from "primeng/paginator";

@Component({
  selector: 'app-add-edit-main-acc',
  // standalone: true,
  // imports: [],
  templateUrl: './add-edit-main-acc.component.html',
  styleUrl: './add-edit-main-acc.component.css',
  animations: [appModuleAnimation()]
})
export class AddEditMainAccComponent extends AppComponentBase implements OnInit {

  saving = false;
  id: number;
  // tblLocation: SelectItem[] = [];
  tblMainType: SelectItem[] = [];
  tblMainAccounts: MainAccountDto = new MainAccountDto();
  tblMainAccountsHistory: MainAccountDto[] = [];
  @Output() onSave = new EventEmitter<any>();
  primengTableHelper: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  eventClone: LazyLoadEvent;

  constructor(
    injector: Injector,
    // private _locationService: LocationServiceProxy,
    private _mainTypeService: MainAccountServiceProxy,
    private _mainAccountService: MainAccountServiceProxy,
    private _modalService: BsModalService,
    public bsModalRef: BsModalRef,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);
    this.tblMainAccounts.isActive = true;
  }

  ngOnInit(): void {
    if (this.id > 0) {
      this.getById();
    }
    this.getMainTypeDropdown();
  }

  getMainTypeDropdown() {
    this.tblMainType = [];
    this._mainTypeService.getMainTypeDropdown().subscribe((result) => {
      this.tblMainType = result;
      this.cdr.detectChanges();
    });
  }

  save(): void {
    this.saving = true;
    if (this.id) {
      this.update();
    } else 
    this.create();
  }

  update(): void {
    if (!this.tblMainAccounts.mainTypeId) {
      abp.notify.error("Please Select Main Type.");
      this.saving = false;
    }
    if (!this.tblMainAccounts.mainTitle) {
      abp.notify.error("Please Enter Main Account Name.");
      this.saving = false;
    }
    this._mainAccountService.update(this.tblMainAccounts).subscribe({
      next: (value: any) => {
        this.notify.success("Update Successfuly");
        this.bsModalRef.hide();
        this.onSave.emit(true);
      },
      error: (err) => {
        this.saving = false;
      },
    });
  }

  create(): void {
    if (!this.tblMainAccounts.mainTypeId) {
      abp.notify.error("Please Select Main Type.");
      this.saving = false;
    }
    if (!this.tblMainAccounts.mainTitle) {
      abp.notify.error("Please Enter Main Account Name.");
      this.saving = false;
    }
    this._mainAccountService.create(this.tblMainAccounts).subscribe({
      next: () => {
        this.notify.success("Saved Successfuly");
        this.bsModalRef.hide();
        this.onSave.emit(true);
      },
      error: (err) => {
        this.saving = false;
      },
    });
  }

  getById(id?: number) {
    this._mainAccountService.get(id).subscribe((result) => {
      if (result) this.id = result.id;
      else this.id = 0;

      this.tblMainAccounts = result;
      this.cdr.detectChanges();
    });
  }

  getHistory(event?: LazyLoadEvent) {
    debugger
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
    this._mainAccountService
      .getMainAccountHistory(
        event && event.filters && event.filters["global"]
          ? event.filters["global"].value
          : undefined,
        "",
        this.primengTableHelper.getSkipCount(this.paginator, event),
        this.primengTableHelper.getModalMaxResultCount(this.paginator, event)
      )
      .subscribe((result) => {
        this.primengTableHelper.records = result.items;
        this.primengTableHelper.totalRecordsCount = result.totalCount;
        this.cdr.detectChanges();
      })
      .add(() => abp.ui.clearBusy());
  }
}
 