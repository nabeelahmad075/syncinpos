import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DayCloseHistoryComponent } from './day-close-history.component';

describe('DayCloseHistoryComponent', () => {
  let component: DayCloseHistoryComponent;
  let fixture: ComponentFixture<DayCloseHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DayCloseHistoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DayCloseHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
