import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap/modal';
import { showMessageBox } from 'src/app/_helpers/helper-functions';
import { AuthService } from 'src/app/_services/auth.service';
import { LoginRequest } from 'src/app/_models/login-request';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    submitted: boolean;

    get form() {
        return this.loginForm.controls;
    }

    constructor(
        private formBuilder: FormBuilder,
        private authService: AuthService,
        private modalService: BsModalService,
        private router: Router) { }

    ngOnInit() {
        this.submitted = false;

        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', [Validators.required, Validators.minLength(4)]]
        });
    }

    login() {
        this.submitted = true;

        if (this.loginForm.invalid) {
            return;
        }

        const login: LoginRequest = {
            username: this.loginForm.get('username').value,
            password: this.loginForm.get('password').value
        };

        this.authService.login(login).subscribe(() => {
            console.log('Login successful');
        }, error => {
            showMessageBox(this.modalService, 'Error', [`${error.status} - ${error.statusText}`]);
        }, () => {
            this.router.navigate(['/home']);
        });
    }
}
