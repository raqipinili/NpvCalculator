import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ChartListModule } from './_components/charts/chart-list.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './_components/home/home.component';
import { PageNotFoundComponent } from './_components/page-not-found/page-not-found.component';
import { NetPresentValueComponent } from './_components/net-present-value/net-present-value.component';
import { PresentValueComponent } from './_components/present-value/present-value.component';
import { FutureValueComponent } from './_components/future-value/future-value.component';
import { LoginComponent } from './_components/auth/login/login.component';
import { RegisterComponent } from './_components/auth/register/register.component';
import { MessageBoxComponent } from './_components/message-box/message-box.component';

import { AuthGuard } from './_guards/auth.guard';
import { PermissionGuard } from './_guards/permission.guard';
import { FinancialService } from './_services/financial.service';
import { AuthService } from './_services/auth.service';


// import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
// import { library, dom } from '@fortawesome/fontawesome-svg-core';
// import { faTimesCircle } from '@fortawesome/free-regular-svg-icons';
export const tokenGetter = () => localStorage.getItem('token');

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        PageNotFoundComponent,
        NetPresentValueComponent,
        PresentValueComponent,
        FutureValueComponent,
        LoginComponent,
        RegisterComponent,
        MessageBoxComponent,
    ],
    imports: [
        AppRoutingModule,
        BrowserModule,
        HttpClientModule,
        ReactiveFormsModule,
        JwtModule.forRoot({
            config: {
                tokenGetter,
                whitelistedDomains: ['localhost:5001', 'localhost:5000'],
                blacklistedRoutes: ['localhost:5001/api/auth', 'localhost:5000/api/auth']
            }
        }),
        NgxDatatableModule,
        BsDropdownModule.forRoot(),
        ModalModule.forRoot(),
        TabsModule.forRoot(),
        ChartListModule,
        // FontAwesomeModule
    ],
    providers: [
        AuthGuard,
        PermissionGuard,
        AuthService,
        FinancialService,
        // { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true, }
    ],
    bootstrap: [
        AppComponent
    ],
    entryComponents: [
        MessageBoxComponent
    ]
})
export class AppModule {
    constructor() {
        // library.add(faTimesCircle);
        // dom.watch();
    }
}
