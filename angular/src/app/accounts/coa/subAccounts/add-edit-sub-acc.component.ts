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
  MainAccountServiceProxy,
SubAccountDto, SubAccountServiceProxy
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
  selector: 'app-add-edit-sub-acc',
  // standalone: true,
  // imports: [],
  templateUrl: './add-edit-sub-acc.component.html',
  styleUrl: './add-edit-sub-acc.component.css',
  animations: [appModuleAnimation()]
})
export class AddEditSubAccComponent extends AppComponentBase implements OnInit {

  saving = false;
  id: number;
  subCode: string;
  tblMainAccounts: SelectItem[] = [];
  tblAccountType: SelectItem[] = [];
  tblSubAccounts: SubAccountDto = new SubAccountDto();
  tblSubAccountsHistory: SubAccountDto[] = [];
  @Output() onSave = new EventEmitter<any>();
  primengTableHelper: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  eventClone: LazyLoadEvent;

  constructor(
    injector: Injector,
    private _mainAccountService: MainAccountServiceProxy,
    private _subAccService: SubAccountServiceProxy,
    private _modalService: BsModalService,
    public bsModalRef: BsModalRef,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);
    this.tblSubAccounts.isActive = true;
  }

  ngOnInit(): void {
    this.getMainAccDropdown();
    this.getAccTypeDropdown();
  }

  getMainAccDropdown() {
    this.tblMainAccounts = [];
    this._mainAccountService.getMainAccountDropdown().subscribe((result) => {
      this.tblMainAccounts = result;
      this.cdr.detectChanges();
    });
  }
  
  getAccTypeDropdown() {
    this.tblAccountType = [];
    this._subAccService.getAccountTypeDropdown().subscribe((result) => {
      this.tblAccountType = result;
      this.cdr.detectChanges();
    });
  }

  save(): void {
    this.saving = true;
    this.tblSubAccounts.subCode = this.subCode;
    // this.tblMainAccounts.locationId = undefined;
    if (this.id) {
      this.update();
    } else 
    this.create();
  }

  update(): void {
    if (!this.tblSubAccounts.accountTypeId) {
      abp.notify.error("Please Select Sub Type.");
      this.saving = false;
    }
    if (!this.tblSubAccounts.subTitle) {
      abp.notify.error("Please Enter Sub Account Name.");
      this.saving = false;
    }
    this._subAccService.update(this.tblSubAccounts).subscribe({
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
    if (!this.tblSubAccounts.accountTypeId) {
      abp.notify.error("Please Select Sub Type.");
      this.saving = false;
    }
    if (!this.tblSubAccounts.subTitle) {
      abp.notify.error("Please Enter Sub Account Name.");
      this.saving = false;
    }
    this._subAccService.create(this.tblSubAccounts).subscribe({
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
    this._subAccService.get(id).subscribe((result) => {
      if (result) this.id = result.id;
      else this.id = 0;

      this.tblSubAccounts = result;
      this.subCode = result.subCode;
      this.cdr.detectChanges();
    });
  }

  getSubCode(mainAccountId: number) {
    this._subAccService.getNewSubAccountCode(mainAccountId).subscribe((result) => {
      this.subCode = result;
      this.cdr.detectChanges();
    });
  }

  getSubAccHistory(event?: LazyLoadEvent) {
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
    this._subAccService
      .getSubAccountHistory(
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
