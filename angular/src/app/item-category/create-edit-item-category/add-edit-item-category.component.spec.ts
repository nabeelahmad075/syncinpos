import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditItemCategoryComponent } from './add-edit-item-category.component';

describe('AddEditItemCategoryComponent', () => {
  let component: AddEditItemCategoryComponent;
  let fixture: ComponentFixture<AddEditItemCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddEditItemCategoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddEditItemCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
