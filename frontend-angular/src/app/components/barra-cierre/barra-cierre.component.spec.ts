import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BarraCierreComponent } from './barra-cierre.component';

describe('BarraCierreComponent', () => {
  let component: BarraCierreComponent;
  let fixture: ComponentFixture<BarraCierreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BarraCierreComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BarraCierreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
