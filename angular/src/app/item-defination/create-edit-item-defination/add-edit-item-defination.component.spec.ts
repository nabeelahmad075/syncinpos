import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditItemDefinationComponent } from './add-edit-item-defination.component';

describe('AddEditItemDefinationComponent', () => {
  let component: AddEditItemDefinationComponent;
  let fixture: ComponentFixture<AddEditItemDefinationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddEditItemDefinationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddEditItemDefinationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
