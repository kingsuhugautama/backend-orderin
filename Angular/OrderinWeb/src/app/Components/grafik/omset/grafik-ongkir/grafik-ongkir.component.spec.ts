import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrafikOngkirComponent } from './grafik-ongkir.component';

describe('GrafikOngkirComponent', () => {
  let component: GrafikOngkirComponent;
  let fixture: ComponentFixture<GrafikOngkirComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrafikOngkirComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrafikOngkirComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
