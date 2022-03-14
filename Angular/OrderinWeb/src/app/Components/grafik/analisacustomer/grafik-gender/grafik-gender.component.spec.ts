import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrafikGenderComponent } from './grafik-gender.component';

describe('GrafikGenderComponent', () => {
  let component: GrafikGenderComponent;
  let fixture: ComponentFixture<GrafikGenderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrafikGenderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrafikGenderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
