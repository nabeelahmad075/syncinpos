import { ChangeDetectorRef, Component, Injector, OnInit } from "@angular/core";
import * as moment from "moment";
import { AppComponentBase } from "@shared/app-component-base";
import {
  EmployeeServiceProxy,
  ItemCategoryServiceProxy,
  ItemServiceProxy,
  POSDetailDto,
  POSMasterDto,
  POSServiceProxy,
  SelectItemDto,
} from "@shared/service-proxies/service-proxies";
import { SelectItem } from "@node_modules/primeng/api";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { PendingOrdersComponent } from "../pending-orders/pending-orders.component";
import { Subscription } from "rxjs";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { ItemSearchHistoryComponent } from "../item-search/item-search-history.component";

@Component({
  selector: "app-main-pos",
  // standalone: true,
  // imports: [],
  templateUrl: "./main-pos.component.html",
  animations: [appModuleAnimation()],
  styleUrl: "./main-pos.component.css",
})
export class MainPosComponent extends AppComponentBase implements OnInit {
  locationId: number = 14;
  categoryId: number;
  softwareDate: Date = new Date();
  selectedItem: number;
  orderTakerTypeId: number = 1;
  deliveryManTypeId: number = 2;
  selectedServiceType: string = "Order Taker";
  dineInServiceType: number = 1;
  takeAwayServiceType: number = 2;
  deliveryManServiceType: number = 3;
  cashAmount: number = 0;
  cashTaxAmount: number = 0;
  cardAmount: number = 0;
  cardTaxAmount: number = 0;

  private subscription!: Subscription;

  tblCategory: SelectItemDto[] = [];
  tblItems: SelectItemDto[] = [];
  tblAllItems: SelectItemDto[] = [];
  tblEmployee: SelectItem[] = [];
  tblPosMaster: POSMasterDto = new POSMasterDto();

  constructor(
    injector: Injector,
    private _categoryService: ItemCategoryServiceProxy,
    private _itemService: ItemServiceProxy,
    private _orderTakerService: EmployeeServiceProxy,
    private _posService: POSServiceProxy,
    private _modalService: BsModalService,

    private cd: ChangeDetectorRef
  ) {
    super(injector);
    this.softwareDate = this.appSession.application.openedDay.toDate();
  }

  ngOnInit() {
    this.newOrder();
    this.cd.detectChanges();
  }

  getCategories() {
    this._categoryService
      .getItemCategoryDropdown(0, 0, 1)
      .subscribe((result) => {
        this.tblCategory = result;
        this.cd.detectChanges();
      });
  }
  
  getAllItems() {
    
    this._itemService
      .getItemDropdown(
        this.locationId,
        moment(this.softwareDate)
      )
      .subscribe((result) => {
        this.tblAllItems = result;
        this.selectedItem = undefined;
        this.cd.detectChanges();
      });
  }

  getItemsByCategory() {

    this._itemService
      .getCategoryWiseItemsList(
        this.categoryId,
        this.locationId,
        moment(this.softwareDate),
        undefined
      )
      .subscribe((result) => {
        this.tblItems = result;
        this.selectedItem = undefined;
        this.cd.detectChanges();
      });
  }

  onItemClick(itemId: number) {
    debugger    
    console.log(this.tblAllItems);
    this.selectedItem = itemId;
    this.cd.detectChanges();
    if (!this.tblPosMaster.posDetails) {
      this.tblPosMaster.posDetails = [];
    }

    let itemFoundInGrid = this.tblPosMaster.posDetails.find(
      (element) => element.itemId === itemId
    );
    let itemPrice = this.tblAllItems.find((element) => element.value === itemId)
      .other.price;
    let itemName = this.tblAllItems.find(
      (element) => element.value === itemId
    ).label;

    if (itemFoundInGrid) {
      itemFoundInGrid.qty += 1;
    } else {
      let posDetail = new POSDetailDto();
      posDetail.itemId = itemId;
      posDetail.qty = 1;
      posDetail.price = itemPrice;
      posDetail["itemName"] = itemName;
      this.tblPosMaster.posDetails.push(posDetail);
      itemFoundInGrid = posDetail;
    }

    itemFoundInGrid.amount = itemFoundInGrid.qty * itemFoundInGrid.price;
    this.calculateAmounts();
    this.cd.detectChanges();
  }

  removeDetail(index: number) {
    this.tblPosMaster.posDetails.splice(index, 1);
    this.calculateAmounts();
  }

  getOrderTakersDropdown(serviceTypeId: number) {
    this.tblEmployee = [];

    this.tblPosMaster.serviceTypeId = serviceTypeId;

    let desigTypeId;
    if (
      this.tblPosMaster.serviceTypeId === this.dineInServiceType ||
      this.tblPosMaster.serviceTypeId === this.takeAwayServiceType
    ) {
      desigTypeId = this.orderTakerTypeId;
      this.selectedServiceType = "Order Taker";
    } else if (
      this.tblPosMaster.serviceTypeId === this.deliveryManServiceType
    ) {
      desigTypeId = this.deliveryManTypeId;
      this.selectedServiceType = "Delivery Man";
    }

    this._orderTakerService
      .getEmployeesDropdown(desigTypeId)
      .subscribe((result) => {
        this.tblEmployee = result;
      });

    setTimeout(() => {
      this.cd.detectChanges();
    }, 0);
  }

  newOrder() {
    this.tblPosMaster = new POSMasterDto();
    this.tblPosMaster.invoiceNo = 123;
    this.tblPosMaster.orderNo = 456;
    this.tblPosMaster.invoiceDate = moment(this.softwareDate);
    this.tblPosMaster.locationId = this.locationId;
    this.tblPosMaster.customerId = 5;
    this.tblPosMaster.employeeId = undefined;
    this.tblPosMaster.isInvoiced = false;
    this.tblPosMaster.printDate = moment(new Date());
    this.tblPosMaster.serviceTypeId = this.dineInServiceType;
    this.tblPosMaster.paymentMode = 0;
    this.tblPosMaster.coverTable = 4;
    this.tblPosMaster.tableId = 1;
    this.tblPosMaster.deliveryCharges = 0;
    this.tblPosMaster.deliveryChargesPer = 0;
    this.tblPosMaster.serviceCharges = 0;
    this.tblPosMaster.serviceChargesPer = 0;
    this.tblPosMaster.bankCharges = 0;
    this.tblPosMaster.bankChargesPer = 0;
    this.tblPosMaster.salesTaxAmount = 0;
    this.tblPosMaster.discountAmount = 0;
    this.tblPosMaster.salesTaxPer = 0;
    this.tblPosMaster.discountPer = 0;
    this.tblPosMaster.grossAmount = 0;
    this.tblPosMaster.netAmount = 0;
    this.tblPosMaster.paymentIn = 0;
    this.tblPosMaster.balance = 0;
    this.tblPosMaster.posDetails = [];
    this.getOrderTakersDropdown(this.tblPosMaster.serviceTypeId);
    this.getCategories();
    this.getAllItems();
    this.tblItems = [];
    this.taxCalculation();
    this.cd.detectChanges();
  }

  placeOrder() {
    if (this.tblPosMaster.posDetails.length === 0) {
      this.notify.error("Please! Select item to place order.");
      return;
    }
    if (this.tblPosMaster.employeeId === undefined) {
      this.notify.error("Please! Select " + this.selectedServiceType + ".");
      return;
    }

    if (this.tblPosMaster.id > 0) {
      this._posService.update(this.tblPosMaster).subscribe((result) => {
        if (result) {
          this.newOrder();
          this.notify.success("Order Updated Successfully");
        } else {
          this.notify.error("Failed to place order. Please try again.");
        }
      });
    } else {
      this._posService.create(this.tblPosMaster).subscribe((result) => {
        if (result) {
          this.newOrder();
          this.notify.success("Order Placed Successfully");
        } else {
          this.notify.error("Failed to place order. Please try again.");
        }
      });
    }
  }

  showPendingOrdersDialog(): void {
    let pendingOrdersDialog: BsModalRef;
    pendingOrdersDialog = this._modalService.show(PendingOrdersComponent, {
      class: "modal-lg modal-dialog-centered",
      backdrop: "static",
      ignoreBackdropClick: true,
      initialState: {
        locationId: this.locationId,
      },
    });

    this.subscription = pendingOrdersDialog.content?.orderSelected.subscribe(
      (orderId: number) => {
        this.viewPendingOrder(orderId); // Navigate to order details
      }
    );

    // Cleanup subscription when modal is hidden
    pendingOrdersDialog.onHidden?.subscribe(() => {
      if (this.subscription) {
        this.subscription.unsubscribe();
      }
    });
  }

  viewPendingOrder(orderId: number) {
    this._posService.get(orderId).subscribe((result) => {
      this.tblPosMaster = result;
      this.calculateAmounts();
      this.getOrderTakersDropdown(result.serviceTypeId);
      setTimeout(() => {
        this.cd.detectChanges();
      }, 100);
    });
  }

  addQty(flgAdd: boolean, index: number) {
    if (this.tblPosMaster.posDetails[index].qty === 1 && !flgAdd) {
      return;
    }
    this.tblPosMaster.posDetails[index].qty = flgAdd
      ? this.tblPosMaster.posDetails[index].qty + 1
      : this.tblPosMaster.posDetails[index].qty - 1;
    this.tblPosMaster.posDetails[index].amount =
      this.tblPosMaster.posDetails[index].qty *
      this.tblPosMaster.posDetails[index].price;
    this.calculateAmounts();
  }

  sum(colName: string = "amount"): number {
    let sumvar = this.tblPosMaster.posDetails.reduce(
      (sum, current) => sum + (current[colName] ?? 0),
      0
    );
    return sumvar;
  }

  taxCalculation(): number {
    this.cashTaxAmount = (this.tblPosMaster.grossAmount * 16) / 100;
    this.cashAmount = this.cashTaxAmount + this.tblPosMaster.grossAmount;
    this.cardTaxAmount = (this.tblPosMaster.grossAmount * 5) / 100;
    this.cardAmount = this.cardTaxAmount + this.tblPosMaster.grossAmount;
    // this.tblPosMaster.salesTaxPer = 16;
    this.cd.detectChanges();
    return (
      (this.tblPosMaster.grossAmount * this.tblPosMaster.salesTaxPer) / 100
    );
  }

  calculateAmounts() {
    this.tblPosMaster.grossAmount = this.sum();
    this.tblPosMaster.salesTaxAmount = this.taxCalculation();
    this.tblPosMaster.netAmount =
      this.tblPosMaster.grossAmount + this.tblPosMaster.salesTaxAmount;
    this.cd.detectChanges();
    // this.tblPosMaster.netAmount = this.tblPosMaster.grossAmount - this.tblPosMaster.discountAmount + this.tblPosMaster.salesTaxAmount + this.tblPosMaster.deliveryCharges + this.tblPosMaster.serviceCharges + this.tblPosMaster.bankCharges;
  }

  showItemSearchDialog(): void {
    let itemSearchDialog: BsModalRef;
    itemSearchDialog = this._modalService.show(ItemSearchHistoryComponent, {
      class: "modal-xl modal-dialog-centered",
      backdrop: "static",
      ignoreBackdropClick: true,
      initialState: {
        locationId: this.locationId,
        effectedDate: this.softwareDate,
      },
    });

    
    this.subscription = itemSearchDialog.content?.itemSelected.subscribe(
      (itemId: number) => {
        console.log(itemId);
        // this.getAllItems();
        this.onItemClick(itemId); // Navigate to order details
      }
    );

    // Cleanup subscription when modal is hidden
    itemSearchDialog.onHidden?.subscribe(() => {
      if (this.subscription) {
        this.subscription.unsubscribe();
      }
    });
  }
}
