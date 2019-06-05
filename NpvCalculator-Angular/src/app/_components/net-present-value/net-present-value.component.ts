import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, FormControl, Validators, AbstractControl } from '@angular/forms';
import { FinancialService } from 'src/app/_services/financial.service';
import { CashFlow } from 'src/app/_models/cash-flow';
import { NetPresentValueRequest } from 'src/app/_models/net-present-value-request';
import { NetPresentValuePerRate } from 'src/app/_models/net-present-value-per-rate';
import { Observable, of, Subscription } from 'rxjs';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { getAllControlErrors, showMessageBox } from 'src/app/_helpers/helper-functions';
import { PeriodAmount } from 'src/app/_models/period-amount';
import { ActivatedRoute } from '@angular/router';
import { NetPresentValueResponse } from 'src/app/_models/net-present-value-response';

@Component({
    selector: 'app-net-present-value',
    templateUrl: './net-present-value.component.html',
    styleUrls: ['./net-present-value.component.css']
})
export class NetPresentValueComponent implements OnInit, OnDestroy {
    bsModalRef: BsModalRef;
    serviceSubscription: Subscription;
    npvForm: FormGroup;
    rows: Observable<any[]>;
    // columns: any[] = [
    //     { prop: 'value', name: 'Net Present Value' },
    //     { prop: 'rate', name: 'Rate' }
    // ];
    chartLabels = null;
    chartData = null;
    isLoading = false;

    get cashFlowFormArray(): FormArray {
        return this.npvForm.get('cashFlows') as FormArray;
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
        this.npvForm = this.formBuilder.group({
            initialInvestment: ['', Validators.required],
            lowerBoundDiscountRate: ['', Validators.required],
            upperBoundDiscountRate: ['', Validators.required],
            discountRateIncrement: ['', Validators.required],
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
        if (this.serviceSubscription) {
            this.serviceSubscription.unsubscribe();
            this.serviceSubscription = null;
        }
    }

    setFormData(data: NetPresentValueResponse) {
        if (data) {
            this.npvForm.setValue({
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

    getNpvFormValues(): NetPresentValueRequest {
        return {
            initialInvestment: this.npvForm.value.initialInvestment,
            lowerBoundDiscountRate: this.npvForm.value.lowerBoundDiscountRate,
            upperBoundDiscountRate: this.npvForm.value.upperBoundDiscountRate,
            discountRateIncrement: this.npvForm.value.discountRateIncrement,
            cashFlows: this.npvForm.value.cashFlows
                .map((value: number, index: number) =>
                    ({ period: index + 1, amount: value || 0 } as CashFlow))
                .filter((cf: PeriodAmount) => cf.amount !== 0)
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

        this.npvForm.reset({
            initialInvestment: null,
            lowerBoundDiscountRate: null,
            upperBoundDiscountRate: null,
            discountRateIncrement: null,
            cashFlows: new Array(this.cashFlowFormArray.length).fill(null)
        });

        this.npvForm.markAsPristine();
        this.npvForm.markAsUntouched();
        this.cashFlowFormArray.markAsPristine();
        this.cashFlowFormArray.markAsUntouched();

        console.log(this.npvForm, 'form group');
        console.log(this.cashFlowFormArray, 'form array');

    }
}
