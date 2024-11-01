import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SectionHistoryComponent } from './section-history.component';

describe('SectionHistoryComponent', () => {
  let component: SectionHistoryComponent;
  let fixture: ComponentFixture<SectionHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SectionHistoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SectionHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
