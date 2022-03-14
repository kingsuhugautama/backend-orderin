import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrafikPeringkatProdukPerDropshipComponent } from './grafik-peringkat-produk-per-dropship.component';

describe('GrafikPeringkatProdukPerDropshipComponent', () => {
  let component: GrafikPeringkatProdukPerDropshipComponent;
  let fixture: ComponentFixture<GrafikPeringkatProdukPerDropshipComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrafikPeringkatProdukPerDropshipComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrafikPeringkatProdukPerDropshipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
