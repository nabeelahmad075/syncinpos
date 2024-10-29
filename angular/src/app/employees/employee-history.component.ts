import { ChangeDetectorRef, Component, Injector, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AddEditEmpComponent } from './create-edit-employee/add-edit-emp.component';
import { extend, sortBy } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { finalize } from 'rxjs/operators';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';

@Component({
  selector: 'app-employee-history',
  // standalone: true,
  // imports: [],
  templateUrl: './employee-history.component.html',
  styleUrl: './employee-history.component.css',
  animations: [appModuleAnimation()]
})
export class EmployeeHistoryComponent{
  
  // constructor(
  //   injector: Injector,
  //   private _modalService: BsModalService,
  //   // private _locationService: LocationServiceProxy,
  //   // cd: ChangeDetectorRef
  // ) {
  //   super(injector);
  //   // this.requestDto = new PagedLocationHistoryRequestDto();
  // }

  // createEmployee(): void {
  //   this.showCreateOrEditEmpDialog();
  // }

  // showCreateOrEditEmpDialog(id?: number): void {
  //   let createOrEditEmpDialog: BsModalRef;
  //   if (!id) {
  //     createOrEditEmpDialog = this._modalService.show(
  //       AddEditEmpComponent,
  //       {
  //         class: 'modal-lg',
  //       }
  //     );
  //   }
  //   else {
  //     createOrEditEmpDialog = this._modalService.show(
  //       AddEditEmpComponent, {
  //       class: 'modal-lg',
  //       initialState: {
  //         id: id,
  //       },
  //     }
  //     );
  //   }

    // createOrEditEmpDialog.content.onSave.subscribe(() => {
    //   this.refresh();
    // });

}
