import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NetPresentValueComponent } from './net-present-value.component';

describe('NetPresentValueComponent', () => {
    let component: NetPresentValueComponent;
    let fixture: ComponentFixture<NetPresentValueComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [NetPresentValueComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(NetPresentValueComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
