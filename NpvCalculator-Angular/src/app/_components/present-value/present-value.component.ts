import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators, AbstractControl } from '@angular/forms';
import { Observable, Subscription, of } from 'rxjs';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { getAllControlErrors, showMessageBox } from 'src/app/_helpers/helper-functions';

import { FinancialService } from 'src/app/_services/financial.service';
import { PresentValueRequest } from 'src/app/_models/present-value-request';
import { PeriodAmount } from 'src/app/_models/period-amount';

@Component({
    selector: 'app-present-value',
    templateUrl: './present-value.component.html',
    styleUrls: ['./present-value.component.css']
})
export class PresentValueComponent implements OnInit, OnDestroy {
    subscription: Subscription;
    formGroup: FormGroup;
    bsModalRef: BsModalRef;
    rows: Observable<any[]>;

    isLoading = false;
    chartLabels = null;
    chartData = null;
    presentValue = 0;

    get presentValueClass(): string {
        return this.presentValue === 0 ? 'text-secondary' : 'text-success';
    }

    constructor(
        private formBuilder: FormBuilder,
        private financialService: FinancialService,
        private modalService: BsModalService) {
        this.formGroup = this.formBuilder.group({
            futureValue: [null, Validators.required],
            discountRate: [null, Validators.required],
            periods: [null, Validators.required],
            compoundInterval: [1]
        });
    }

    ngOnInit() {
        this.formGroup.get('compoundInterval').disable();
    }

    ngOnDestroy() {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }

    calculate() {
        if (this.formGroup.invalid) {
            const controlErrors: string[] =
                getAllControlErrors(
                    this.formGroup,
                    ['futureValue', 'discountRate', 'periods'],
                    ['Future Value', 'Discount Rate', 'Periods']);

            this.bsModalRef = showMessageBox(this.modalService, this.bsModalRef, 'Error', controlErrors);
            return;
        }

        this.isLoading = true;
        const formValue = this.formGroup.getRawValue() as PresentValueRequest;

        this.subscription = this.financialService.getPresentValueMulti(formValue).subscribe(
            (response: PeriodAmount[]) => {
                if (response.length > 0) {
                    this.presentValue = response[0].amount;
                }

                this.rows = of(response);
                this.chartLabels = response.map(pv => pv.period.toFixed(2));
                this.chartData = [{
                    data: response.map(pv => Number(pv.amount.toFixed(2))),
                    label: 'Present Value'
                }];
            }, error => {
                console.log(error, 'Error in getPresentValueMulti method');
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
        this.presentValue = 0;

        this.formGroup.reset({
            futureValue: null,
            discountRate: null,
            periods: null,
            compountInterval: 1
        });
    }
}
