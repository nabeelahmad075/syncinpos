import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemCategoryHistoryComponent } from './item-category-history.component';

describe('ItemCategoryHistoryComponent', () => {
  let component: ItemCategoryHistoryComponent;
  let fixture: ComponentFixture<ItemCategoryHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ItemCategoryHistoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ItemCategoryHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
