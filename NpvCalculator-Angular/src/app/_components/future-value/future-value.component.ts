import { Component, OnInit, OnDestroy, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Observable, Subscription, of } from 'rxjs';
import { showMessageBox, getAllControlErrors } from 'src/app/_helpers/helper-functions';

import { FinancialService } from 'src/app/_services/financial.service';
import { FutureValueRequest } from 'src/app/_models/future-value-request';
import { PeriodAmount } from 'src/app/_models/period-amount';

@Component({
    selector: 'app-future-value',
    templateUrl: './future-value.component.html',
    styleUrls: ['./future-value.component.css']
})
export class FutureValueComponent implements OnInit, OnDestroy {
    subscription: Subscription;
    formGroup: FormGroup;
    bsModalRef: BsModalRef;
    rows: Observable<any[]>;

    isLoading = false;
    chartLabels = null;
    chartData = null;
    futureValue = 0;

    get futureValueClass(): string {
        return this.futureValue === 0 ? 'text-secondary' : 'text-success';
    }

    constructor(
        private formBuilder: FormBuilder,
        private financialService: FinancialService,
        private modalService: BsModalService) {
        this.formGroup = this.formBuilder.group({
            presentValue: [null, Validators.required],
            interestRate: [null, Validators.required],
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
                    ['presentValue', 'interestRate', 'periods'],
                    ['Present Value', 'Interest Rate', 'Periods']);

            this.bsModalRef = showMessageBox(this.modalService, this.bsModalRef, 'Error', controlErrors);
            return;
        }

        this.isLoading = true;
        const formValue = this.formGroup.getRawValue() as FutureValueRequest;

        this.subscription = this.financialService.getFutureValueMulti(formValue).subscribe(
            (response: PeriodAmount[]) => {
                if (response.length > 0) {
                    this.futureValue = response[response.length - 1].amount;
                }

                this.rows = of(response);
                this.chartLabels = response.map(pv => pv.period.toFixed(2));
                this.chartData = [{
                    data: response.map(pv => Number(pv.amount.toFixed(2))),
                    label: 'Present Value'
                }];
            }, error => {
                console.log(error, 'Error in getFutureValueMulti method');
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
        this.futureValue = 0;

        this.formGroup.reset({
            presentValue: null,
            interestRate: null,
            periods: null,
            compountInterval: 1
        });
    }
}
