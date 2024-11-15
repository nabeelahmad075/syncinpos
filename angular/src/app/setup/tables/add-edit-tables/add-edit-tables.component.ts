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
import {
  FloorEntityDto, 
  FloorEntityServiceProxy,
  LocationServiceProxy,
  LocationDto,
  TableEntityDto, TableEntityServiceProxy
} from "@shared/service-proxies/service-proxies";
import { result } from "lodash-es";
import * as moment from "moment";
import { BsModalRef } from "ngx-bootstrap/modal";
import { LazyLoadEvent, SelectItem } from "primeng/api";
import { Dropdown } from "primeng/dropdown";

@Component({
  selector: 'app-add-edit-tables',
  templateUrl: './add-edit-tables.component.html',
  styleUrls: ['./add-edit-tables.component.css'],
  animations: [appModuleAnimation()]
})

export class AddEditTablesComponent extends AppComponentBase implements OnInit {

  saving = false;
  id: number;
  tblLocation: SelectItem[] = [];
  tblFloor: SelectItem[] = [];
  tblTables: TableEntityDto = new TableEntityDto();
  @Output() onSave = new EventEmitter<any>();
  eventClone: LazyLoadEvent;

  constructor(    
    injector: Injector,
    private _locationService: LocationServiceProxy,
    private _floorService: FloorEntityServiceProxy,
    private _tableService: TableEntityServiceProxy,
    public bsModalRef: BsModalRef,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);
    this.tblTables.isActive = true;
  }

  ngOnInit(): void {
    if (this.id > 0){
      this.getById();
    }
    this.getLocationDropdown();
    // this.getFloorDropdown();
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

  getFloorDropdown(locationId: number) {
    this.tblFloor = [];
    this._floorService.getFloorDropdown(locationId).subscribe(
      (result) => {
        this.tblFloor = result;
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
    if (!this.tblTables.locationId) {
      abp.notify.error("Please Select Location.")
    }
    if (!this.tblTables.floorId) {
      abp.notify.error("Please Select Floor.")
    }
    this._tableService.update(this.tblTables).subscribe({
      next: (value:any) => {
        this.notify.success("Update Successfuly");
        this.bsModalRef.hide();
        this.onSave.emit(true);
      },
      error: (err) => {
        this.saving = false;
      }
    })
  }

  create(): void {
    if (!this.tblTables.locationId) {
      abp.notify.error("Please Select Location.")
    }
    if (!this.tblTables.floorId) {
      abp.notify.error("Please Select Floor.")
    }
    this._tableService.create(this.tblTables).subscribe({
      next: () => {
        this.notify.success("Saved Successfuly");
        this.bsModalRef.hide();
        this.onSave.emit(true);
      },
      error: (err) => {
        this.saving = false;
      }
    })
  } 

  getById (){
    this._tableService.get (this.id).subscribe((result) => {
      this.tblTables = result;
      this.getFloorDropdown(result.locationId);
      this.cdr.detectChanges();
    })
  }

}
