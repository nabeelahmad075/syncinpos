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
  DetailAccountDto,
  DetailAccountServiceProxy,
  SelectItemDto,
  SubAccountServiceProxy,
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
  selector: "app-add-edit-detail-acc",
  // standalone: true,
  // imports: [],
  templateUrl: "./add-edit-detail-acc.component.html",
  styleUrl: "./add-edit-detail-acc.component.css",
  animations: [appModuleAnimation()],
})
export class AddEditDetailAccComponent
  extends AppComponentBase
  implements OnInit
{
  saving = false;
  id: number;
  detailCode: string;
  isReadonlyTitle: boolean = true;
  tblSubAccounts: SelectItemDto[] = [];
  tblDetailAccounts: DetailAccountDto = new DetailAccountDto();
  tblDetailAccountsHistory: DetailAccountDto[] = [];
  @Output() onSave = new EventEmitter<any>();
  primengTableHelper: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  eventClone: LazyLoadEvent;

  constructor(
    injector: Injector,
    private _subAccService: SubAccountServiceProxy,
    private _detailAccService: DetailAccountServiceProxy,
    private _modalService: BsModalService,
    public bsModalRef: BsModalRef,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);
    this.tblDetailAccounts.isActive = true;
  }

  ngOnInit(): void {
    if(this.id>0)
      this.getById(this.id);
    this.getDetailAccDropdown();
  }

  setIsReadonlyTitle(subAccountId: number = undefined) {
    const selectedSubAcc = this.tblSubAccounts.find(
      (acc) => acc.value === subAccountId
    );
    if (selectedSubAcc && selectedSubAcc.code == "True") 
    {
      this.isReadonlyTitle = true;
    }
    else 
    {
      this.isReadonlyTitle = false;
    }

  }

  getDetailAccDropdown() {
    this.tblSubAccounts = [];
    this._subAccService.getSubAccountDropdown().subscribe((result) => {
      this.tblSubAccounts = result;
      this.cdr.detectChanges();
    });
  }

  save(): void {
    this.saving = true;
    this.tblDetailAccounts.detailCode = this.detailCode;
    // this.tblMainAccounts.locationId = undefined;
    if (this.id) {
      this.update();
    } else this.create();
  }

  update(): void {
    if (!this.tblDetailAccounts.subAccountId) {
      abp.notify.error("Please Select Sub Account.");
      this.saving = false;
    }
    if (!this.tblDetailAccounts.detailTitle) {
      abp.notify.error("Please Enter Detail Account Name.");
      this.saving = false;
    }
    this._detailAccService.update(this.tblDetailAccounts).subscribe({
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
    if (!this.tblDetailAccounts.subAccountId) {
      abp.notify.error("Please Select Sub Account.");
      this.saving = false;
    }
    if (!this.tblDetailAccounts.detailTitle) {
      abp.notify.error("Please Enter Detail Account Name.");
      this.saving = false;
    }
    this._detailAccService.create(this.tblDetailAccounts).subscribe({
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
    this._detailAccService.get(id).subscribe((result) => {
      if (result) this.id = result.id;
      else this.id = 0;

      this.tblDetailAccounts = result;
      this.detailCode = result.detailCode;
      this.setIsReadonlyTitle(result.subAccountId);
      this.cdr.detectChanges();
    });
  }

  getDetailCode(subAccountId: number, id?: number) {

    this.setIsReadonlyTitle(subAccountId);
    this.cdr.detectChanges();

    this._detailAccService
      .getNewDetailAccountCode(subAccountId, id)
      .subscribe((result) => {
        this.detailCode = result;
        this.cdr.detectChanges();
      });

  }
}
