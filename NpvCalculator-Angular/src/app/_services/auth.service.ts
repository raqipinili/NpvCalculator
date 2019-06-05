import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { RegisterRequest } from '../_models/register-request';
import { LoginRequest } from '../_models/login-request';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    jwtHelper: JwtHelperService;
    baseUrl = environment.apiUrl.concat('auth/');
    decodedToken: any;

    get username(): string {
        if (this.loggedIn()) {
            const token: string = localStorage.getItem('token');
            const decodedToken = this.jwtHelper.decodeToken(token);
            return decodedToken.unique_name;
        }

        return null;
    }

    get userId(): number {
        if (this.loggedIn()) {
            const token: string = localStorage.getItem('token');
            const decodedToken = this.jwtHelper.decodeToken(token);
            return decodedToken.nameid;
        }

        return null;
    }

    constructor(private http: HttpClient) {
        this.jwtHelper = new JwtHelperService();
    }

    login(login: LoginRequest): Observable<any> {
        return this.http.post(this.baseUrl.concat('login'), login).pipe(
            map((response: any) => {
                if (response && response.token) {
                    localStorage.setItem('token', response.token);
                }
            })
        );
    }

    logout(fnCallback: (() => void) = null): void {
        localStorage.removeItem('token');

        if (fnCallback) {
            fnCallback();
        }
    }

    register(register: RegisterRequest): Observable<number> {
        return this.http.post<number>(this.baseUrl.concat('register'), register);
    }

    loggedIn(): boolean {
        const token: string = localStorage.getItem('token');
        return !this.jwtHelper.isTokenExpired(token);
    }
}
