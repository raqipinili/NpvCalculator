import { Component, OnInit, OnDestroy, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { Observable, Subscription, of } from 'rxjs';
import { FinancialService } from 'src/app/_services/financial.service';
import { PeriodAmount } from 'src/app/_models/period-amount';
import { FutureValueRequest } from 'src/app/_models/future-value-request';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { MessageBoxComponent } from '../message-box/message-box.component';
import { showMessageBox, getAllControlErrors } from 'src/app/_helpers/helper-functions';

@Component({
    selector: 'app-future-value',
    templateUrl: './future-value.component.html',
    styleUrls: ['./future-value.component.css']
})
export class FutureValueComponent implements OnInit, OnDestroy {
    bsModalRef: BsModalRef;
    fvForm: FormGroup;
    serviceSubscription: Subscription;
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
        this.fvForm = this.formBuilder.group({
            presentValue: [1000, Validators.required],
            interestRate: [10, Validators.required],
            periods: [10, Validators.required],
            compoundInterval: [1]
        });
    }

    ngOnInit() {
        this.fvForm.get('compoundInterval').disable();
    }

    ngOnDestroy() {
        if (this.serviceSubscription) {
            this.serviceSubscription.unsubscribe();
            this.serviceSubscription = null;
        }
    }

    getFvFormValues(): FutureValueRequest {
        return {
            presentValue: this.fvForm.value.presentValue,
            interestRate: this.fvForm.value.interestRate,
            periods: this.fvForm.value.periods,
            compountInterval: this.fvForm.value.compoundInterval
        } as FutureValueRequest;
    }

    calculate() {
        const controlErrors: string[] =
            getAllControlErrors(
                this.fvForm,
                ['presentValue', 'interestRate', 'periods'],
                ['Present Value', 'Interest Rate', 'Periods']);

        if (controlErrors.length > 0) {
            this.bsModalRef = showMessageBox(this.modalService, this.bsModalRef, 'Error', controlErrors);
            return;
        }

        this.isLoading = true;
        const fvFormValues = this.getFvFormValues();

        this.serviceSubscription = this.financialService.getFutureValueMulti(fvFormValues).subscribe(
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

        this.fvForm.reset({
            presentValue: 1000,
            interestRate: 10,
            periods: 10,
            compountInterval: 1
        });
    }

    showMessageBox(title: string, message: string[]) {
        const initialState = {
            list: message,
            title
        };

        this.bsModalRef = this.modalService.show(MessageBoxComponent, { initialState });
        this.bsModalRef.content.closeBtnName = 'Close';
    }
}
