import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { AuthService } from './_services/auth.service';
import { showMessageBox } from 'src/app/_helpers/helper-functions';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
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
            showMessageBox(this.modalService, 'Logout', ['Logged out']);
            this.router.navigate(['/login']);
        });
    }
}
