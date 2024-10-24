import { Component, Injector, ViewChild} from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AddEditLocComponent } from './create-edit-location/add-edit-loc.component';
import { extend } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'app-location-history',
  // standalone: true,
  // imports: [],
  templateUrl: './location-history.component.html',
  styleUrl: './location-history.component.css',
  animations: [appModuleAnimation()]
})

export class LocationHistoryComponent extends AppComponentBase{

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
  ) {
    super(injector);
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
          class: 'modal-lg',
        }
      );
    }
}
}