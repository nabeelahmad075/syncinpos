import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Injector,
  OnInit,
  Output,
  ViewChild,
  ViewEncapsulation,
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { AddEditPriceComponent } from "./create-edit-price/add-edit-price.component";
import { extend, result, sortBy } from "lodash-es";
import { AppComponentBase } from "@shared/app-component-base";
import {
  ItemCategoryServiceProxy,
  ItemPriceListDto,
  ItemPriceServiceProxy,
  LocationServiceProxy,
} from "@shared/service-proxies/service-proxies";
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from "@shared/paged-listing-component-base";
import { finalize } from "rxjs/operators";
import { TableModule, Table } from "primeng/table";
import { DropdownModule } from "primeng/dropdown";
import { PrimengTableHelper } from "@shared/helpers/primengTableHelper";
import { Paginator, PaginatorModule } from "primeng/paginator";
import { LazyLoadEvent, SelectItem } from "primeng/api";
import * as moment from "moment";
import { CreateDesignationDepartmentComponent } from "@app/hr/designation-department/create-designation-department.component";
import { map } from "jquery";
import { ParsedPropertyType } from "@angular/compiler";
import { PriceReplicaComponent } from "./price-replica/price-replica.component";

@Component({
  selector: "app-price-history",
  // standalone: true,
  // imports: [],
  templateUrl: "./price-history.component.html",
  styleUrl: "./price-history.component.css",
  animations: [appModuleAnimation()],
  encapsulation: ViewEncapsulation.None,
})
export class PriceHistoryComponent extends AppComponentBase implements OnInit {
  saving = false;

  tblPriceList: ItemPriceListDto[] = [];
  selectedLocations: number[] = [];
  tblLocation: SelectItem[] = [];
  tblCategory: SelectItem[] = [];

  primengTableHelper: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  @Output() onSave = new EventEmitter<any>();
  eventClone: LazyLoadEvent;
  categoryId: number;
  itemPriceDate: Date = new Date();

  constructor(
    injector: Injector,
    private _locationService: LocationServiceProxy,
    private _categoryService: ItemCategoryServiceProxy,
    private _modalService: BsModalService,
    private _itemPriceService: ItemPriceServiceProxy,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.getLocationDropdown();
    this.getCategoryDropdown();
    this.itemPriceDate = new Date();
  }

  getLocationDropdown() {
    this.tblLocation = [];
    this._locationService.getLocationDropDown().subscribe((result) => {
      this.tblLocation = result;
      this.cd.detectChanges();
    });
  }

  getCategoryDropdown() {
    this.tblCategory = [];
    this._categoryService
      .getItemCategoryDropdown(0, 0, 1)
      .subscribe((result) => {
        this.tblCategory = result;
        this.cd.detectChanges();
      });
  }

  getCategoryWiseItemPrice(categoryId: number) {
    this._itemPriceService
      .getCategoryWiseItems(categoryId)
      .subscribe((result) => {
        this.tblPriceList = result;
        this.cd.detectChanges();
      })
      .add(() => abp.ui.clearBusy());
  }

  create(): void {
    this.saving = true;
    let checkPrice = this.tblPriceList.filter((value) => value.price > 0);

if(checkPrice.length <= 0){
  this.saving = false;
  this.notify.error("Price should be greater than zero for atleast one item");
  return;
}

    this.tblPriceList.forEach((element) => {
      element.effectedDate = moment(this.itemPriceDate);
      element.strLocationIds = this.selectedLocations;
    });

    this._itemPriceService.bulkCreate(this.tblPriceList).subscribe({
      next: () => {
        this.notify.success("Saved Successfuly");
        this.onSave.emit(true);
        this.saving = false; // Re-enable the button after saving completes
      },
      error: (err) => {
        this.saving = false; // Re-enable the button if there is an error
        this.notify.error("Save failed. Please try again.");
      },
    });
    this.categoryId = undefined;
    this.getCategoryWiseItemPrice(this.categoryId);
    this.selectedLocations = [];
  }

  showPriceListDialog(): void {
    let createPriceListDialog: BsModalRef;
    createPriceListDialog = this._modalService.show(
      AddEditPriceComponent, //change this component
      {
        class: "modal-dialog modal-xl",
        backdrop: "static",
        ignoreBackdropClick: true,
      }
    );
  }

  showPriceReplicaDialog(): void {
    let createPriceListDialog: BsModalRef;
    createPriceListDialog = this._modalService.show(
      PriceReplicaComponent, //change this component
      {
        class: "modal-dialog-centered modal-lg",
        backdrop: "static",
        ignoreBackdropClick: true,
      }
    );
  }
  save(): void {
    if (this.saving) {
      return; // Prevent multiple saves
    }
    this.saving = true; // Disable the button
    this.create();
  }
}
