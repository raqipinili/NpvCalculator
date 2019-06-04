import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PresentValueComponent } from './present-value.component';

describe('PresentValueComponent', () => {
  let component: PresentValueComponent;
  let fixture: ComponentFixture<PresentValueComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PresentValueComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PresentValueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
