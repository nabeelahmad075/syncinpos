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
  selector: "app-floor",
  templateUrl: "./floor.component.html",
  styleUrls: ["./floor.component.css"],
  animations: [appModuleAnimation()],
})
export class FloorComponent extends AppComponentBase implements OnInit {
  saving = false;
  id: number;
  tblLocation: SelectItem[] = [];
  tblFloor: FloorEntityDto = new FloorEntityDto();
  tblFloorHistory: FloorEntityDto[] = [];
  @Output() onSave = new EventEmitter<any>();
  primengTableHelper: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  eventClone: LazyLoadEvent;
  createFloorDialog: BsModalRef;

  constructor(
    injector: Injector,
    private _locationService: LocationServiceProxy,
    private _floorService: FloorEntityServiceProxy,
    private _modalService: BsModalService,
    public bsModalRef: BsModalRef,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);
    this.tblFloor.isActive = true;
  }

  ngOnInit(): void {
    if (this.id > 0) {
      this.getById();
    }
    this.getLocationDropdown();
  }

  getLocationDropdown() {
    this.tblLocation = [];
    this._locationService.getLocationDropDown().subscribe((result) => {
      this.tblLocation = result;
      this.cdr.detectChanges();
    });
  }

  save(): void {
    this.saving = true;
    if (this.id) {
      this.update();
    } else this.create();

    // debugger
    // if (this.saving) {
    //   // this.id = 0;
    //   // this.tblFloor = new FloorEntityDto();
    //   // this.tblFloor.isActive = true;
    //   //   this.getHistory(event);
      
    // // this.createFloorDialog = this._modalService.show(FloorComponent, {
    // //   class: "modal-lg modal-dialog-centered",
    // //   backdrop: "static",
    // //   ignoreBackdropClick: true,
    // // });
    // // this.createFloorDialog.content.onSave.subscribe((value) => {
    // //   if (value) {
    // //     this.getHistory({});
    // //   }
    // // });
    // }
  }

  update(): void {
    if (!this.tblFloor.locationId) {
      abp.notify.error("Please Select Location.");
      this.saving = false;
    }
    if (!this.tblFloor.title) {
      abp.notify.error("Please Enter Floor Name.");
      this.saving = false;
    }
    this._floorService.update(this.tblFloor).subscribe({
      next: (value: any) => {
        this.notify.info("Update Successfuly");
        this.bsModalRef.hide();
        this.onSave.emit(true);
      },
      error: (err) => {
        this.saving = false;
      },
    });
  }

  create(): void {
    if (!this.tblFloor.locationId) {
      abp.notify.error("Please Select Location.");
      this.saving = false;
    }
    if (!this.tblFloor.title) {
      abp.notify.error("Please Enter Floor Name.");
      this.saving = false;
    }
    this._floorService.create(this.tblFloor).subscribe({
      next: () => {
        this.notify.info("Saved Successfuly");
        this.bsModalRef.hide();
        this.onSave.emit(true);
      },
      error: (err) => {
        this.saving = false;
      },
    });
  }

  getById(id?: number) {
    this._floorService.get(id).subscribe((result) => {
      if (result) this.id = result.id;
      else this.id = 0;

      this.tblFloor = result;
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
    this._floorService
      .getFloorHistory(
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
