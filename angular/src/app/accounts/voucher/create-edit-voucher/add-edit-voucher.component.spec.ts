import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditVoucherComponent } from './add-edit-voucher.component';

describe('AddEditVoucherComponent', () => {
  let component: AddEditVoucherComponent;
  let fixture: ComponentFixture<AddEditVoucherComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddEditVoucherComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddEditVoucherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
