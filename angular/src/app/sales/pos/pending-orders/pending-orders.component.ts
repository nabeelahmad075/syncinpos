import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef,
  ViewChild,
} from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import {
  PendingOrdersCountDto,
  PendingOrdersDto,
  POSServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { BsModalRef } from "ngx-bootstrap/modal";
import { LazyLoadEvent } from "primeng/api";
import { Table } from "primeng/table";
import { appModuleAnimation } from "@shared/animations/routerTransition";

@Component({
  selector: "app-pending-orders",
  // standalone: true,
  // imports: [],
  templateUrl: "./pending-orders.component.html",
  animations: [appModuleAnimation()],
  styleUrl: "./pending-orders.component.css",
})
export class PendingOrdersComponent extends AppComponentBase implements OnInit {
  locationId: number;
  selectedOrderType: number = 1;

  @Output() orderSelected = new EventEmitter<number>(); // Emit orderId

  pendingOrdersList: PendingOrdersDto[] = [];
  pendingOrdersCount: PendingOrdersCountDto = new PendingOrdersCountDto();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  eventClone: LazyLoadEvent;

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private _pendingOrdersService: POSServiceProxy,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    // this.getPendingOrders();
    this.getPendingOrdersCount();
    this.getPendingOrders(this.selectedOrderType);
    this.cdr.detectChanges();
  }

  getPendingOrders(serviceTypeId: number) {
    abp.ui.setBusy();
    this.selectedOrderType = serviceTypeId;
    this._pendingOrdersService
      .getPendingOrders(serviceTypeId, this.locationId)
      .subscribe((result) => {
        this.pendingOrdersList = result;
        this.cdr.detectChanges();
      })
      .add(() => abp.ui.clearBusy());
  }

  getPendingOrdersCount() {
    abp.ui.setBusy();
    this._pendingOrdersService
      .getPendingOrdersCount(this.locationId)
      .subscribe((result) => {
        this.pendingOrdersCount = result;
        this.cdr.detectChanges();
      })
      .add(() => abp.ui.clearBusy());
  }

  viewOrder(orderId: number) {
    this.orderSelected.emit(orderId); // Emit orderId before closing modal
    this.bsModalRef.hide(); // Close modal
  }
}
