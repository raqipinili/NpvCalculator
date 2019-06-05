import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterRequest } from 'src/app/_models/register-request';
import { showMessageBox } from 'src/app/_helpers/helper-functions';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    @Output()
    cancelRegister = new EventEmitter<boolean>();
    registerForm: FormGroup;
    submitted = false;
    bsModalRef: BsModalRef;

    constructor(
        private formBuilder: FormBuilder,
        private authService: AuthService,
        private modalService: BsModalService,
        private route: Router) {
    }

    ngOnInit() {
        this.registerForm = this.formBuilder.group({
            firstname: ['', Validators.required],
            lastname: ['', Validators.required],
            username: ['', Validators.required],
            password: ['', [Validators.required, Validators.minLength(4)]],
            confirmPassword: ['', [Validators.required, Validators.minLength(4)]]
        }, { validator: this.passwordValidator('password', 'confirmPassword') });
    }

    passwordValidator(controlName1: string, controlName2: string) {
        return (formGroup: FormGroup) => {
            const control1 = formGroup.get(controlName1);
            const control2 = formGroup.get(controlName2);

            if (control2.errors && !control2.errors.mustMatch) {
                // return if another validator has already found an error on the control2
                return;
            }

            // set error on control2 if validation fails
            if (control1.value !== control2.value) {
                control2.setErrors({ mustMatch: true });
            } else {
                control2.setErrors(null);
            }
        };
    }

    register() {
        this.submitted = true;

        if (!this.registerForm.valid) {
            return;
        }

        const register: RegisterRequest = this.registerForm.getRawValue() as RegisterRequest;
        this.authService.register(register).subscribe((result: number) => {
            console.log(result, 'Success: Register');
            this.bsModalRef = showMessageBox(this.modalService, this.bsModalRef, 'Success', ['Registration successful']);
            this.route.navigate(['/login']);
        }, error => {
            console.log(error);
            this.bsModalRef = showMessageBox(this.modalService, this.bsModalRef, 'Error', [`${error.status} - ${error.statusText}`]);
        });
    }

    cancel() {
        this.route.navigate(['/login']);
    }
}
