import {
  Component,
  Injector,
  ChangeDetectorRef,
  ViewChild,
  OnInit,
  EventEmitter,
  Output,
} from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import {
  SelectItemDto,
  TableEntityDto,
  TableEntityServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { BsModalRef } from "ngx-bootstrap/modal";
import { LazyLoadEvent } from "primeng/api";
import { PrimengTableHelper } from "@shared/helpers/primengTableHelper";
import { Table } from "primeng/table";
import { Paginator } from "primeng/paginator";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import moment from "moment";

@Component({
  selector: "app-tables-covers",
  // standalone: true,
  // imports: [],
  templateUrl: "./tables-covers.component.html",
  styleUrl: "./tables-covers.component.css",
  animations: [appModuleAnimation()],
})
export class TablesCoversComponent extends AppComponentBase implements OnInit {
  
  locationId: number;

  @Output() tableId = new EventEmitter<number>();
  @Output() covers = new EventEmitter<number>();

  tablesList: SelectItemDto[] = [];

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private _tableService: TableEntityServiceProxy,
    private cdr: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.getTables();
    this.cdr.detectChanges();
  }

  getTables(floorId: number = undefined) {
    abp.ui.setBusy();
    this._tableService
      .getTableDropdown(this.locationId, floorId)
      .subscribe((result) => {
        this.tablesList = result;
        this.cdr.detectChanges();
      })
      .add(() => abp.ui.clearBusy());
  }

  getFloorTitle (tableId: number): string {
    let floorTitle = this.tablesList.find((element) => element.value === tableId).other.floorTitle;
    return floorTitle;
  }

  viewTablesCovers(tableId: number, coversId: number) {
    this.tableId.emit(tableId);
    this.covers.emit(coversId);
  }
}
