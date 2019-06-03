import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { NetPresentValueRequest } from '../_models/net-present-value-request';
import { NetPresentValueResponse } from '../_models/net-present-value-response';
import { CashFlow } from '../_models/cash-flow';



@Injectable({
    providedIn: 'root'
})
export class FinancialService {
    baseUrl = environment.apiUrl.concat('financial');

    constructor(private http: HttpClient) { }

    getNetPresentValueDynamicRate(npvRequest: NetPresentValueRequest): Observable<NetPresentValueResponse> {
        return this.http.post<NetPresentValueResponse>(this.baseUrl + '/npv2', npvRequest);
    }
}
