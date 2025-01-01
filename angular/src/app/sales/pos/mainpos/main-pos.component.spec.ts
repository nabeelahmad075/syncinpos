import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainPosComponent } from './main-pos.component';

describe('MainPosComponent', () => {
  let component: MainPosComponent;
  let fixture: ComponentFixture<MainPosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MainPosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MainPosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
