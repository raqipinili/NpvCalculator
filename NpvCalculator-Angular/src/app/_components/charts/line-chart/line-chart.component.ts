import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { BaseChartDirective, Label, Color } from 'ng2-charts';
import { ChartDataSets, ChartOptions } from 'chart.js';
import * as pluginAnnotations from 'chartjs-plugin-annotation';
import { BaseChartComponent } from '../base-chart.component';

@Component({
    selector: 'app-line-chart',
    templateUrl: './line-chart.component.html',
    styleUrls: ['./line-chart.component.css']
})
export class LineChartComponent extends BaseChartComponent implements OnInit {
    @ViewChild(BaseChartDirective, { read: true })
    chart: BaseChartDirective;

    constructor() {
        super();

        this.chartType = 'line';
        this.chartLegend = true;
        this.chartPlugins = [pluginAnnotations];

        this.chartOptions = {
            responsive: true,
            scales: {
                xAxes: [{}],
                yAxes: [{
                    id: 'y-axis-0',
                    position: 'left'
                }, {
                    id: 'y-axis-1',
                    position: 'right',
                    gridLines: { color: 'rgba(255,0,0,0.3)' },
                    ticks: { fontColor: 'red' }
                }]
            },
            annotation: {
                annotations: [{
                    type: 'line',
                    mode: 'vertical',
                    scaleID: 'x-axis-0',
                    value: 'March',
                    borderColor: 'orange',
                    borderWidth: 2,
                    label: {
                        enabled: true,
                        fontColor: 'orange',
                        content: 'LineAnno'
                    }
                }],
            },
        } as (ChartOptions & { annotation: any });
    }

    ngOnInit() {
    }
}
