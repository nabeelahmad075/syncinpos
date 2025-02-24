import { Component, Injector, OnInit, EventEmitter, Output, ChangeDetectorRef, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import {PendingOrdersCountDto, PendingOrdersDto, POSServiceProxy} from '@shared/service-proxies/service-proxies';
import { result } from 'lodash-es';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { LazyLoadEvent, SelectItem } from "primeng/api";
import { PrimengTableHelper } from '@shared/helpers/primengTableHelper';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Router } from '@node_modules/@angular/router';

@Component({
  selector: 'app-pending-orders',
  // standalone: true,
  // imports: [],
  templateUrl: './pending-orders.component.html',
  animations: [appModuleAnimation()],
  styleUrl: './pending-orders.component.css'
})
export class PendingOrdersComponent extends AppComponentBase implements OnInit{

  locationId: number;

  @Output() orderSelected = new EventEmitter<number>(); // Emit orderId

  pendingOrdersList: PendingOrdersDto[] = [];
  pendingOrdersCount: PendingOrdersCountDto = new PendingOrdersCountDto();
  primengDesignationTable: PrimengTableHelper = new PrimengTableHelper();
  primengDepartmentTable: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  eventClone: LazyLoadEvent;

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private _pendingOrdersService: POSServiceProxy,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    // this.getPendingOrders();
    this.getPendingOrdersCount();
    this.cdr.detectChanges();
  }

  getPendingOrders(serviceTypeId: number) {
    abp.ui.setBusy();
    this._pendingOrdersService
      .getPendingOrders(serviceTypeId, this.locationId)
      .subscribe((result) => {
        this.pendingOrdersList = result;
        console.log(this.pendingOrdersList);
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
