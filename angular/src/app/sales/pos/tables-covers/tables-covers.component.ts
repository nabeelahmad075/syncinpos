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
  tableIdd: number;
  groupedTables: { [key: string]: SelectItemDto[] } = {}; // Grouped tables
  numberOfGuests: number;

  @Output() tableCoverSelected = new EventEmitter<{ tableId: number; covers: number }>();

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

  getTables() {
    abp.ui.setBusy();
    this._tableService
      .getTableDropdown(this.locationId, undefined)
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

  viewTablesCovers(tableId: number) {
    if (!this.numberOfGuests) {
      this.notify.warn("Please! Enter Number Of Guests");
      return;
    }
    this.tableIdd = tableId;
    this.tableCoverSelected.emit({ tableId, covers: this.numberOfGuests });
    this.bsModalRef.hide();
  }
}
