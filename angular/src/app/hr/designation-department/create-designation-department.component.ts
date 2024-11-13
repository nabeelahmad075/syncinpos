import { Component, Injector, OnInit, EventEmitter, Output, ChangeDetectorRef, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { DesignationServiceProxy, DesignationsDto, DesignationHistoryDto, 
  DepartmentServiceProxy, DepartmentsDto, DepartmentsHistoryDto } from '@shared/service-proxies/service-proxies';
import { result } from 'lodash-es';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { LazyLoadEvent, SelectItem } from "primeng/api";
import { Dropdown } from 'primeng/dropdown'; 
import { PrimengTableHelper } from '@shared/helpers/primengTableHelper';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-create-designation-department',
  // standalone: true,
  // imports: [],
  templateUrl: './create-designation-department.component.html',
  styleUrl: './create-designation-department.component.css',
  animations: [appModuleAnimation()],
  providers: [DesignationServiceProxy, DepartmentServiceProxy]
})
export class CreateDesignationDepartmentComponent extends AppComponentBase implements OnInit{

  saving = false;
  flgDesignation: boolean = true;
  id: number;
  tblDesignation: DesignationsDto = new DesignationsDto();
  tblDepartment: DepartmentsDto = new DepartmentsDto();
  tblDesignationHistory: DesignationHistoryDto [] = [];
  tblDepartmentHistory: DepartmentsHistoryDto [] = [];
  @Output() onSave = new EventEmitter<any>();
  primengDesignationTable: PrimengTableHelper = new PrimengTableHelper();
  primengDepartmentTable: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  eventClone: LazyLoadEvent;

  constructor(
    injector: Injector,
    private _designationService: DesignationServiceProxy,
    private _departmentService: DepartmentServiceProxy,
    public bsModalRef: BsModalRef,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.flgDesignation = true;
    this.getDepartmentHistory();
    this.getDesignationHistory();
  }

  getDesignationById(id?: number) {
    this._designationService.get (id).subscribe((result) => {
      
      if (result)
        this.id = result.id
      else
        this.id = 0
      
      this.tblDesignation = result;
      this.cdr.detectChanges();
    })
  }

  getDepartmentById(id?: number) {
    this._departmentService.get (id).subscribe((result) => {

      if (result)
        this.id = result.id
      else
        this.id = 0

      this.tblDepartment = result;
      this.cdr.detectChanges();
    })
  }

  save(): void {
    this.saving = true;
    if (this.id) {



      if(this.flgDesignation)
      this.updateDesignation();

      if(!this.flgDesignation)
      this.updateDepartment();

    } else{

      if(this.flgDesignation)
      this.createDesignation();

      if(!this.flgDesignation)
      this.createDepartment();
    }

  }

  updateDesignation(): void {
    
    if (!this.tblDesignation.title) {
      abp.notify.error("Please Enter Designation.")
      return
    }
    this._designationService.update(this.tblDesignation).subscribe({
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

  updateDepartment(): void {
    if (!this.tblDepartment.title) {
      abp.notify.error("Please Enter Department.")
      return
    }
    this._departmentService.update(this.tblDepartment).subscribe({
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

  createDesignation(): void {
    
    if (!this.tblDesignation.title) {
      abp.notify.error("Please Enter Designation.")
      return
    }
    this._designationService.create(this.tblDesignation).subscribe({
      
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
  
  createDepartment(): void {
    if (!this.tblDepartment.title) {
      abp.notify.error("Please Enter Department.")
      return
    }
    this._departmentService.create(this.tblDepartment).subscribe({
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

  getDesignationHistory(event?: LazyLoadEvent) {
    if (this.primengDesignationTable.shouldResetPaging(event)) {
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
    this._designationService
      .getHistory(
        event && event.filters && event.filters["global"]
          ? event.filters["global"].value
          : undefined,
        "",
        this.primengDesignationTable.getSkipCount(this.paginator, event),
        this.primengDesignationTable.getModalMaxResultCount(this.paginator, event)
      )
      .subscribe((result) => {
        this.primengDesignationTable.records = result.items;
        this.primengDesignationTable.totalRecordsCount = result.totalCount;
        this.cdr.detectChanges();
      })
      .add(() => abp.ui.clearBusy());
  }

  getDepartmentHistory(event?: LazyLoadEvent) {
    if (this.primengDepartmentTable.shouldResetPaging(event)) {
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
    this._departmentService
      .getHistory(
        event && event.filters && event.filters["global"]
          ? event.filters["global"].value
          : undefined,
        "",
        this.primengDepartmentTable.getSkipCount(this.paginator, event),
        this.primengDepartmentTable.getModalMaxResultCount(this.paginator, event)
      )
      .subscribe((result) => {
        this.primengDepartmentTable.records = result.items;
        this.primengDepartmentTable.totalRecordsCount = result.totalCount;
        this.cdr.detectChanges();
      })
      .add(() => abp.ui.clearBusy());
  }

  newDesignation(){
    this.id = 0;
    this.tblDesignation = new DesignationsDto();
  }

  newDepartment(){
    this.id = 0;
    this.tblDepartment = new DepartmentsDto();
  }

 getMeTab(tabId: number){

  if(tabId == 0)
    this.flgDesignation = true;
  
  else
    this.flgDesignation = false;

 }

}
