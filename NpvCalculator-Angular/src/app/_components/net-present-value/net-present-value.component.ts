import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, FormControl, Validators, AbstractControl } from '@angular/forms';
import { FinancialService } from 'src/app/_services/financial.service';
import { CashFlow } from 'src/app/_models/cash-flow';
import { NetPresentValueRequest } from 'src/app/_models/net-present-value-request';
import { NetPresentValue } from 'src/app/_models/net-present-value';
import { Observable, of, Subscription } from 'rxjs';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { getAllControlErrors, showMessageBox } from 'src/app/_helpers/helper-functions';

@Component({
    selector: 'app-net-present-value',
    templateUrl: './net-present-value.component.html',
    styleUrls: ['./net-present-value.component.css']
})
export class NetPresentValueComponent implements OnInit, OnDestroy {
    bsModalRef: BsModalRef;
    npvForm: FormGroup;
    cashFlowCount = 5;
    rows: Observable<any[]>;
    serviceSubscription: Subscription = null;
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
        private financialService: FinancialService,
        private modalService: BsModalService) {
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

    ngOnDestroy() {
        if (this.serviceSubscription) {
            this.serviceSubscription.unsubscribe();
            this.serviceSubscription = null;
        }
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
                ({ period: index + 1, amount: value || 0 } as CashFlow))
        } as NetPresentValueRequest;
    }

    calculate() {
        const controlErrors: string[] =
            getAllControlErrors(
                this.npvForm,
                ['initialInvestment', 'lowerBoundDiscountRate', 'upperBoundDiscountRate', 'discountRateIncrement'],
                ['Initial Investment', 'Lower Bound Discount Rate', 'Upper Bound Discount Rate', 'Discount Rate Increment']);

        if (controlErrors.length > 0) {
            this.bsModalRef = showMessageBox(this.modalService, this.bsModalRef, 'Error', controlErrors);
            return;
        }

        this.isLoading = true;
        const npvFormValues = this.getNpvFormValues();

        this.serviceSubscription = this.financialService.getNetPresentValueDynamicRate(npvFormValues).subscribe(
            (response: NetPresentValue[]) => {
                this.rows = of(response);
                this.chartLabels = response.map(npv => npv.rate.toFixed(2));
                this.chartData = [{
                    data: response.map(npv => Number(npv.amount.toFixed(2))),
                    label: 'Net Present Value'
                }];
            }, error => {
                console.log(error, 'Error in getNetPresentValueDynamicRate method');
                this.bsModalRef = showMessageBox(this.modalService, this.bsModalRef, 'Error', [`${error.status} - ${error.statusText}`]);
                this.isLoading = false;
            }, () => {
                this.isLoading = false;
            });
    }

    clear() {
        while (this.cashFlowFormArray.length !== 0) {
            this.cashFlowFormArray.removeAt(0);
        }

        for (const value of this.defaultCashFlowValues) {
            this.cashFlowFormArray.push(new FormControl(value, Validators.required));
        }

        this.isLoading = false;
        this.rows = null;
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
