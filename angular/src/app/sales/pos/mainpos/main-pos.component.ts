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
  locationId: number = 13;
  categoryId: number;
  softwareDate: Date = new Date();
  selectedItem: number;
  desigTypeId: number;
  selectedServiceType: string = 'Order Taker';

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
    this.getCategories();
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

  getItemsByCategory() {
    this._itemService
      .getCategoryWiseItemsList(
        this.categoryId,
        this.locationId,
        moment(this.softwareDate)
      )
      .subscribe((result) => {
        this.tblItems = result;
        this.selectedItem = undefined
        this.cd.detectChanges();
      });
  }

  onItemClick(itemId: number) {
    this.selectedItem = itemId;

    if (!this.tblPosMaster.posDetails) {
      this.tblPosMaster.posDetails = [];
    }

    let itemFoundInGrid = this.tblPosMaster.posDetails.find(element => element.itemId === itemId);
    let itemPrice = this.tblItems.find(element => element.value === itemId).other.price;
    let itemName = this.tblItems.find(element => element.value === itemId).label;

    if (itemFoundInGrid) {
      itemFoundInGrid.qty += 1;
    } else {
      let posDetail = new POSDetailDto();
      posDetail.itemId = itemId;
      posDetail.qty = 1;
      posDetail.price = itemPrice;
      posDetail['itemName'] = itemName;
      this.tblPosMaster.posDetails.push(posDetail);
      itemFoundInGrid = posDetail;
    }
  
    itemFoundInGrid.amount = itemFoundInGrid.qty * itemFoundInGrid.price;


    console.log(this.tblPosMaster);

  }

  removeDetail(index: number) {
    this.tblPosMaster.posDetails.splice(index, 1);
  }

  getOrderTakersDropdown(desigTypeId: number) {
    this.tblEmployee = [];
    this._orderTakerService.getEmployeesDropdown(desigTypeId).subscribe((result) => {
      this.tblEmployee = result;
    });
    if (desigTypeId === 1) {
      this.selectedServiceType = 'Order Taker';  
    } else if (desigTypeId === 2) {
      this.selectedServiceType = 'Delivery Man';  
    }
  }
}
