import { Component, OnInit, Input } from '@angular/core';
import { RadialChartOptions, ChartDataSets, ChartType } from 'chart.js';
import { Label } from 'ng2-charts';
import { BaseChartComponent } from '../base-chart.component';

@Component({
    selector: 'app-radar-chart',
    templateUrl: './radar-chart.component.html',
    styleUrls: ['./radar-chart.component.css']
})
export class RadarChartComponent extends BaseChartComponent implements OnInit {
    constructor() {
        super();

        this.chartType = 'radar';
        this.chartOptions = { responsive: true } as RadialChartOptions;
    }

    ngOnInit() {
    }

}
