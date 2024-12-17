import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '../../shared/app-component-base';

@Component({
  selector: 'header-date',
  // standalone: true,
  // imports: [],
  templateUrl: './header-date.component.html'
})
export class HeaderDateComponent extends AppComponentBase implements OnInit{
  
  softwareDate: Date;
    constructor(injector: Injector) {
      super(injector);
    }

    ngOnInit(): void {
      this.softwareDate = new Date();
    }

}
