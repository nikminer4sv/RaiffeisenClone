import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewDepositComponent } from './new-deposit.component';

describe('NewDepositComponent', () => {
  let component: NewDepositComponent;
  let fixture: ComponentFixture<NewDepositComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewDepositComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewDepositComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
