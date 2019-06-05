import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, FormControl, Validators, AbstractControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable, of, Subscription } from 'rxjs';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { getAllControlErrors, showMessageBox } from 'src/app/_helpers/helper-functions';

import { FinancialService } from 'src/app/_services/financial.service';
import { NetPresentValueRequest } from 'src/app/_models/net-present-value-request';
import { NetPresentValueResponse } from 'src/app/_models/net-present-value-response';
import { NetPresentValuePerRate } from 'src/app/_models/net-present-value-per-rate';
import { CashFlow } from 'src/app/_models/cash-flow';
import { PeriodAmount } from 'src/app/_models/period-amount';

@Component({
    selector: 'app-net-present-value',
    templateUrl: './net-present-value.component.html',
    styleUrls: ['./net-present-value.component.css']
})
export class NetPresentValueComponent implements OnInit, OnDestroy {
    subscription: Subscription;
    formGroup: FormGroup;
    bsModalRef: BsModalRef;
    rows: Observable<any[]>;

    isLoading = false;
    chartLabels = null;
    chartData = null;

    get cashFlowFormArray(): FormArray {
        return this.formGroup.get('cashFlows') as FormArray;
    }

    get canDeleteCashFlow(): boolean {
        return this.cashFlowFormArray.controls.length > 1;
    }

    get currentTabIndex(): number {
        return 4 + this.cashFlowFormArray.controls.length;
    }

    constructor(
        private activatedRoute: ActivatedRoute,
        private formBuilder: FormBuilder,
        private financialService: FinancialService,
        private modalService: BsModalService) {
        this.formGroup = this.formBuilder.group({
            netPresentValueId: [0],
            initialInvestment: [null, Validators.required],
            lowerBoundDiscountRate: [null, Validators.required],
            upperBoundDiscountRate: [null, Validators.required],
            discountRateIncrement: [null, Validators.required],
            cashFlows: this.formBuilder.array([null])
        });
    }

    ngOnInit() {
        this.activatedRoute.data.subscribe(data => {
            // tslint:disable-next-line: no-string-literal
            const netPresentValueData = data['netPresentValue'];
            this.setFormData(netPresentValueData);
        });
    }

    ngOnDestroy() {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }

    setFormData(data: NetPresentValueResponse) {
        if (data) {
            this.formGroup.setValue({
                netPresentValueId: data.netPresentValueId,
                initialInvestment: data.initialInvestment,
                lowerBoundDiscountRate: data.lowerBoundDiscountRate,
                upperBoundDiscountRate: data.upperBoundDiscountRate,
                discountRateIncrement: data.discountRateIncrement,
                cashFlows: [null]
            });

            if (data.cashFlows) {
                const cashFlowLength = Math.max(...data.cashFlows.map(cf => cf.period));

                for (let i = 1; i <= cashFlowLength - 1; i++) {
                    this.cashFlowFormArray.push(new FormControl(null));
                }

                data.cashFlows.forEach((cf: CashFlow) => {
                    this.cashFlowFormArray.at(cf.period - 1).setValue(cf.amount);
                });
            }

            this.rows = this.getTableData(data.results);
            const chartDataAndLabels = this.getChartData(data.results);
            this.chartLabels = chartDataAndLabels[0];
            this.chartData = chartDataAndLabels[1];
        }
    }

    getChartData(npvs: NetPresentValuePerRate[]) {
        const labels: string[] = npvs.map(npv => npv.rate.toFixed(2));
        const data = [{
            data: npvs.map(npv => Number(npv.amount.toFixed(2))),
            label: 'Net Present Value'
        }];

        return [labels, data];
    }

    getTableData(data: NetPresentValuePerRate[]): Observable<NetPresentValuePerRate[]> {
        return of(data);
    }

    addCashFlowControl() {
        this.cashFlowFormArray.push(new FormControl(null));
    }

    deleteCashFlowControl(index: number) {
        if (!this.canDeleteCashFlow) {
            alert('Cannot delete cash flow!');
            return;
        }

        this.cashFlowFormArray.removeAt(index);
    }

    getFormValue(): NetPresentValueRequest {
        const formValue = this.formGroup.getRawValue();
        const newValue = Object.assign({}, formValue);

        newValue.cashFlows = formValue.cashFlows
            .map((value: number, index: number) =>
                ({ period: index + 1, amount: value || 0 } as CashFlow))
            .filter((cf: PeriodAmount) => cf.amount !== 0);

        return newValue as NetPresentValueRequest;
    }

    calculate() {
        if (this.formGroup.invalid) {
            const controlErrors: string[] =
                getAllControlErrors(
                    this.formGroup,
                    ['initialInvestment', 'lowerBoundDiscountRate', 'upperBoundDiscountRate', 'discountRateIncrement'],
                    ['Initial Investment', 'Lower Bound Discount Rate', 'Upper Bound Discount Rate', 'Discount Rate Increment']);

            this.bsModalRef = showMessageBox(this.modalService, this.bsModalRef, 'Error', controlErrors);
            return;
        }

        this.isLoading = true;
        const formValue = this.getFormValue();

        this.subscription = this.financialService.getNetPresentValueDynamicRate(formValue).subscribe(
            (response: NetPresentValuePerRate[]) => {
                this.rows = this.getTableData(response);
                const chartDataAndLabels = this.getChartData(response);
                this.chartLabels = chartDataAndLabels[0];
                this.chartData = chartDataAndLabels[1];
            }, error => {
                console.log(error, 'Error in getNetPresentValueDynamicRate method');
                this.bsModalRef = showMessageBox(this.modalService, this.bsModalRef, 'Error', [`${error.status} - ${error.statusText}`]);
                this.isLoading = false;
            }, () => {
                this.isLoading = false;
            });
    }

    clear() {
        this.isLoading = false;
        this.rows = null;
        this.chartLabels = null;
        this.chartData = null;

        this.formGroup.reset({
            netPresentValueId: 0,
            initialInvestment: null,
            lowerBoundDiscountRate: null,
            upperBoundDiscountRate: null,
            discountRateIncrement: null,
            cashFlows: new Array(this.cashFlowFormArray.length).fill(null)
        });

    }
}
