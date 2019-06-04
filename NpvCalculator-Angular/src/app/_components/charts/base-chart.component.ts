import { Input } from '@angular/core';
import { Label, Color } from 'ng2-charts';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';

export class BaseChartComponent {
    chartType: ChartType;
    chartOptions: ChartOptions;
    chartColors: Color[];
    chartLegend: boolean;
    chartPlugins: any[];

    private $chartLabels: Label[];
    private $chartData: ChartDataSets[];

    get chartLabels(): Label[] {
        return this.$chartLabels;
    }

    @Input()
    set chartLabels(value) {
        this.$chartLabels = (value instanceof Array) ? value : new Array<string>();
    }

    get chartData(): ChartDataSets[] {
        return this.$chartData;
    }

    @Input()
    set chartData(value) {
        this.$chartData = (value instanceof Array) ? value : [{ data: [], label: '' }];
    }

    constructor() {
        this.chartType = null;
        this.chartLegend = true;
        this.chartPlugins = [];
        this.chartOptions = { responsive: true };

        this.chartColors = [{
            // red
            backgroundColor: 'rgba(255,0,0,0.3)',
            borderColor: 'red',
            pointBackgroundColor: 'rgba(148,159,177,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(148,159,177,0.8)'
        }, {
            // dark grey
            backgroundColor: 'rgba(77,83,96,0.2)',
            borderColor: 'rgba(77,83,96,1)',
            pointBackgroundColor: 'rgba(77,83,96,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(77,83,96,1)'
        }, {
            // grey
            backgroundColor: 'rgba(148,159,177,0.2)',
            borderColor: 'rgba(148,159,177,1)',
            pointBackgroundColor: 'rgba(148,159,177,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(148,159,177,0.8)'
        }];
    }
}
