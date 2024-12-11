import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef,
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/app-component-base";
import { AppConsts } from "@shared/AppConsts";
import {
  LocationServiceProxy,
  VoucherMasterDto,
  VoucherDetailDto,
  VoucherServiceProxy,
  DetailAccountServiceProxy
} from "@shared/service-proxies/service-proxies";
import { result } from "lodash-es";
import * as moment from "moment";
import { BsModalRef } from "ngx-bootstrap/modal";
import { LazyLoadEvent, SelectItem } from "primeng/api";
import { Dropdown } from "primeng/dropdown";
import { SharedService } from "@shared/shared.service";
import { sharedHelperFunctions } from "@shared/helpers/sharedHelperFunctions";

@Component({
  selector: 'app-add-edit-voucher',
  // standalone: true,
  // imports: [],
  templateUrl: './add-edit-voucher.component.html',
  styleUrl: './add-edit-voucher.component.css',
  animations: [appModuleAnimation()]
})
export class AddEditVoucherComponent extends AppComponentBase implements OnInit {

  saving = false;
  id: number;
  currentVoucherNo: string;
  tblLocation: SelectItem[] = [];
  tblVoucherMaster: VoucherMasterDto = new VoucherMasterDto();
  tblVoucherType: SelectItem[] = [];
  tblDetailAccounts: SelectItem [] = [];
  //tblVoucherDetail: VoucherDetailDto = new VoucherDetailDto();
  @Output() onSave = new EventEmitter<any>();
  voucherDate: Date = new Date();
  eventClone: LazyLoadEvent;

  constructor(
    injector: Injector,
    private _locationService: LocationServiceProxy,
    private _voucherService: VoucherServiceProxy,
    private _detailAccService: DetailAccountServiceProxy,
    public _sharedService: SharedService,
    public bsModalRef: BsModalRef,
    private cdr: ChangeDetectorRef,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.tblVoucherMaster.voucherDetails = [];
    this.getLocationDropdown();
    this.getVoucherType();
    this.addVoucherDetails(0);
    this.getDetailAccounts();
    this.voucherDate = new Date();

    if (this.id > 0) {
      this.getById();
    }
    else
    {
      this.getVoucherNumber();
    }

    this.cdr.detectChanges();
  }

  getLocationDropdown() {
    this.tblLocation = [];
    this._locationService.getLocationDropDown().subscribe((result) => {
      this.tblLocation = result;
      console.log(result);
      this.cdr.detectChanges();
    });
  }

  getVoucherType(){
    this.tblVoucherType = [];
    this._voucherService.getVoucherTypeDropdown().subscribe((result) => {
      this.tblVoucherType = result;   
      this.cdr.detectChanges();
    });
  }

  save(): void {

    let debit = this.sum("debitAmount");
    let credit = this.sum("creditAmount");

    if (debit - credit != 0){
      abp.notify.error("Unbalanced voucher.");
      return;
    }

    this.saving = true;
    this.tblVoucherMaster.voucherDate = moment(this.voucherDate);
    if (this.id) {
      this.update();
    } else this.create();
  }

  
  update(): void {
    if (!this.tblVoucherMaster.locationId) {
      abp.notify.error("Please Select Location.");
      return;
    }
    this._voucherService.update(this.tblVoucherMaster).subscribe({
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
    if (!this.tblVoucherMaster.locationId) {
      abp.notify.error("Please Select Location.");
    }
    this._voucherService.create(this.tblVoucherMaster).subscribe({
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

  getById() {
    this._voucherService.get(this.id).subscribe((result) => {
      console.log(result);
      this.tblVoucherMaster = result;
      this.currentVoucherNo = result.voucherNo;
      this.tblVoucherMaster.voucherDetails.forEach(item => {item['uid'] = this.getUniqueId()});
      this.voucherDate = this.tblVoucherMaster.voucherDate.toDate();
      this.cdr.detectChanges();
    });
  }

  getVoucherNumber(){
    const formattedDate = moment(this.voucherDate).format('YYMM');
    this._voucherService.getNewDocNo(this.currentVoucherNo, this.id, this.tblVoucherMaster.voucherTypeId, 
      formattedDate, this.tblVoucherMaster.locationId).subscribe((result) => 
    this.tblVoucherMaster.voucherNo = result);
    this.cdr.detectChanges();
  }

  getDetailAccounts(){
    this._detailAccService.getDetailAccountDropdown().subscribe((result) => {
      this.tblDetailAccounts = result;
      this.cdr.detectChanges();
    });
  }

  getUniqueId(): string {
    return this._sharedService.getUniqueId();
  }

  addVoucherDetails(index: number) {
    let tempDetail = new VoucherDetailDto();
    tempDetail.id = 0;
    tempDetail.detailAccountId = 0;
    tempDetail.description = "";
    tempDetail.debitAmount = 0;
    tempDetail.creditAmount = 0;

    tempDetail["uid"] = this.getUniqueId();
    this.tblVoucherMaster.voucherDetails.splice(index+1, 0, tempDetail);
  }

  removeVoucherDetails(index: number) {
    this.tblVoucherMaster.voucherDetails.splice(index, 1);
    if (this.tblVoucherMaster.voucherDetails.length == 0) this.addVoucherDetails(0);
  }

  sum(col: string) {
    return this.getSum(col, this.tblVoucherMaster.voucherDetails);
  }

  getSum(column:string,list:any) {
    return sharedHelperFunctions.getSum(column, list);
  }
}
