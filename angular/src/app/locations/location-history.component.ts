import { ChangeDetectorRef, Component, Injector, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AddEditLocComponent } from './create-edit-location/add-edit-loc.component';
import { extend } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import { LocationServiceProxy, LocationHistoryDtoPagedResultDto, 
  LocationHistoryDto,
  LocationDtoPagedResultDto,
  LocationDto,  } from '@shared/service-proxies/service-proxies';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { finalize } from 'rxjs/operators';

class PagedLocationHistoryRequestDto extends PagedRequestDto {
  keyword: string;
}
@Component({
  selector: 'app-location-history',
  // standalone: true,
  // imports: [],
  templateUrl: './location-history.component.html',
  styleUrl: './location-history.component.css',
  animations: [appModuleAnimation()]
})

export class LocationHistoryComponent extends PagedListingComponentBase<LocationHistoryDto> {

  locationHistory: LocationHistoryDto[] = [];
  keyword = '';


  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private _locationService: LocationServiceProxy,
    cd: ChangeDetectorRef
  ) {
    super(injector, cd);
  }

  list(
    request: PagedLocationHistoryRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;

    this._locationService
      .getHistory(request.keyword, undefined, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: LocationHistoryDtoPagedResultDto) => {
        this.locationHistory = result.items;
        this.showPaging(result, pageNumber);
        this.cd.detectChanges();
      });
  }

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

  showCreateOrEditLocDialog(id?: number): void {
    let createOrEditLocDialog: BsModalRef;
    if (!id) {
      createOrEditLocDialog = this._modalService.show(
        AddEditLocComponent,
        {
          class: 'modal-xl',
        }
      );
    }
    else {
      createOrEditLocDialog = this._modalService.show(
        AddEditLocComponent, {
        class: 'modal-xl',
        initialState: {
          id: id,
        },
      }
      );
    }
    // createOrEditLocDialog.content.onSave.subscribe((value) => {
    //   if(value){
    //     this.list();
    //   }
    // });
  }
}