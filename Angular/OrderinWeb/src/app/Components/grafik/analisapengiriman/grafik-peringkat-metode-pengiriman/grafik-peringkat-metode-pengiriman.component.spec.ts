import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrafikPeringkatMetodePengirimanComponent } from './grafik-peringkat-metode-pengiriman.component';

describe('GrafikPeringkatMetodePengirimanComponent', () => {
  let component: GrafikPeringkatMetodePengirimanComponent;
  let fixture: ComponentFixture<GrafikPeringkatMetodePengirimanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrafikPeringkatMetodePengirimanComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrafikPeringkatMetodePengirimanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
