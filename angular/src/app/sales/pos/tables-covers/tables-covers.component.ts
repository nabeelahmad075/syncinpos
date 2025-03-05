import {
  Component,
  Injector,
  ChangeDetectorRef,
  OnInit,
  EventEmitter,
  Output,
} from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import {
  SelectItemDto,
  TableEntityServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { BsModalRef } from "ngx-bootstrap/modal";
import { appModuleAnimation } from "@shared/animations/routerTransition";

@Component({
  selector: "app-tables-covers",
  templateUrl: "./tables-covers.component.html",
  styleUrl: "./tables-covers.component.css",
  animations: [appModuleAnimation()],
})
export class TablesCoversComponent extends AppComponentBase implements OnInit {
  
  locationId: number;
  groupedTables: { [key: string]: SelectItemDto[] } = {}; // Grouped tables

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
  }

  getTables(floorId: number = undefined) {
    abp.ui.setBusy();
    this._tableService
      .getTableDropdown(this.locationId, floorId)
      .subscribe((result) => {
        this.tablesList = result;
        this.groupTablesByFloor(); // Group tables after fetching
        this.cdr.detectChanges();
      })
      .add(() => abp.ui.clearBusy());
  }

  groupTablesByFloor() {
    this.groupedTables = this.tablesList.reduce((acc, table) => {
      const floorTitle = table.other.floorTitle;
      if (!acc[floorTitle]) {
        acc[floorTitle] = [];
      }
      acc[floorTitle].push(table);
      return acc;
    }, {} as { [key: string]: SelectItemDto[] });
  }

  viewTablesCovers(tableId: number, coversId: number) {
    this.tableId.emit(tableId);
    this.covers.emit(coversId);
  }
}
