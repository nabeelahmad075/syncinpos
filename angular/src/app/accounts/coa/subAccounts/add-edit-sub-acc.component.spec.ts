import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditSubAccComponent } from './add-edit-sub-acc.component';

describe('AddEditSubAccComponent', () => {
  let component: AddEditSubAccComponent;
  let fixture: ComponentFixture<AddEditSubAccComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddEditSubAccComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddEditSubAccComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
