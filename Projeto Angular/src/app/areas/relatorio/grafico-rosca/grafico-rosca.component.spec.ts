import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GraficoRoscaComponent } from './grafico-rosca.component';

describe('GraficoRoscaComponent', () => {
  let component: GraficoRoscaComponent;
  let fixture: ComponentFixture<GraficoRoscaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GraficoRoscaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GraficoRoscaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
