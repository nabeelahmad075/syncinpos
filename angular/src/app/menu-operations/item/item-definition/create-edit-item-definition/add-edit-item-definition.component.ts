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
  ItemDto,
  ItemServiceProxy,
  ItemTypeDto,
  ItemTypeServiceProxy,
  ItemCategoryDto,
  ItemCategoryServiceProxy,
  SectionDto,
  SectionsServiceProxy,
  UnitOfMeasurementDto,
  UnitOfMeasurementServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { result } from "lodash-es";
import * as moment from "moment";
import { BsModalRef } from "ngx-bootstrap/modal";
import { LazyLoadEvent, SelectItem } from "primeng/api";
import { Dropdown } from "primeng/dropdown";

@Component({
  selector: "app-add-edit-item-definition",
  // standalone: true,
  // imports: [],
  templateUrl: "./add-edit-item-definition.component.html",
  styleUrl: "./add-edit-item-definition.component.css",
  animations: [appModuleAnimation()],
})
export class AddEditItemDefinitionComponent
  extends AppComponentBase
  implements OnInit
{
  saving = false;
  id: number = 0;
  itemTypeIdd: number;
  tblItemTypes: SelectItem[] = [];
  tblItemCategories: SelectItem[] = [];
  tblSections: SelectItem[] = [];
  tblUnitOfMeasurements: SelectItem[] = [];
  tblItemDefinitions: ItemDto = new ItemDto();
  @Output() onSave = new EventEmitter<any>();
  joiningDate: Date = new Date();
  eventClone: LazyLoadEvent;

  constructor(
    injector: Injector,
    private _itemTypeService: ItemTypeServiceProxy,
    private _itemCategoryService: ItemCategoryServiceProxy,
    private _sectionService: SectionsServiceProxy,
    private _uomService: UnitOfMeasurementServiceProxy,
    private _itemDefinitionService: ItemServiceProxy,
    public bsModalRef: BsModalRef,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);
    this.tblItemDefinitions.isActive = true;
  }

  ngOnInit(): void {

    if (this.id > 0) {
      this.getById();
    }

    this.getItemTypeDropdown();
    this.getSectionsDropdown();
    this.getUnitDropdown();
    // this.getItemCategoryDropdown();
  }

  ngAfterViewInit(): void {
    // Call detectChanges to ensure any asynchronous updates are reflected in the view
    this.cdr.detectChanges();

    // Log values for debugging
    console.log("ngAfterViewInit - tblItemDefinitions:", this.tblItemDefinitions);
    console.log("ngAfterViewInit - itemTypeIdd:", this.itemTypeIdd);
  }

  getItemTypeDropdown() {
    this.tblItemTypes = [];
    debugger
    this._itemTypeService.getItemTypeDropdown().subscribe((result) => {
      this.tblItemTypes = result;
      if (this.tblItemTypes && this.tblItemTypes.length && this.id == 0) {
        debugger
        this.itemTypeIdd = this.tblItemTypes[0].value;
      } // Assuming tblItemTypes has 'value' field
      this.cdr.detectChanges();
    });
  }

  getItemCategoryDropdown(editMode: boolean = false) {
    this.tblItemCategories = [];
    debugger;
    this._itemCategoryService
      .getItemCategoryDropdown(
        this.id,
        this.tblItemDefinitions.itemCategoryId,
        this.itemTypeIdd
      )
      .subscribe((result) => {
        this.tblItemCategories = result;
        // if (this.tblItemCategories && this.tblItemCategories.length && editMode == false) {
        //   debugger
        //   this.tblItemDefinitions.itemCategoryId = this.tblItemCategories[0].value;
        // } // Assuming tblItemTypes has 'value' field
        this.cdr.detectChanges();
      });
  }

  getSectionsDropdown() {
    this.tblSections = [];
    this._sectionService
      .getSectionsDropdown(this.id, this.tblItemDefinitions.sectionId)
      .subscribe((result) => {
        this.tblSections = result;
        this.cdr.detectChanges();
      });
  }

  getUnitDropdown() {
    this.tblUnitOfMeasurements = [];
    this._uomService.getUnitDropdown().subscribe((result) => {
      this.tblUnitOfMeasurements = result;
      this.cdr.detectChanges();
    });
  }

  save(): void {
    this.saving = true;
    this.tblItemDefinitions.itemTypeId = this.itemTypeIdd;
    if (this.id) {
      this.update();
    } else this.create();
  }

  update(): void {
    if (!this.tblItemDefinitions.itemTypeId) {
      abp.notify.error("Please Select Item Type.");
    }
    if (!this.tblItemDefinitions.itemCategoryId) {
      abp.notify.error("Please Select Category.");
    }
    if (!this.tblItemDefinitions.sectionId) {
      abp.notify.error("Please Select Section.");
    }
    this._itemDefinitionService.update(this.tblItemDefinitions).subscribe({
      next: (value: any) => {
        this.notify.info("Update Successfuly");
        this.bsModalRef.hide();
        this.onSave.emit(true);
      },
      error: (err) => {
        this.saving = false;
      },
    });
  }

  create(): void {
    if (!this.tblItemDefinitions.itemTypeId) {
      abp.notify.error("Please Select Item Type.");
    }
    if (!this.tblItemDefinitions.itemCategoryId) {
      abp.notify.error("Please Select Category.");
    }
    if (!this.tblItemDefinitions.sectionId) {
      abp.notify.error("Please Select Section.");
    }
    this._itemDefinitionService.create(this.tblItemDefinitions).subscribe({
      next: () => {
        this.notify.info("Saved Successfuly");
        this.bsModalRef.hide();
        this.onSave.emit(true);
      },
      error: (err) => {
        this.saving = false;
      },
    });
  }

  getById() {
    this.itemTypeIdd = undefined;
    this._itemDefinitionService.get(this.id).subscribe((result) => {
      debugger;
      this.tblItemDefinitions = result;

      this.itemTypeIdd = result.itemTypeId;

      this.getItemCategoryDropdown(true);

      this.cdr.detectChanges();
    });
  }
}
