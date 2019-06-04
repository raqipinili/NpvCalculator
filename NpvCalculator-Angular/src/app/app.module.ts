import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { TabsModule } from 'ngx-bootstrap/tabs';

import { AppComponent } from './app.component';
import { NetPresentValueComponent } from './_components/net-present-value/net-present-value.component';
import { PageNotFoundComponent } from './_components/page-not-found/page-not-found.component';

import { FinancialService } from './_services/financial.service';
import { ChartListModule } from './_components/charts/chart-list.module';

// import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
// import { library, dom } from '@fortawesome/fontawesome-svg-core';
// import { faTimesCircle } from '@fortawesome/free-regular-svg-icons';

@NgModule({
    declarations: [
        AppComponent,
        NetPresentValueComponent,
        PageNotFoundComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        ReactiveFormsModule,
        NgxDatatableModule,
        TabsModule.forRoot(),
        ChartListModule
        // FontAwesomeModule
    ],
    providers: [
        FinancialService
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule {
    constructor() {
        // library.add(faTimesCircle);
        // dom.watch();
    }
}
