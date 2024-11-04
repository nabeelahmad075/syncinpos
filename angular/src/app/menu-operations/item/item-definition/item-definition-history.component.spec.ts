import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemDefinitionHistoryComponent } from './item-definition-history.component';

describe('ItemDefinitionHistoryComponent', () => {
  let component: ItemDefinitionHistoryComponent;
  let fixture: ComponentFixture<ItemDefinitionHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ItemDefinitionHistoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ItemDefinitionHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
