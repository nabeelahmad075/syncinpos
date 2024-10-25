import { Component, Injector, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AddEditLocComponent } from './create-edit-location/add-edit-loc.component';
import { extend } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import { LocationServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-location-history',
  // standalone: true,
  // imports: [],
  templateUrl: './location-history.component.html',
  styleUrl: './location-history.component.css',
  animations: [appModuleAnimation()]
})

export class LocationHistoryComponent extends AppComponentBase {

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private _locationService: LocationServiceProxy,
  ) {
    super(injector);
  }

  // getHistory(event?: LazyLoadEvent) {
  //   if (this.primengTableHelper.shouldResetPaging(event)) {
  //     this.paginator.changePage(0);
  //     return;
  //   }
  //   if (this.eventClone && !event.filters)
  //     event.filters = this.eventClone.filters;
  //   if (this.eventClone && this.eventClone.sortField && !event.sortField) {
  //     event.sortField = this.eventClone.sortField
  //     event.sortOrder = this.eventClone.sortOrder
  //   }
  //   abp.ui.setBusy();
  //   this._detailAccountService
  //     .getHistory(
  //       event && event.filters && event.filters["global"] ? event.filters["global"].value : undefined,
  //       "",
  //       this.primengTableHelper.getSkipCount(this.paginator, event),
  //       this.primengTableHelper.getMaxResultCount(this.paginator, event)
  //     )
  //     .subscribe((result) => {
  //       this.primengTableHelper.records = result.items;
  //       this.primengTableHelper.totalRecordsCount = result.totalCount;
  //     })
  //     .add(() => abp.ui.clearBusy());
  // }

  createLocation(): void {
    this.showCreateOrEditLocDialog();
  }

  showCreateOrEditLocDialog(id?: number): void {
    let createOrEditLocDialog: BsModalRef;
    if (!id) {
      createOrEditLocDialog = this._modalService.show(
        AddEditLocComponent,
        {
          class: 'modal-lg',
        }
      );
    }
    else {
      createOrEditLocDialog = this._modalService.show(
        AddEditLocComponent, {
        class: 'modal-lg',
        initialState: {
          id: id,
        },
      }
      );
    }
  }
}