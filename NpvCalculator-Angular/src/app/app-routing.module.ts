import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './_components/home/home.component';
import { PageNotFoundComponent } from './_components/page-not-found/page-not-found.component';
import { NetPresentValueComponent } from './_components/net-present-value/net-present-value.component';
import { PresentValueComponent } from './_components/present-value/present-value.component';
import { FutureValueComponent } from './_components/future-value/future-value.component';
import { LoginComponent } from './_components/auth/login/login.component';
import { RegisterComponent } from './_components/auth/register/register.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [{
            path: 'net-present-value',
            component: NetPresentValueComponent,
            // resolve: { data: [] }
        },
        {
            path: 'present-value',
            component: PresentValueComponent
        },
        {
            path: 'future-value',
            component: FutureValueComponent
        }]
    },
    { path: 'home', component: HomeComponent, runGuardsAndResolvers: 'always', canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'page-not-found', component: PageNotFoundComponent },
    { path: '**', redirectTo: 'page-not-found', pathMatch: 'full' }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }
