import {
  ChangeDetectorRef,
  Component,
  Injector,
  ViewChild,
} from "@angular/core";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import {
  UserServiceProxy,
  UserHistoryDto,
} from "@shared/service-proxies/service-proxies";
import { CreateUserDialogComponent } from "./create-user/create-user-dialog.component";
import { EditUserDialogComponent } from "./edit-user/edit-user-dialog.component";
import { ResetPasswordDialogComponent } from "./reset-password/reset-password.component";
import { Table } from "primeng/table";
import { PrimengTableHelper } from "@shared/helpers/primengTableHelper";
import { Paginator, PaginatorModule } from "primeng/paginator";
import { LazyLoadEvent, MenuItem } from "primeng/api";
import { AppComponentBase } from "@shared/app-component-base";
import { ButtonModule } from "primeng/button";

// class PagedUsersRequestDto extends PagedRequestDto {
//   keyword: string;
//   isActive: boolean | null;
// }

@Component({
  templateUrl: "./users.component.html",
  animations: [appModuleAnimation()],
})
export class UsersComponent extends AppComponentBase {
  users: UserHistoryDto[] = [];
  keyword = "";
  maxResultCount: number = 5;
  isActive: boolean | null;
  advancedFiltersVisible = false;
  primengTableHelper: PrimengTableHelper = new PrimengTableHelper();
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;
  eventClone: LazyLoadEvent;
  items: MenuItem[];

  constructor(
    injector: Injector,
    private _userService: UserServiceProxy,
    private _modalService: BsModalService,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  // createUser(): void {
  //   this.showCreateOrEditUserDialog();
  // }

  editUser(user: UserHistoryDto): void {
    this.showCreateOrEditUserDialog(user.id);
  }

  resetPassword(user: UserHistoryDto): void {
    this.showResetPasswordUserDialog(user.id);
  }

  // clearFilters(): void {
  //   this.keyword = '';
  //   this.isActive = undefined;
  //   this.getDataPage(1);
  // }

  // protected list(
  //   request: PagedUsersRequestDto,
  //   pageNumber: number,
  //   finishedCallback: Function
  // ): void {
  //   request.keyword = this.keyword;
  //   request.isActive = this.isActive;

  //   this._userService
  //     .getAll(
  //       request.keyword,
  //       request.isActive,
  //       request.skipCount,
  //       request.maxResultCount
  //     )
  //     .pipe(
  //       finalize(() => {
  //         finishedCallback();
  //       })
  //     )
  //     .subscribe((result: UserDtoPagedResultDto) => {
  //       this.users = result.items;
  //       this.showPaging(result, pageNumber);
  //     });
  // }

  // protected delete(user: UserDto): void {
  //   abp.message.confirm(
  //     this.l('UserDeleteWarningMessage', user.fullName),
  //     undefined,
  //     (result: boolean) => {
  //       if (result) {
  //         this._userService.delete(user.id).subscribe(() => {
  //           abp.notify.success(this.l('SuccessfullyDeleted'));
  //           this.refresh();
  //         });
  //       }
  //     }
  //   );
  // }

  showResetPasswordUserDialog(id?: number): void {
    this._modalService.show(ResetPasswordDialogComponent, {
      class: "modal-lg",
      initialState: {
        id: id,
      },
    });
  }

  showCreateOrEditUserDialog(id?: number): void {
    let createOrEditUserDialog: BsModalRef;
    createOrEditUserDialog = this._modalService.show(EditUserDialogComponent, {
      class: "modal-lg",
      initialState: {
        id: id,
      },
    });

    createOrEditUserDialog.content.onSave.subscribe(() => {
      this.getHistory({});
    });
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
    this._userService
      .getUsersHistory(
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

  performAction(user: UserHistoryDto, menu: any, event: MouseEvent) {
    this.items = [
      {
        label: "Edit",
        icon: "fas fa-pencil-alt",
        command: () => this.showCreateOrEditUserDialog(user.id),
      },
      { separator: true },
      {
        label: "Reset Password",
        icon: "fas fa-lock",
        command: () => this.showResetPasswordUserDialog(user.id),
      },
    ];
    menu.toggle(event);
  }
}
