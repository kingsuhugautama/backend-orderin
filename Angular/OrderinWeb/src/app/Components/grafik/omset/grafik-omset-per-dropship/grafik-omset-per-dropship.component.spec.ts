import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrafikOmsetPerDropshipComponent } from './grafik-omset-per-dropship.component';

describe('GrafikOmsetPerDropshipComponent', () => {
  let component: GrafikOmsetPerDropshipComponent;
  let fixture: ComponentFixture<GrafikOmsetPerDropshipComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrafikOmsetPerDropshipComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrafikOmsetPerDropshipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
