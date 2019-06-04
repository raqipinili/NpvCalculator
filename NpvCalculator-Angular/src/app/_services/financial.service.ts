import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { NetPresentValueRequest } from '../_models/net-present-value-request';
import { PresentValueRequest } from '../_models/present-value-request';
import { FutureValueRequest } from '../_models/future-value-request';
import { NetPresentValue } from '../_models/net-present-value';
import { PeriodAmount } from '../_models/period-amount';


@Injectable({
    providedIn: 'root'
})
export class FinancialService {
    baseUrl = environment.apiUrl.concat('financial');

    constructor(private http: HttpClient) { }

    getNetPresentValueDynamicRate(npvRequest: NetPresentValueRequest): Observable<NetPresentValue[]> {
        return this.http.post<NetPresentValue[]>(this.baseUrl + '/npv/dynamicrate', npvRequest);
    }

    getPresentValueMulti(pvRequest: PresentValueRequest): Observable<PeriodAmount[]> {
        return this.http.post<PeriodAmount[]>(this.baseUrl + '/pv/multi', pvRequest);
    }

    getFutureValueMulti(fvRequest: FutureValueRequest): Observable<PeriodAmount[]> {
        return this.http.post<PeriodAmount[]>(this.baseUrl + '/fv/multi', fvRequest);
    }
}
