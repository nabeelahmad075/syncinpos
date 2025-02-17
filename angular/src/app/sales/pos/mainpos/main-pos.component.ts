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

@Component({
  selector: "app-main-pos",
  // standalone: true,
  // imports: [],
  templateUrl: "./main-pos.component.html",
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

  tblCategory: SelectItemDto[] = [];
  tblItems: SelectItemDto[] = [];
  tblEmployee: SelectItem[] = [];
  tblPosMaster: POSMasterDto = new POSMasterDto();

  constructor(
    injector: Injector,
    private _categoryService: ItemCategoryServiceProxy,
    private _itemService: ItemServiceProxy,
    private _orderTakerService: EmployeeServiceProxy,
    private _posService: POSServiceProxy,

    private cd: ChangeDetectorRef
  ) {
    super(injector);
    this.softwareDate = this.appSession.application.openedDay.toDate();
  }

  ngOnInit() {
    this.clearState();
  }

  getCategories() {
    this._categoryService
      .getItemCategoryDropdown(0, 0, 1)
      .subscribe((result) => {
        this.tblCategory = result;
        this.cd.detectChanges();
      });
  }

  getItemsByCategory() {
    this._itemService
      .getCategoryWiseItemsList(
        this.categoryId,
        this.locationId,
        moment(this.softwareDate)
      )
      .subscribe((result) => {
        this.tblItems = result;
        this.selectedItem = undefined;
        this.cd.detectChanges();
      });
  }

  onItemClick(itemId: number) {
    this.selectedItem = itemId;

    if (!this.tblPosMaster.posDetails) {
      this.tblPosMaster.posDetails = [];
    }

    let itemFoundInGrid = this.tblPosMaster.posDetails.find(
      (element) => element.itemId === itemId
    );
    let itemPrice = this.tblItems.find((element) => element.value === itemId)
      .other.price;
    let itemName = this.tblItems.find(
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
  }

  removeDetail(index: number) {
    this.tblPosMaster.posDetails.splice(index, 1);
  }

  getOrderTakersDropdown(desigTypeId: number, serviceTypeId: number) {
    this.tblEmployee = [];
    this._orderTakerService
      .getEmployeesDropdown(desigTypeId)
      .subscribe((result) => {
        this.tblEmployee = result;
      });
    this.tblPosMaster.serviceTypeId = serviceTypeId;
    if (desigTypeId === this.orderTakerTypeId) {
      this.selectedServiceType = "Order Taker";
    } else if (desigTypeId === this.deliveryManTypeId) {
      this.selectedServiceType = "Delivery Man";
    }
  }

  clearState() {
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
    this.getOrderTakersDropdown(
      this.orderTakerTypeId,
      this.tblPosMaster.serviceTypeId
    );
    this.getCategories();
    this.tblItems = [];
    this.cd.detectChanges();
  }
}
