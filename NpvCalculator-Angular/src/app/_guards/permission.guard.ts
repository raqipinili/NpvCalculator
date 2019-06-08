import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { BsModalService } from 'ngx-bootstrap/modal';
import { AuthService } from '../_services/auth.service';
import { showMessageBox } from '../_helpers/helper-functions';

@Injectable({ providedIn: 'root' })
export class PermissionGuard implements CanActivate {
    constructor(
        private authService: AuthService,
        private modalService: BsModalService,
        private router: Router) {
    }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        // const route = next.children[0];
        let canActivate = false;

        if (next.data && next.data.permissions) {
            const permissions = Object.assign([], this.authService.permissions);
            canActivate = next.data.permissions.every((p1: number) => permissions.some(p2 => p1 === p2));
        }

        if (!canActivate) {
            showMessageBox(this.modalService, 'Error', ['Access denied!']);
            this.router.navigate(['/home']);
            return false;
        }

        return true;
    }
}
