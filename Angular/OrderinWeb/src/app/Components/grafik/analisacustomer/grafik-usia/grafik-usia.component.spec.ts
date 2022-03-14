import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrafikUsiaComponent } from './grafik-usia.component';

describe('GrafikUsiaComponent', () => {
  let component: GrafikUsiaComponent;
  let fixture: ComponentFixture<GrafikUsiaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrafikUsiaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrafikUsiaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
