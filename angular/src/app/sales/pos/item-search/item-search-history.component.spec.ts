import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemSearchHistoryComponent } from './item-search-history.component';

describe('ItemSearchHistoryComponent', () => {
  let component: ItemSearchHistoryComponent;
  let fixture: ComponentFixture<ItemSearchHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ItemSearchHistoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ItemSearchHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
