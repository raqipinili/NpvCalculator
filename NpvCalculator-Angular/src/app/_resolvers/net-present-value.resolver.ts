import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { FinancialService } from '../_services/financial.service';
import { NetPresentValueResponse } from '../_models/net-present-value-response';
import { showMessageBox } from 'src/app/_helpers/helper-functions';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';


@Injectable()
export class NetPresentValueResolver implements Resolve<NetPresentValueResponse> {
    bsModalRef: BsModalRef;

    constructor(
        private financialService: FinancialService,
        private modalService: BsModalService,
        private router: Router) {
    }

    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<NetPresentValueResponse> {
        return this.financialService.getNetPresentValueLatest().pipe(
            catchError(error => {
                console.log(error);
                this.bsModalRef = showMessageBox(this.modalService, this.bsModalRef, 'Error', ['Problem resolving data']);
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
