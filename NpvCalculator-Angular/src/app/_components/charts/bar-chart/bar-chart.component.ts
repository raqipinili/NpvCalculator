import { Component, OnInit, Input } from '@angular/core';
import { ChartDataSets, ChartType, ChartOptions } from 'chart.js';
import { Label } from 'ng2-charts';
import { BaseChartComponent } from '../base-chart.component';


@Component({
    selector: 'app-bar-chart',
    templateUrl: './bar-chart.component.html',
    styleUrls: ['./bar-chart.component.css']
})
export class BarChartComponent extends BaseChartComponent implements OnInit {
    constructor() {
        super();

        this.chartType = 'bar';
        this.chartOptions = { responsive: true };
        this.chartLegend = true;
        this.chartPlugins = [];
    }

    ngOnInit() {
    }
}
