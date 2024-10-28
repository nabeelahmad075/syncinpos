import { ChangeDetectorRef, Component, Injector, ViewChild } from '@angular/core';
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
import { TableModule } from 'primeng/table';

class PagedLocationHistoryRequestDto extends PagedRequestDto {
  keyword: string;
}
@Component({
  selector: 'app-location-history',
  // standalone: true,
  // imports: [TableModule],
  templateUrl: './location-history.component.html',
  styleUrl: './location-history.component.css',
  animations: [appModuleAnimation()]
})

export class LocationHistoryComponent extends PagedListingComponentBase<LocationHistoryDto> {

  locationHistory: LocationHistoryDto[] = [];
  keyword = '';
maxResultCount: number = 5;

requestDto: PagedLocationHistoryRequestDto;
  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private _locationService: LocationServiceProxy,
    cd: ChangeDetectorRef
  ) {
    super(injector, cd);
    this.requestDto = new PagedLocationHistoryRequestDto();
  }
  

  onPageChange(event: any): void {
    debugger
    const pageNumber = (event.page ?? 0) + 1; // p-table uses 0-based index for pages
    this.list(this.requestDto, pageNumber, () => {
      // Optional: Any additional logic after loading is finished
    });
    
  }

  list(
    request: PagedLocationHistoryRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    request.maxResultCount=this.maxResultCount;
    request.skipCount=(pageNumber-1)*request.maxResultCount;

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


  editLocation(locationHistory: LocationHistoryDto): void {
    this.showCreateOrEditLocDialog(locationHistory.id);
  }


  showCreateOrEditLocDialog(id?: number): void {
    let createOrEditLocDialog: BsModalRef;
    debugger
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

    createOrEditLocDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
    // createOrEditLocDialog.content.onSave.subscribe((value) => {
    //   if(value){
    //     this.list();
    //   }
    // });
  }
}