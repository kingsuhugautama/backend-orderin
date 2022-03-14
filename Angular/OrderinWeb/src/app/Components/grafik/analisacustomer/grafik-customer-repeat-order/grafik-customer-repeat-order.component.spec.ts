import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrafikCustomerRepeatOrderComponent } from './grafik-customer-repeat-order.component';

describe('GrafikCustomerRepeatOrderComponent', () => {
  let component: GrafikCustomerRepeatOrderComponent;
  let fixture: ComponentFixture<GrafikCustomerRepeatOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrafikCustomerRepeatOrderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrafikCustomerRepeatOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
