import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RateHistoryComponent } from './rate-history.component';

describe('RateHistoryComponent', () => {
  let component: RateHistoryComponent;
  let fixture: ComponentFixture<RateHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RateHistoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RateHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
