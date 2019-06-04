import { Component, OnInit, Input } from '@angular/core';
import { Label } from 'ng2-charts';
import { ChartDataSets } from 'chart.js';

@Component({
    selector: 'app-tabbed-charts',
    templateUrl: './tabbed-charts.component.html',
    styleUrls: ['./tabbed-charts.component.css']
})
export class TabbedChartsComponent implements OnInit {
    @Input()
    chartLabels: Label[];

    @Input()
    chartData: ChartDataSets[];

    constructor() { }

    ngOnInit() {
    }
}
