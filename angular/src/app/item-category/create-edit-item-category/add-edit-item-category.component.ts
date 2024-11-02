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
  ItemTypeDto, ItemTypeServiceProxy,
  ItemCategoryDto, ItemCategoryServiceProxy
} from "@shared/service-proxies/service-proxies";
import { result } from "lodash-es";
import * as moment from "moment";
import { BsModalRef } from "ngx-bootstrap/modal";
import { LazyLoadEvent, SelectItem } from "primeng/api";
import { Dropdown } from "primeng/dropdown";

@Component({
  selector: 'app-add-edit-item-category',
  // standalone: true,
  // imports: [],
  templateUrl: './add-edit-item-category.component.html',
  styleUrl: './add-edit-item-category.component.css',
  animations: [appModuleAnimation()]
})
export class AddEditItemCategoryComponent extends AppComponentBase implements OnInit {

  saving = false;
  id: number;
  tblItemTypes: SelectItem[] = [];
  tblItemCategories: ItemCategoryDto = new ItemCategoryDto();
  @Output() onSave = new EventEmitter<any>();
  joiningDate: Date = new Date();
  eventClone: LazyLoadEvent;

  constructor(
    injector: Injector,
    private _itemTypeService: ItemTypeServiceProxy,
    private _itemCategoryService: ItemCategoryServiceProxy,
    public bsModalRef: BsModalRef,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);
    this.tblItemCategories.isActive = true;
  }

  ngOnInit(): void {
    if (this.id > 0){
      this.getById();
    }
    this.getItemTypeDropdown();
  }

  getItemTypeDropdown() {
    this.tblItemTypes = [];
    this._itemTypeService.getItemTypeDropdown().subscribe(
      (result) => {
        this.tblItemTypes = result;
        this.cdr.detectChanges();
      }
    );
  }

  
  getById (){
    this._itemCategoryService.get (this.id).subscribe((result) => {
      this.tblItemCategories = result;
      this.cdr.detectChanges();
    })
  }

  save(): void {
    this.saving = true;
    if (this.id) {
      this.update();
    } else
      this.create();
  }

  create(): void {
    if (!this.tblItemCategories.itemTypeId) {
      abp.notify.error("Please Select Item Type.")
    }
    if (!this.tblItemCategories.title) {
      abp.notify.error("Please Enter Category Name.")
    }
    this._itemCategoryService.create(this.tblItemCategories).subscribe({
      next: () => {
        this.notify.info("Saved Successfuly");
        this.bsModalRef.hide();
        this.onSave.emit(true);
      },
      error: (err) => {
        this.saving = false;
      }
    })
  } 

  update(): void {
    if (!this.tblItemCategories.itemTypeId) {
      abp.notify.error("Please Select Item Type.")
    }
    if (!this.tblItemCategories.title) {
      abp.notify.error("Please Enter Category Name.")
    }
    this._itemCategoryService.update(this.tblItemCategories).subscribe({
      next: (value:any) => {
        this.notify.info("Update Successfuly");
        this.bsModalRef.hide();
        this.onSave.emit(true);
      },
      error: (err) => {
        this.saving = false;
      }
    })
  }


}
