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
  SubAccountServiceProxy,
  SubAccountDto,
  LocationServiceProxy,
  LocationDto,
  CustomerServiceProxy,
  CustomerDto,
  SelectItemDto,
} from "@shared/service-proxies/service-proxies";
import { result } from "lodash-es";
import * as moment from "moment";
import { BsModalRef } from "ngx-bootstrap/modal";
import { LazyLoadEvent, SelectItem } from "primeng/api";
import { Dropdown } from "primeng/dropdown";

@Component({
  selector: "app-add-edit-customer",
  // standalone: true,
  // imports: [],
  templateUrl: "./add-edit-customer.component.html",
  styleUrl: "./add-edit-customer.component.css",
  animations: [appModuleAnimation()]
})
export class AddEditCustomerComponent extends AppComponentBase implements OnInit {

  saving = false;
  id: number;
  empCode: string;
  tblLocation: SelectItem[] = [];
  tblSubAccounts: SelectItem[] = [];
  tblCustomer: CustomerDto = new CustomerDto();
  @Output() onSave = new EventEmitter<any>();
  dateOfBirth: Date = new Date();
  eventClone: LazyLoadEvent;

  //enum functionality, also amend appconsts file
  private _gender = AppConsts.genderList;
  public get Gender() {
    return this._gender;
  }
  public set Gender(value) {
    this._gender = value;
  }
  
  constructor(
    injector: Injector,
    private _locationService: LocationServiceProxy,
    private _subAccountService: SubAccountServiceProxy,
    private _customerService: CustomerServiceProxy,
    public bsModalRef: BsModalRef,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);
    this.tblCustomer.isActive = true;
  }

  ngOnInit(): void {
    if (this.id > 0) {
      this.getById();
    }
    this.getLocationDropdown();
    this.getSubAccountDropdown();
    this.dateOfBirth = new Date();
  }

  getLocationDropdown() {
    this.tblLocation = [];
    this._locationService.getLocationDropDown().subscribe((result) => {
      this.tblLocation = result;
      this.cdr.detectChanges();
    });
  }

  getSubAccountDropdown() {
    this.tblSubAccounts = [];
    this._subAccountService.getSubAccountDropdownOnAccountType('Customer').subscribe((result) => {
      this.tblSubAccounts = result;
      this.cdr.detectChanges();
    });
  }

  save(): void {
    this.saving = true;
    this.tblCustomer.dateOfBirth = moment(this.dateOfBirth);
    if (this.id) {
      this.update();
    } else this.create();
  }

  update(): void {
    if (!this.tblCustomer.locationId) {
      abp.notify.error("Please Select Location.");
    }
    if (!this.tblCustomer.subAccountId) {
      abp.notify.error("Please Select Customer Type.");
    }
    if (!this.tblCustomer.name) {
      abp.notify.error("Please Enter Customer Name.");
    }
    if (!this.tblCustomer.contactNo) {
      abp.notify.error("Please Enter Contact No.");
    }
    this._customerService.update(this.tblCustomer).subscribe({
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
    if (!this.tblCustomer.locationId) {
      abp.notify.error("Please Select Location.");
    }
    if (!this.tblCustomer.subAccountId) {
      abp.notify.error("Please Select Customer Type.");
    }
    if (!this.tblCustomer.name) {
      abp.notify.error("Please Enter Customer Name.");
    }
    if (!this.tblCustomer.contactNo) {
      abp.notify.error("Please Enter Contact No.");
    }
    this._customerService.create(this.tblCustomer).subscribe({
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
    this._customerService.get(this.id).subscribe((result) => {
      this.tblCustomer = result;
      this.dateOfBirth = this.tblCustomer.dateOfBirth.toDate();
      this.cdr.detectChanges();
    });
  }
  

}
