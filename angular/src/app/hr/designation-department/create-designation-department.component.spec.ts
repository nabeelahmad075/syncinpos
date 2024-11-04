import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateDesignationDepartmentComponent } from './create-designation-department.component';

describe('CreateDesignationDepartmentComponent', () => {
  let component: CreateDesignationDepartmentComponent;
  let fixture: ComponentFixture<CreateDesignationDepartmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateDesignationDepartmentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateDesignationDepartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
