import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartsModule } from 'ng2-charts';
import { LineChartComponent } from './line-chart/line-chart.component';
import { BarChartComponent } from './bar-chart/bar-chart.component';
import { RadarChartComponent } from './radar-chart/radar-chart.component';
import { TabbedChartsComponent } from './tabbed-charts/tabbed-charts.component';
import { TabsModule } from 'ngx-bootstrap/tabs';

@NgModule({
    declarations: [
        LineChartComponent,
        BarChartComponent,
        RadarChartComponent,
        TabbedChartsComponent,
    ],
    imports: [
        CommonModule,
        ChartsModule,
        TabsModule.forRoot(),
    ],
    exports: [
        LineChartComponent,
        BarChartComponent,
        RadarChartComponent,
        TabbedChartsComponent,
    ],
    entryComponents: [
        LineChartComponent,
        BarChartComponent,
        RadarChartComponent,
        TabbedChartsComponent,
    ]
})
export class ChartListModule { }
