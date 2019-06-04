import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TabbedChartsComponent } from './tabbed-charts.component';

describe('TabbedChartsComponent', () => {
    let component: TabbedChartsComponent;
    let fixture: ComponentFixture<TabbedChartsComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [TabbedChartsComponent]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TabbedChartsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
