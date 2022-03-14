import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrafikPeringkatProdukComponent } from './grafik-peringkat-produk.component';

describe('GrafikPeringkatProdukComponent', () => {
  let component: GrafikPeringkatProdukComponent;
  let fixture: ComponentFixture<GrafikPeringkatProdukComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrafikPeringkatProdukComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrafikPeringkatProdukComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
