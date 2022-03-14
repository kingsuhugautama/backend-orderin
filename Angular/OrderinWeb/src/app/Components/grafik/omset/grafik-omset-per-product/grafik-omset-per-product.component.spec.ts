import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrafikOmsetPerProductComponent } from './grafik-omset-per-product.component';

describe('GrafikOmsetPerProductComponent', () => {
  let component: GrafikOmsetPerProductComponent;
  let fixture: ComponentFixture<GrafikOmsetPerProductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrafikOmsetPerProductComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrafikOmsetPerProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
