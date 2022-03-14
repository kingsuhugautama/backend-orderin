import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrafikOmsetPerProductDropshipComponent } from './grafik-omset-per-product-dropship.component';

describe('GrafikOmsetPerProductDropshipComponent', () => {
  let component: GrafikOmsetPerProductDropshipComponent;
  let fixture: ComponentFixture<GrafikOmsetPerProductDropshipComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrafikOmsetPerProductDropshipComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrafikOmsetPerProductDropshipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
