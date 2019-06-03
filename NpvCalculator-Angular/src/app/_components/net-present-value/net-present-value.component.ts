import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, FormControl, Validators, AbstractControl } from '@angular/forms';
import { FinancialService } from 'src/app/_services/financial.service';
import { CashFlow } from 'src/app/_models/cash-flow';
import { NetPresentValueRequest } from 'src/app/_models/net-present-value-request';
import { Observable } from 'rxjs';
import { ChartDataSets, ChartOptions, ChartData } from 'chart.js';
import { Label, Color, BaseChartDirective } from 'ng2-charts';
import * as pluginAnnotations from 'chartjs-plugin-annotation';

@Component({
    selector: 'app-net-present-value',
    templateUrl: './net-present-value.component.html',
    styleUrls: ['./net-present-value.component.css']
})
export class NetPresentValueComponent implements OnInit {
    npvForm: FormGroup;
    cashFlowCount = 5;
    rows: Observable<any[]>;
    // columns: any[] = [
    //     { prop: 'value', name: 'Net Present Value' },
    //     { prop: 'rate', name: 'Rate' }
    // ];

    lineChartData: ChartDataSets[] = [
        { data: [], label: 'Net Present Value' }
    ];
    lineChartLabels: Label[] = [];
    lineChartOptions: (ChartOptions & { annotation: any }) = {
        responsive: true,
        scales: {
            // We use this empty structure as a placeholder for dynamic theming.
            xAxes: [{}],
            yAxes: [
                {
                    id: 'y-axis-0',
                    position: 'left',
                },
                {
                    id: 'y-axis-1',
                    position: 'right',
                    gridLines: {
                        color: 'rgba(255,0,0,0.3)',
                    },
                    ticks: {
                        fontColor: 'red',
                    }
                }
            ]
        },
        annotation: {
            annotations: [
                {
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
                },
            ],
        },
    };
    lineChartColors: Color[] = [{
        // grey
        backgroundColor: 'rgba(148,159,177,0.2)',
        borderColor: 'rgba(148,159,177,1)',
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
        // red
        backgroundColor: 'rgba(255,0,0,0.3)',
        borderColor: 'red',
        pointBackgroundColor: 'rgba(148,159,177,1)',
        pointBorderColor: '#fff',
        pointHoverBackgroundColor: '#fff',
        pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    }];

    lineChartLegend = true;
    lineChartType = 'line';
    lineChartPlugins = [pluginAnnotations];
    // @ViewChild(BaseChartDirective, { static: true }) chart: BaseChartDirective;

    get cashFlowFormArray(): FormArray {
        return this.npvForm.get('cashFlows') as FormArray;
    }

    get canDeleteCashFlow(): boolean {
        return this.cashFlowFormArray.controls.length > 1;
    }

    get currentTabIndex(): number {
        return 4 + this.cashFlowFormArray.controls.length;
    }

    get defaultCashFlowValues(): number[] {
        return new Array<number>(this.cashFlowCount).fill(null);
    }

    constructor(
        private formBuilder: FormBuilder,
        private financialService: FinancialService) {
        this.npvForm = this.formBuilder.group({
            initialInvestment: [15000, Validators.required],
            lowerBoundDiscountRate: [1, Validators.required],
            upperBoundDiscountRate: [15, Validators.required],
            discountRateIncrement: [.25, Validators.required],
            cashFlows: this.formBuilder.array(this.defaultCashFlowValues)
        });
    }

    ngOnInit() {
    }

    addCashFlowControl() {
        this.cashFlowFormArray.push(new FormControl(null, Validators.required));
    }

    deleteCashFlowControl(index: number) {
        if (!this.canDeleteCashFlow) {
            alert('Cannot delete cash flow!');
            return;
        }

        this.cashFlowFormArray.removeAt(index);
    }

    getNpvFormValues(): NetPresentValueRequest {
        return {
            initialInvestment: this.npvForm.value.initialInvestment,
            lowerBoundDiscountRate: this.npvForm.value.lowerBoundDiscountRate,
            upperBoundDiscountRate: this.npvForm.value.upperBoundDiscountRate,
            discountRateIncrement: this.npvForm.value.discountRateIncrement,
            cashFlows: this.npvForm.value.cashFlows.map((value: number, index: number) =>
                ({ period: index, amount: value || 0 } as CashFlow))
        } as NetPresentValueRequest;
    }

    calculate() {
        const npvFormValues = this.getNpvFormValues();

        this.financialService.getNetPresentValueDynamicRate(npvFormValues)
            .subscribe(response => {
                // this.rows = Object.assign([], response.netPresentValues);
                // console.log(this.rows);

                this.lineChartLabels = response.netPresentValues.map(npv => npv.rate.toString());
                this.lineChartData = [{
                    data: response.netPresentValues.map(npv => npv.value.toFixed(2)),
                    label: 'Net Present Value'
                } as ChartDataSets];

                this.rows = Observable.create((subscriber) => {
                    subscriber.next(response.netPresentValues);
                    subscriber.complete();
                });
            });
    }

    clear() {
        this.rows = null;

        while (this.cashFlowFormArray.length !== 0) {
            this.cashFlowFormArray.removeAt(0);
        }

        for (const value of this.defaultCashFlowValues) {
            this.cashFlowFormArray.push(new FormControl(value, Validators.required));
        }

        this.npvForm.reset({
            initialInvestment: 1500,
            lowerBoundDiscountRate: 1,
            upperBoundDiscountRate: 15,
            discountRateIncrement: .25
        });
    }
}
