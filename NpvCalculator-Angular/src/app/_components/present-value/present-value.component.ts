import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators, AbstractControl } from '@angular/forms';
import { Observable, Subscription, of } from 'rxjs';
import { FinancialService } from 'src/app/_services/financial.service';
import { PeriodAmount } from 'src/app/_models/period-amount';
import { PresentValueRequest } from 'src/app/_models/present-value-request';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { getAllControlErrors, showMessageBox } from 'src/app/_helpers/helper-functions';

// import { MessageBoxComponent } from '../message-box/message-box.component';

@Component({
    selector: 'app-present-value',
    templateUrl: './present-value.component.html',
    styleUrls: ['./present-value.component.css']
})
export class PresentValueComponent implements OnInit, OnDestroy {
    bsModalRef: BsModalRef;
    pvForm: FormGroup;
    serviceSubscription: Subscription;
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
        this.pvForm = this.formBuilder.group({
            futureValue: [1000, Validators.required],
            discountRate: [10, Validators.required],
            periods: [10, Validators.required],
            compoundInterval: [1]
        });
    }

    ngOnInit() {
        this.pvForm.get('compoundInterval').disable();
    }

    ngOnDestroy() {
        if (this.serviceSubscription) {
            this.serviceSubscription.unsubscribe();
            this.serviceSubscription = null;
        }
    }

    getPvFormValues(): PresentValueRequest {
        return {
            futureValue: this.pvForm.value.futureValue,
            discountRate: this.pvForm.value.discountRate,
            periods: this.pvForm.value.periods,
            compountInterval: this.pvForm.value.compoundInterval
        } as PresentValueRequest;
    }

    calculate() {
        const controlErrors: string[] =
            getAllControlErrors(
                this.pvForm,
                ['futureValue', 'discountRate', 'periods'],
                ['Future Value', 'Discount Rate', 'Periods']);

        if (controlErrors.length > 0) {
            this.bsModalRef = showMessageBox(this.modalService, this.bsModalRef, 'Error', controlErrors);
            return;
        }

        this.isLoading = true;
        const pvFormValues = this.getPvFormValues();

        this.serviceSubscription = this.financialService.getPresentValueMulti(pvFormValues).subscribe(
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

        this.pvForm.reset({
            futureValue: 1000,
            discountRate: 10,
            periods: 10,
            compountInterval: 1
        });
    }
}
