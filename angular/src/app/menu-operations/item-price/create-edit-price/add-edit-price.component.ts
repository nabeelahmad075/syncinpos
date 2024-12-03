import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef,
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/app-component-base";
import {
  LocationServiceProxy,
  LocationDto,
  ItemCategoryServiceProxy,
  ItemPriceServiceProxy
} from "@shared/service-proxies/service-proxies";
import { result } from "lodash-es";
import * as moment from "moment";
import { BsModalRef } from "ngx-bootstrap/modal";
import { LazyLoadEvent, SelectItem } from "primeng/api";
import { Dropdown } from "primeng/dropdown";

@Component({
  selector: 'app-add-edit-price',
  // standalone: true,
  // imports: [],
  templateUrl: './add-edit-price.component.html',
  styleUrl: './add-edit-price.component.css',
  animations: [appModuleAnimation()]
})

export class AddEditPriceComponent extends AppComponentBase implements OnInit {

  saving = false;
  id: number;
  priceListNo: number;
  selectedAreas: number[] = [];
  tblLocation: SelectItem[] = [];
  tblCategory: SelectItem[] = [];
  // tblItemPriceMaster: ItemPriceMasterDto = new ItemPriceMasterDto();
  // tblItemPriceDetail: ItemPriceDetailDto = new ItemPriceDetailDto();
  @Output() onSave = new EventEmitter<any>();
  itemPriceDate: Date = new Date();
  eventClone: LazyLoadEvent;
  locationId: number;
  categoryId: number;

  constructor(
    injector: Injector,
    private _categoryService: ItemCategoryServiceProxy,
    private _locationService: LocationServiceProxy,
    private _itemPriceService: ItemPriceServiceProxy,
    public bsModalRef: BsModalRef,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    if (this.id > 0) {
      this.getById();
    }
    this.getLocationDropdown();
    this.getCategoryDropdown();
    this.itemPriceDate = new Date();
  }

  getLocationDropdown() {
    this.tblLocation = [];
    this._locationService.getLocationDropDown().subscribe((result) => {
      this.tblLocation = result;
      this.cdr.detectChanges();
    });
  }

  getCategoryDropdown() {
    this.tblCategory = [];
    this._categoryService.getItemCategoryDropdown(0,0,1).subscribe((result) => {
      this.tblCategory = result;
      this.cdr.detectChanges();
    });
  }

  save(): void {
    this.saving = true;
    // this.tblItemPriceMaster.itemPriceNo = this.priceListNo;
    // this.tblItemPriceDetail.locationId = this.locationId;
    // this.tblItemPriceDetail.itemCategoryId = this.categoryId;
    // this.tblItemPriceMaster.itemPriceDate = moment(this.itemPriceDate);
    if (this.id) {
      this.update();
    } else this.create();
  }

  update(): void {
    // if (!this.tblItemPriceDetail.locationId) {
    //   abp.notify.error("Please Select Location.");
    // }
    this._itemPriceService.update(undefined).subscribe({
      next: (value: any) => {
        this.notify.success("Update Successfuly");
        this.bsModalRef.hide();
        this.onSave.emit(true);
      },
      error: (err) => {
        this.saving = false;
      },
    });
  }

  create(): void {
    // if (!this.tblItemPriceDetail.locationId) {
    //   abp.notify.error("Please Select Location.");
    // }
    this._itemPriceService.create(undefined).subscribe({
      next: () => {
        this.notify.success("Saved Successfuly");
        this.bsModalRef.hide();
        this.onSave.emit(true);
      },
      error: (err) => {
        this.saving = false;
      },
    });

  }

  getById() {
    // this._itemPriceService.get(this.id).subscribe((result) => {
    //   this.priceListNo = result.itemPriceNo;
    //   this.tblItemPriceMaster = result;
    //   this.itemPriceDate = this.tblItemPriceMaster.itemPriceDate.toDate();
    //   this.cdr.detectChanges();
    // });
  }

  getNewDocNo() {
    // this._itemPriceService.getNewDocNo().subscribe((result) => {
    //   this.priceListNo = result;
    //   this.cdr.detectChanges();
    // });
  }

  edit(){

  }
}
