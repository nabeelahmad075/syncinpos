import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentHistoryComponent } from './department-history.component';

describe('DepartmentHistoryComponent', () => {
  let component: DepartmentHistoryComponent;
  let fixture: ComponentFixture<DepartmentHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DepartmentHistoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DepartmentHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
