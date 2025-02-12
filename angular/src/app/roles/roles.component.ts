import {
  Component,
  Injector,
  ChangeDetectorRef,
  ViewChild,
} from "@angular/core";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import {
  RoleServiceProxy,
  RolesHistoryDto,
} from "@shared/service-proxies/service-proxies";
import { CreateRoleDialogComponent } from "./create-role/create-role-dialog.component";
import { EditRoleDialogComponent } from "./edit-role/edit-role-dialog.component";
import { DialogService } from "primeng/dynamicdialog";
import { AppComponentBase } from "@shared/app-component-base";
import { LazyLoadEvent, MenuItem } from "primeng/api";
import { PrimengTableHelper } from "@shared/helpers/primengTableHelper";
import { Table } from "primeng/table";
import { Paginator } from "primeng/paginator";
import { ButtonModule } from "primeng/button";
import { finalize } from "rxjs";

@Component({
  templateUrl: "./roles.component.html",
  providers: [DialogService],
  animations: [appModuleAnimation()],
})
export class RolesComponent extends AppComponentBase{
  roles: RolesHistoryDto[] = [];
  keyword = "";
  primengTableHelper: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  eventClone: LazyLoadEvent;
  items: MenuItem[];

  constructor(
    injector: Injector,
    private _rolesService: RoleServiceProxy,
    private _modalService: BsModalService,
    public dialogService: DialogService,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  // list(
  //   request: PagedRolesRequestDto,
  //   pageNumber: number,
  //   finishedCallback: Function
  // ): void {
  //   request.keyword = this.keyword;

  //   this._rolesService
  //     .getAll(request.keyword, request.skipCount, request.maxResultCount)
  //     .pipe(
  //       finalize(() => {
  //         finishedCallback();
  //       })
  //     )
  //     .subscribe((result: RoleDtoPagedResultDto) => {
  //       this.roles = result.items;
  //       this.showPaging(result, pageNumber);
  //       this.cd.detectChanges();
  //     });
  // }

  delete(role: RolesHistoryDto): void {
    abp.message.confirm(
      this.l('RoleDeleteWarningMessage', role.displayName),
      undefined,
      (result: boolean) => {
        if (result) {
          this._rolesService
            .delete(role.id)
            .pipe(
              finalize(() => {
                abp.notify.info(this.l('Successfully Deleted'));
                this.getHistory({});
              })
            )
            .subscribe(() => {});
        }
      }
    );
  }

  createRole(): void {
    this.showCreateOrEditRoleDialog();
  }

  editRole(role: RolesHistoryDto): void {
    this.showCreateOrEditRoleDialog(role.id);
  }

  showCreateOrEditRoleDialog(id?: number): void {
    let createOrEditRoleDialog: BsModalRef;
    if (!id) {
      createOrEditRoleDialog = this._modalService.show(
        EditRoleDialogComponent,
        {
          class: "modal-lg modal-dialog-centered",
          backdrop: "static",
          ignoreBackdropClick: true,
        }
      );
    } else {
      createOrEditRoleDialog = this._modalService.show(
        EditRoleDialogComponent,
        {
          class: "modal-lg modal-dialog-centered",
          backdrop: "static",
          ignoreBackdropClick: true,
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditRoleDialog.content.onSave.subscribe(() => {
      this.getHistory({});
    });

    // this.dialogService
    // .open(EditRoleDialogComponent, {
    //   header: `${id > 0 ? "Update" : "Create"} Role`,
    //   width: "60%",
    //   data: {
    //     id: id,
    //   },
    // })
    // .onClose.subscribe((result) => {
    //   if (result)
    //     this.refresh();
    // });
  }

  //my code

  getHistory(event?: LazyLoadEvent) {
    if (this.primengTableHelper.shouldResetPaging(event)) {
      this.paginator.changePage(0);
      return;
    }
    if (this.eventClone && !event.filters)
      event.filters = this.eventClone.filters;
    if (this.eventClone && this.eventClone.sortField && !event.sortField) {
      event.sortField = this.eventClone.sortField;
      event.sortOrder = this.eventClone.sortOrder;
    }
    abp.ui.setBusy();
    this._rolesService
      .getRolesHistory(
        event && event.filters && event.filters["global"]
          ? event.filters["global"].value
          : undefined,
        "",
        this.primengTableHelper.getSkipCount(this.paginator, event),
        this.primengTableHelper.getMaxResultCount(this.paginator, event)
      )
      .subscribe((result) => {
        this.primengTableHelper.records = result.items;
        this.primengTableHelper.totalRecordsCount = result.totalCount;
        this.cd.detectChanges();
      })
      .add(() => abp.ui.clearBusy());
  }

  performAction(roles: RolesHistoryDto, menu: any, event: MouseEvent) {
    this.items = [
      {
        label: "Edit",
        icon: "fas fa-pencil-alt",
        command: () => this.editRole(roles),
      },
      { separator: true },
      {
        label: "Delete",
        icon: "fas fa-trash",
        iconClass: "text-danger",
        command: () => this.delete(roles),
      },
    ];
    menu.toggle(event);
  }
}
