import {
  Component,
  Injector,
  ChangeDetectorRef,
  ViewChild,
  OnInit,
  EventEmitter,
  Output,
} from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import {
  ItemServiceProxy,
  SearchItemDto,
} from "@shared/service-proxies/service-proxies";
import { BsModalRef } from "ngx-bootstrap/modal";
import { LazyLoadEvent } from "primeng/api";
import { PrimengTableHelper } from "@shared/helpers/primengTableHelper";
import { Table } from "primeng/table";
import { Paginator } from "primeng/paginator";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import moment from "moment";

@Component({
  selector: "app-item-search-history",
  // standalone: true,
  // imports: [],
  templateUrl: "./item-search-history.component.html",
  styleUrl: "./item-search-history.component.css",
  animations: [appModuleAnimation()],
})
export class ItemSearchHistoryComponent extends AppComponentBase implements OnInit{

  itemHistory: SearchItemDto[] = [];
  keyword = "";
  maxResultCount: number = 5;
  primengTableHelper: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  eventClone: LazyLoadEvent;

  locationId: number;
  effectedDate: Date;

    @Output() itemSelected = new EventEmitter<number>(); // Emit itemId

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private _itemSearchService: ItemServiceProxy,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.getHistory();
    this.cd.detectChanges(); 
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
    this._itemSearchService
      .getSearchedItem(
        // event && event.filters && event.filters["global"]
        //   ? event.filters["global"].value
        //   : undefined,
        // "",
        // this.primengTableHelper.getSkipCount(this.paginator, event),
        // this.primengTableHelper.getMaxResultCount(this.paginator, event)
        event && event.filters && event.filters["section"] ? event.filters["section"].value : undefined,
        event && event.filters && event.filters["category"] ? event.filters["category"].value : undefined,
        event && event.filters && event.filters["itemName"] ? event.filters["itemName"].value : undefined,
        event && event.filters && event.filters["barcode"] ? event.filters["barcode"].value : undefined,
        event && event.filters && event.filters["uOM"] ? event.filters["uOM"].value : undefined,
        event && event.filters && event.filters["price"] ? event.filters["price"].value : undefined,
        this.locationId,
        moment(this.effectedDate),
        
        event && event.sortField && event.sortOrder ? event.sortField + (event.sortOrder == 1 ? " asc" : " desc") : "id desc",
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

  loadItem(itemId: number) {
    this.itemSelected.emit(itemId); // Emit orderId before closing modal
    this.bsModalRef.hide(); // Close modal
  }
}
