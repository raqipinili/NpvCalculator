import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, FormControl, Validators, AbstractControl } from '@angular/forms';
import { FinancialService } from 'src/app/_services/financial.service';
import { CashFlow } from 'src/app/_models/cash-flow';
import { NetPresentValueRequest } from 'src/app/_models/net-present-value-request';
import { Observable } from 'rxjs';

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

    isLoading = false;
    chartLabels = null;
    chartData = null;

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
        this.isLoading = true;
        const npvFormValues = this.getNpvFormValues();

        this.financialService.getNetPresentValueDynamicRate(npvFormValues)
            .subscribe(response => {
                // this.rows = Object.assign([], response.netPresentValues);
                // console.log(this.rows);

                this.chartLabels = response.netPresentValues.map(npv => npv.rate.toFixed(2));
                this.chartData = [{
                    data: response.netPresentValues.map(npv => Number(npv.value.toFixed(2))),
                    label: 'Net Present Value'
                }];

                this.rows = Observable.create((subscriber) => {
                    subscriber.next(response.netPresentValues);
                    subscriber.complete();
                });

                this.isLoading = false;
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

        this.isLoading = false;
        this.chartLabels = null;
        this.chartData = null;

        this.npvForm.reset({
            initialInvestment: 1500,
            lowerBoundDiscountRate: 1,
            upperBoundDiscountRate: 15,
            discountRateIncrement: .25
        });
    }
}
