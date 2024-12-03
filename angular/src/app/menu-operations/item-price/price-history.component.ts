import {
  ChangeDetectorRef,
  Component,
  Injector,
  OnInit,
  ViewChild,
  ViewEncapsulation,
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { AddEditPriceComponent } from "./create-edit-price/add-edit-price.component";
import { extend, sortBy } from "lodash-es";
import { AppComponentBase } from "@shared/app-component-base";
import { ItemCategoryServiceProxy, ItemPriceListDto, ItemPriceServiceProxy,
LocationServiceProxy
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

@Component({
  selector: "app-price-history",
  // standalone: true,
  // imports: [],
  templateUrl: "./price-history.component.html",
  styleUrl: "./price-history.component.css",
  animations: [appModuleAnimation()]
})
export class PriceHistoryComponent extends AppComponentBase implements OnInit {

  tblPriceList: ItemPriceListDto[] = [];
  selectedLocations: number[] = [];
  tblLocation: SelectItem[] = [];
  tblCategory: SelectItem[] = [];

  primengTableHelper: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  eventClone: LazyLoadEvent;
  categoryId: number;

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
    // if (this.id > 0) {
    //   this.getById();
    // }
    this.getLocationDropdown();
    this.getCategoryDropdown();
    // this.itemPriceDate = new Date();
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
    this._categoryService.getItemCategoryDropdown(0,0,1).subscribe((result) => {
      this.tblCategory = result;
      this.cd.detectChanges();
    });
  }

  getCategoryWiseItemPrice(categoryId: number){

      this._itemPriceService
        .getCategoryWiseItems(categoryId)
        .subscribe((result) => {
          this.tblPriceList = result;

          this.cd.detectChanges();
        })
        .add(() => abp.ui.clearBusy());
    };

  //getHistory(event?: LazyLoadEvent) {
    // if (this.primengTableHelper.shouldResetPaging(event)) {
    //   this.paginator.changePage(0);
    //   return;
    // }
    // if (this.eventClone && !event.filters)
    //   event.filters = this.eventClone.filters;
    // if (this.eventClone && this.eventClone.sortField && !event.sortField) {
    //   event.sortField = this.eventClone.sortField;
    //   event.sortOrder = this.eventClone.sortOrder;
    // }
    // abp.ui.setBusy();
    // this._itemPriceService
    //   .getItemPriceHistory(
    //     event && event.filters && event.filters["global"]
    //       ? event.filters["global"].value
    //       : undefined,
    //     "",
    //     this.primengTableHelper.getSkipCount(this.paginator, event),
    //     this.primengTableHelper.getMaxResultCount(this.paginator, event)
    //   )
    //   .subscribe((result) => {
    //     this.primengTableHelper.records = result.items;
    //     this.primengTableHelper.totalRecordsCount = result.totalCount;
    //     this.cd.detectChanges();
    //   })
    //   .add(() => abp.ui.clearBusy());
  //}

  showCreateOrEditDialog(id?: number): void {
    let createOrEditDialog: BsModalRef;
    if (!id) {
      createOrEditDialog = this._modalService.show(AddEditPriceComponent, {
        class: "modal-lg modal-dialog-centered",
        backdrop: "static",
        ignoreBackdropClick: true,
      });
    } else {
      createOrEditDialog = this._modalService.show(AddEditPriceComponent, {
        class: "modal-lg modal-dialog-centered",
        backdrop: "static",
        ignoreBackdropClick: true,
        initialState: {
          id: id,
        },
      });
    }
    // createOrEditDialog.content.onSave.subscribe((value) => {
    //   if (value) {
    //     this.getHistory({});
    //   }
    // });
  }

  create(): void {
    this.showCreateOrEditDialog();
  }

  // edit(itemPriceHistory: ItemPriceHistoryDto): void {
  //   this.showCreateOrEditDialog(itemPriceHistory.id);
  // }

  showPriceListDialog(): void {
    let createPriceListDialog: BsModalRef;
    createPriceListDialog = this._modalService.show(
      AddEditPriceComponent,                            //change this component
      {
        class: "modal-lg modal-dialog-centered",
        backdrop: "static",
        ignoreBackdropClick: true,
      }
    );
    // createPriceListDialog.content.onSave.subscribe((value) => {
    //   if (value) {
    //     this.getHistory({});
    //   }
    // });
  }
}
