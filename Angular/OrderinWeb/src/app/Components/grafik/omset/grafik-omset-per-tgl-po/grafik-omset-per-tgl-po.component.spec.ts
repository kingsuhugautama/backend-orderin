import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrafikOmsetPerTglPoComponent } from './grafik-omset-per-tgl-po.component';

describe('GrafikOmsetPerTglPoComponent', () => {
  let component: GrafikOmsetPerTglPoComponent;
  let fixture: ComponentFixture<GrafikOmsetPerTglPoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrafikOmsetPerTglPoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrafikOmsetPerTglPoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
