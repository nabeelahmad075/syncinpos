import { Component, Injector, OnInit, EventEmitter, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import {
  RegionServiceProxy, RegionDto, LocationDto, LocationServiceProxy,
  LocationTypeDto, LocationTypeServiceProxy
} from '@shared/service-proxies/service-proxies';
import { result } from 'lodash-es';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { SelectItem } from "primeng/api";



@Component({
  selector: 'app-add-edit-loc',
  //standalone: true,
  //imports: [ DropdownModule ],
  templateUrl: './add-edit-loc.component.html',
  styleUrl: './add-edit-loc.component.css',
  providers: [RegionServiceProxy, LocationTypeServiceProxy, LocationServiceProxy]
})
export class AddEditLocComponent extends AppComponentBase implements OnInit {

  saving = false;
  id: number;
  tblRegions: SelectItem[] = [];
  tblLocationType: SelectItem[] = [];
  tenantName: string;
  tblLocation: LocationDto = new LocationDto();
  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _regionService: RegionServiceProxy,
    private _locationTypeService: LocationTypeServiceProxy,
    private _locationService: LocationServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.tenantName = this.appSession.tenant.name;
    this.getRegionsDropdown();
    this.getLocationTypeDropdown();
    if (this.id > 0){
      this.getLocationById();
    }
  }

  getRegionsDropdown() {
    this._regionService.getRegionsDropDown().subscribe(
      (result) => {
        console.log(result)
        this.tblRegions = result;
      }
    );
  }

  getLocationTypeDropdown() {
    this._locationTypeService.getLocationTypesDropDown().subscribe(
      (result) => {
        console.log(result)
        this.tblLocationType = result;
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
    if (!this.tblLocation.locationCode) {
      abp.notify.error("Please Enter Location Code.")
    }
    if (!this.tblLocation.locationName) {
      abp.notify.error("Please Enter Location Name.")
    }
    this._locationService.update(this.tblLocation).subscribe({
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
    if (!this.tblLocation.locationCode) {
      abp.notify.error("Please Enter Location Code.")
    }
    if (!this.tblLocation.locationName) {
      abp.notify.error("Please Enter Location Name.")
    }
    this.tblLocation.isActive = false;
    this._locationService.create(this.tblLocation).subscribe({
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

  getLocationById (){
    this._locationService.get (this.id).subscribe((result) => {
      this.tblLocation= result;
    })
  }
}
