import {
  ChangeDetectorRef,
  Component,
  Injector,
  OnInit,
  ViewChild,
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { extend, sortBy } from "lodash-es";
import { AppComponentBase } from "@shared/app-component-base";
import {
  ItemCategoryServiceProxy,
  ItemPriceListHistoryDto,
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

@Component({
  selector: "app-add-edit-price",
  // standalone: true,
  // imports: [],
  templateUrl: "./add-edit-price.component.html",
  styleUrl: "./add-edit-price.component.css",
  animations: [appModuleAnimation()],
})
export class AddEditPriceComponent extends AppComponentBase implements OnInit {
  itemPriceHistory: ItemPriceListHistoryDto[] = [];
  tblLocation: SelectItem[] = [];
  keyword = "";
  selectedLocations: number[] = [];
  tblCategory: SelectItem[] = [];
  categoryId: number;
  maxResultCount: number = 25;
  primengTableHelper: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  eventClone: LazyLoadEvent;

  constructor(
    injector: Injector,
    private _locationService: LocationServiceProxy,
    private _categoryService: ItemCategoryServiceProxy,
    private _modalService: BsModalService,
    private _itemPriceService: ItemPriceServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.getLocationDropdown();
    this.getCategoryDropdown();
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

  getHistory(event?: LazyLoadEvent) {
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
    this._itemPriceService
      .getItemPriceHistory(
        event && event.filters && event.filters["global"]
          ? event.filters["global"].value
          : undefined,
        this.selectedLocations, //location ID string
        this.categoryId, //category ID
        "",
        this.primengTableHelper.getSkipCount(this.paginator, event),
        this.primengTableHelper.getMaxResultCount(this.paginator, event)
      )
      .subscribe((result) => {
        this.primengTableHelper.records = result.items;
        this.primengTableHelper.totalRecordsCount = result.totalCount;
        this.cd.detectChanges();
      })
      .add(() => abp.ui.clearBusy());
  }
}
