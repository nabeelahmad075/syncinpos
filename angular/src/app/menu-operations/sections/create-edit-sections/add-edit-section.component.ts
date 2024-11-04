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
  SectionDto,
  SectionsServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { result } from "lodash-es";
import * as moment from "moment";
import { BsModalRef } from "ngx-bootstrap/modal";
import { LazyLoadEvent, SelectItem } from "primeng/api";

@Component({
  selector: "app-add-edit-section",
  // standalone: true,
  // imports: [],
  templateUrl: "./add-edit-section.component.html",
  styleUrl: "./add-edit-section.component.css",
  animations: [appModuleAnimation()],
})
export class AddEditSectionComponent
  extends AppComponentBase
  implements OnInit
{
  saving = false;
  id: number;
  tblSections: SectionDto = new SectionDto();
  @Output() onSave = new EventEmitter<any>();
  eventClone: LazyLoadEvent;

  constructor(
    injector: Injector,
    private _sectionService: SectionsServiceProxy,
    public bsModalRef: BsModalRef,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);
    this.tblSections.isActive = true;
  }

  ngOnInit(): void {
    if (this.id > 0) {
      this.getById();
    }
  }

  save(): void {
    this.saving = true;
    if (this.id) {
      this.update();
    } else this.create();
  }

  getById() {
    this._sectionService.get(this.id).subscribe((result) => {
      this.tblSections = result;
      this.cdr.detectChanges();
    });
  }

  create(): void {
    if (!this.tblSections.title) {
      abp.notify.error("Please Enter Section Name.");
    }
    this._sectionService.create(this.tblSections).subscribe({
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

  update(): void {
    if (!this.tblSections.title) {
      abp.notify.error("Please Enter Section Name.");
    }
    this._sectionService.update(this.tblSections).subscribe({
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
}
