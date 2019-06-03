import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NetPresentValueComponent } from './_components/net-present-value/net-present-value.component';
import { PageNotFoundComponent } from './_components/page-not-found/page-not-found.component';

const routes: Routes = [
    { path: '', redirectTo: 'net-present-value', pathMatch: 'full' },
    { path: 'home', redirectTo: 'net-present-value', pathMatch: 'full' },
    { path: 'net-present-value', component: NetPresentValueComponent },
    { path: 'page-not-found', component: PageNotFoundComponent },
    { path: '**', redirectTo: 'page-not-found', pathMatch: 'full' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
