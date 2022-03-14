import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrafikOmsetPerDropshipPerTglPoComponent } from './grafik-omset-per-dropship-per-tgl-po.component';

describe('GrafikOmsetPerDropshipPerTglPoComponent', () => {
  let component: GrafikOmsetPerDropshipPerTglPoComponent;
  let fixture: ComponentFixture<GrafikOmsetPerDropshipPerTglPoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrafikOmsetPerDropshipPerTglPoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrafikOmsetPerDropshipPerTglPoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
