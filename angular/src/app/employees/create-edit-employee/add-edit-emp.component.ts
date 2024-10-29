import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef,
} from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import {
  DepartmentServiceProxy,
  DepartmentsDto,
  DesignationServiceProxy,
  DesignationsDto,
  LocationServiceProxy,
  LocationDto,
  EmployeeDto,
  EmployeeServiceProxy
} from "@shared/service-proxies/service-proxies";
import { result } from "lodash-es";
import { BsModalRef } from "ngx-bootstrap/modal";
import { SelectItem } from "primeng/api";
import { Dropdown } from "primeng/dropdown";

@Component({
  selector: "app-add-edit-emp",
  // standalone: true,
  // imports: [],
  templateUrl: "./add-edit-emp.component.html",
  styleUrl: "./add-edit-emp.component.css",
})
export class AddEditEmpComponent extends AppComponentBase implements OnInit {

  saving = false;
  id: number;
  tblLocation: SelectItem[] = [];
  tblDesignation: SelectItem[] = [];
  tblDepartment: SelectItem[]=[];
  tblEmployee: EmployeeDto = new EmployeeDto();
  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _departmentService: DepartmentServiceProxy,
    private _designationService: DesignationServiceProxy,
    private _locationService: LocationServiceProxy,
    private _empService: EmployeeServiceProxy,
    public bsModalRef: BsModalRef,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    if (this.id > 0){
      this.getById();
    }
    this.getLocationDropdown();
    this.getDepartmentDropdown();
    this.getDesignationDropdown();
  }

  getLocationDropdown() {
    this.tblLocation = [];
    this._locationService.getLocationDropDown().subscribe(
      (result) => {
        this.tblLocation = result;
        this.cdr.detectChanges();
      }
    );
  }

  getDesignationDropdown() {
    this.tblDesignation = [];
    this._designationService.getDesignationDropdown().subscribe(
      (result) => {
        this.tblDesignation = result;
        this.cdr.detectChanges();
      }
    );
  }

  getDepartmentDropdown() {
    this.tblDepartment = [];
    this._departmentService.getDepartmentsDropDown().subscribe(
      (result) => {
        this.tblDepartment = result;
        this.cdr.detectChanges();
      }
    );
  }

  save(): void {
    this.saving = true;
    if (this.id) {
      this.update();
    } else
      this.create();
  }

  update(): void {
    if (!this.tblEmployee.locationId) {
      abp.notify.error("Please Select Location.")
    }
    if (!this.tblEmployee.employeeName) {
      abp.notify.error("Please Enter Employee Name.")
    }
    if (!this.tblEmployee.mobileNo) {
      abp.notify.error("Please Enter Mobile No.")
    }
    debugger
    this._empService.update(this.tblEmployee).subscribe({
      next: (value:any) => {
        this.notify.info("Update Successfuly");
        this.bsModalRef.hide();
        this.onSave.emit(true);
      },
      error: (err) => {
        this.saving = false;
      }
    })
  }

  create(): void {
    if (!this.tblEmployee.locationId) {
      abp.notify.error("Please Select Location.")
    }
    if (!this.tblEmployee.employeeName) {
      abp.notify.error("Please Enter Employee Name.")
    }
    if (!this.tblEmployee.mobileNo) {
      abp.notify.error("Please Enter Mobile No.")
    }
    this._empService.create(this.tblEmployee).subscribe({
      next: () => {
        this.notify.info("Saved Successfuly");
        this.bsModalRef.hide();
        this.onSave.emit(true);
      },
      error: (err) => {
        this.saving = false;
      }
    })
  }

  getById (){
    this._empService.get (this.id).subscribe((result) => {
      this.tblEmployee= result;
      this.cdr.detectChanges();
    })
  }

}
