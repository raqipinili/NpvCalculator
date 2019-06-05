import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './_services/auth.service';
import { showMessageBox } from 'src/app/_helpers/helper-functions';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';


@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    bsModalRef: BsModalRef;

    get username() {
        return this.authService.username;
    }

    constructor(
        private router: Router,
        private authService: AuthService,
        private modalService: BsModalService) {

    }

    loggedIn() {
        return this.authService.loggedIn();
    }

    logout() {
        this.authService.logout(() => {
            this.bsModalRef = showMessageBox(this.modalService, this.bsModalRef, 'Logout', ['Logged out']);
            this.router.navigate(['/login']);
        });
    }
}
