import { ChangeDetectorRef, Component, Injector, ViewChild, ViewEncapsulation } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AddEditLocComponent } from './create-edit-location/add-edit-loc.component';
import { extend, sortBy } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import { LocationServiceProxy, LocationHistoryDtoPagedResultDto, 
  LocationHistoryDto,
  LocationDtoPagedResultDto,
  LocationDto,  } from '@shared/service-proxies/service-proxies';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { finalize } from 'rxjs/operators';
import { TableModule, Table } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { PrimengTableHelper } from "@shared/helpers/primengTableHelper";
import { Paginator, PaginatorModule } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';

@Component({
  selector: 'app-location-history',
  // standalone: true,
  // imports: [TableModule],
  templateUrl: './location-history.component.html',
  styleUrl: './location-history.component.css',
  animations: [appModuleAnimation()],
  encapsulation: ViewEncapsulation.Emulated
})

export class LocationHistoryComponent extends AppComponentBase {

  locationHistory: LocationHistoryDto[] = [];
  keyword = '';
maxResultCount: number = 5;
primengTableHelper: PrimengTableHelper = new PrimengTableHelper();
@ViewChild('dataTable', { static: true }) dataTable: Table;
@ViewChild('paginator', { static: true }) paginator: Paginator;
eventClone: LazyLoadEvent;

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private _locationService: LocationServiceProxy,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }
  

  getHistory(event?: LazyLoadEvent) {
    if (this.primengTableHelper.shouldResetPaging(event)) {
      this.paginator.changePage(0);
      return;
    }
    if (this.eventClone && !event.filters)
      event.filters = this.eventClone.filters;
    if (this.eventClone && this.eventClone.sortField && !event.sortField) {
      event.sortField = this.eventClone.sortField
      event.sortOrder = this.eventClone.sortOrder
    }
    abp.ui.setBusy();
    this._locationService
      .getHistory(
        event && event.filters && event.filters["global"] ? event.filters["global"].value : undefined,
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

  // list(
  //   request: PagedLocationHistoryRequestDto,
  //   pageNumber: number,
  //   finishedCallback: Function
  // ): void {
  //   request.keyword = this.keyword;
  //   request.maxResultCount=this.maxResultCount;
  //   request.skipCount=(pageNumber-1)*request.maxResultCount;

  //   this._locationService
  //     .getHistory(request.keyword, undefined, request.skipCount, request.maxResultCount)
  //     .pipe(
  //       finalize(() => {
  //         finishedCallback();
  //       })
  //     )
  //     .subscribe((result: LocationHistoryDtoPagedResultDto) => {
  //       this.locationHistory = result.items;
  //       this.showPaging(result, pageNumber);
  //       this.cd.detectChanges();
  //     });
  // }

  delete(locationHistory: LocationHistoryDto): void {
    // abp.message.confirm(
    //   this.l('RoleDeleteWarningMessage', role.displayName),
    //   undefined,
    //   (result: boolean) => {
    //     if (result) {
    //       this._rolesService
    //         .delete(role.id)
    //         .pipe(
    //           finalize(() => {
    //             abp.notify.success(this.l('SuccessfullyDeleted'));
    //             this.refresh();
    //           })
    //         )
    //         .subscribe(() => {});
    //     }
    //   }
    // );
  }

  createLocation(): void {
    this.showCreateOrEditLocDialog();
  }


  editLocation(locationHistory: LocationHistoryDto): void {
    this.showCreateOrEditLocDialog(locationHistory.id);
  }


  showCreateOrEditLocDialog(id?: number): void {
    let createOrEditLocDialog: BsModalRef;
    if (!id) {
      createOrEditLocDialog = this._modalService.show(
        AddEditLocComponent,
        {
          class: 'modal-lg modal-dialog-centered',
          backdrop: "static",
          ignoreBackdropClick: true
        }
      );
    }
    else {
      createOrEditLocDialog = this._modalService.show(
        AddEditLocComponent, {
        class: 'modal-lg modal-dialog-centered',
        backdrop: "static",
        ignoreBackdropClick: true,
        initialState: {
          id: id,
        },
      }
      );
    }
    
    createOrEditLocDialog.content.onSave.subscribe((value) => {
      if(value){
        this.getHistory({});
      }
    });
  }
}