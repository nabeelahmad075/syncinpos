import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditPriceComponent } from './add-edit-price.component';

describe('AddEditPriceComponent', () => {
  let component: AddEditPriceComponent;
  let fixture: ComponentFixture<AddEditPriceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddEditPriceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddEditPriceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
