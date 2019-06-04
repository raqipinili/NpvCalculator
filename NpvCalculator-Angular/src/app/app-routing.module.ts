import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NetPresentValueComponent } from './_components/net-present-value/net-present-value.component';
import { PageNotFoundComponent } from './_components/page-not-found/page-not-found.component';
import { HomeComponent } from './_components/home/home.component';
import { FutureValueComponent } from './_components/future-value/future-value.component';
import { PresentValueComponent } from './_components/present-value/present-value.component';

const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'net-present-value', component: NetPresentValueComponent },
    { path: 'present-value', component: PresentValueComponent },
    { path: 'future-value', component: FutureValueComponent },
    { path: 'page-not-found', component: PageNotFoundComponent },
    { path: '**', redirectTo: 'page-not-found', pathMatch: 'full' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
