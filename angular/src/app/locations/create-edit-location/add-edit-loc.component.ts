import { Component, Injector, OnInit} from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { RegionServiceProxy, RegionDto, LocationDto, LocationServiceProxy} from '@shared/service-proxies/service-proxies';
import { result } from 'lodash-es';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { SelectItem } from "primeng/api";
import { DropdownModule } from 'primeng/dropdown';



@Component({
  selector: 'app-add-edit-loc',
  //standalone: true,
  //imports: [ DropdownModule ],
  templateUrl: './add-edit-loc.component.html',
  styleUrl: './add-edit-loc.component.css',
  providers: [RegionServiceProxy]
})
export class AddEditLocComponent extends AppComponentBase implements OnInit{


  regions: SelectItem[] = [];
  location: LocationDto = new LocationDto();
  constructor(
    injector: Injector,
    private _regionService: RegionServiceProxy,
    public bsModalRef : BsModalRef
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.getRegionsDropdown();
  }
  getRegionsDropdown (){
    this._regionService.getRegionsDropDown().subscribe(
      (result) => {
        console.log(result)
        this.regions = result;
      }
    );
  }
}
