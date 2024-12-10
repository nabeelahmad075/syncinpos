import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Injector,
  OnInit,
  Output,
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
import moment from "moment";

@Component({
  selector: 'app-price-replica',
  // standalone: true,
  // imports: [],
  templateUrl: './price-replica.component.html',
  styleUrl: './price-replica.component.css',
  animations: [appModuleAnimation()]
})

export class PriceReplicaComponent extends AppComponentBase implements OnInit {

  itemPriceHistory: ItemPriceListHistoryDto[] = [];
  tblLocation: SelectItem[] = [];
  keyword = "";
  selectedLocations: number[] = [];
  tblCategory: SelectItem[] = [];
  selectedCategories: number[] = [];
  fromLocationId: number;
  eventClone: LazyLoadEvent;
  itemPriceDate: Date = new Date();
  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _locationService: LocationServiceProxy,
    private _categoryService: ItemCategoryServiceProxy,
    private _itemPriceService: ItemPriceServiceProxy,
    private _modalService: BsModalService,
    public bsModalRef: BsModalRef,
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

  create(): void {

    this._itemPriceService.getItemPriceListForReplica(this.fromLocationId, this.selectedLocations, 
      this.selectedCategories, moment(this.itemPriceDate)).subscribe({
      next: () => {
        this.notify.success("Saved Successfuly");
        this.onSave.emit(true);
        this.bsModalRef.hide();
      },
      error: (err) => {
        this.notify.error("No Price List Found!");
      },
    });
  }
}
