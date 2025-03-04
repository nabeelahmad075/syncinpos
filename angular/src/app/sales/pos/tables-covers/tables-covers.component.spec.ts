import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TablesCoversComponent } from './tables-covers.component';

describe('TablesCoversComponent', () => {
  let component: TablesCoversComponent;
  let fixture: ComponentFixture<TablesCoversComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TablesCoversComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TablesCoversComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
