import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditItemDefinitionComponent } from './add-edit-item-definition.component';

describe('AddEditItemDefinitionComponent', () => {
  let component: AddEditItemDefinitionComponent;
  let fixture: ComponentFixture<AddEditItemDefinitionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddEditItemDefinitionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddEditItemDefinitionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
