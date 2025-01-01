import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PriceReplicaComponent } from './price-replica.component';

describe('PriceReplicaComponent', () => {
  let component: PriceReplicaComponent;
  let fixture: ComponentFixture<PriceReplicaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PriceReplicaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PriceReplicaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
