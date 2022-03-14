import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrafikOmsetPerKategoriComponent } from './grafik-omset-per-kategori.component';

describe('GrafikOmsetPerKategoriComponent', () => {
  let component: GrafikOmsetPerKategoriComponent;
  let fixture: ComponentFixture<GrafikOmsetPerKategoriComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrafikOmsetPerKategoriComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrafikOmsetPerKategoriComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
