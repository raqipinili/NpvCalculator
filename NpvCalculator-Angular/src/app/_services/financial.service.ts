import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { NetPresentValueRequest } from '../_models/net-present-value-request';
import { PresentValueRequest } from '../_models/present-value-request';
import { FutureValueRequest } from '../_models/future-value-request';
import { NetPresentValuePerRate } from '../_models/net-present-value-per-rate';
import { PeriodAmount } from '../_models/period-amount';
import { NetPresentValueResponse } from '../_models/net-present-value-response';


@Injectable({
    providedIn: 'root'
})
export class FinancialService {
    baseUrl = environment.apiUrl.concat('financial');

    constructor(private http: HttpClient) { }

    getNetPresentValueDynamicRate(request: NetPresentValueRequest): Observable<NetPresentValuePerRate[]> {
        return this.http.post<NetPresentValuePerRate[]>(this.baseUrl + '/npv/dynamicrate', request);
    }

    getPresentValueMulti(request: PresentValueRequest): Observable<PeriodAmount[]> {
        return this.http.post<PeriodAmount[]>(this.baseUrl + '/pv/multi', request);
    }

    getFutureValueMulti(request: FutureValueRequest): Observable<PeriodAmount[]> {
        return this.http.post<PeriodAmount[]>(this.baseUrl + '/fv/multi', request);
    }

    getNetPresentValueLatest(): Observable<NetPresentValueResponse> {
        return this.http.get<NetPresentValueResponse>(this.baseUrl + '/npv/getlatest');
    }
}
