import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemDefinationHistoryComponent } from './item-defination-history.component';

describe('ItemDefinationHistoryComponent', () => {
  let component: ItemDefinationHistoryComponent;
  let fixture: ComponentFixture<ItemDefinationHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ItemDefinationHistoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ItemDefinationHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
