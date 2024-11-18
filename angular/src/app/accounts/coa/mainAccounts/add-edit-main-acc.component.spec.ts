import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditMainAccComponent } from './add-edit-main-acc.component';

describe('AddEditMainAccComponent', () => {
  let component: AddEditMainAccComponent;
  let fixture: ComponentFixture<AddEditMainAccComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddEditMainAccComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddEditMainAccComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
