import { ChangeDetectorRef, Component, Injector, OnInit } from "@angular/core";
import * as moment from "moment";
import { AppComponentBase } from "@shared/app-component-base";
import {
  ItemCategoryServiceProxy,
  ItemServiceProxy,
  SelectItemDto,
} from "@shared/service-proxies/service-proxies";

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

  tblCategory: SelectItemDto[] = [];
  tblItems: SelectItemDto[] = [];

  constructor(
    injector: Injector,
    private _categoryService: ItemCategoryServiceProxy,
    private _itemService: ItemServiceProxy,

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

  onItemClick(item: number) {
    this.selectedItem = item;
    // console.log("Selected Item:", this.selectedItem);
    // this.cd.detectChanges();
  }
}
