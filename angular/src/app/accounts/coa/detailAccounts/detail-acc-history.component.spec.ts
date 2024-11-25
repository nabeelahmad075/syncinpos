import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailAccHistoryComponent } from './detail-acc-history.component';

describe('DetailAccHistoryComponent', () => {
  let component: DetailAccHistoryComponent;
  let fixture: ComponentFixture<DetailAccHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetailAccHistoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetailAccHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
