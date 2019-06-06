import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap/modal';
import { showMessageBox } from 'src/app/_helpers/helper-functions';
import { AuthService } from 'src/app/_services/auth.service';
import { RegisterRequest } from 'src/app/_models/register-request';
import { Permissions } from 'src/app/_shared/permissions.enum';



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
            confirmPassword: ['', [Validators.required, Validators.minLength(4)]],
            npvPermission: [false],
            pvPermission: [false],
            fvPermission: [false]
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

        const formValue = this.registerForm.getRawValue();
        const permissions = [];

        // tslint:disable-next-line: curly
        if (formValue.npvPermission) permissions.push(Permissions.NetPresentValue);
        // tslint:disable-next-line: curly
        if (formValue.pvPermission) permissions.push(Permissions.PresentValue);
        // tslint:disable-next-line: curly
        if (formValue.fvPermission) permissions.push(Permissions.FutureValue);

        const register: RegisterRequest = {
            firstname: formValue.firstname,
            lastname: formValue.lastname,
            username: formValue.username,
            password: formValue.password,
            permissions
        };

        this.authService.register(register).subscribe((result: number) => {
            console.log(result, 'Success: Register');
            showMessageBox(this.modalService, 'Success', ['Registration successful']);
            this.route.navigate(['/login']);
        }, error => {
            console.log(error);
            showMessageBox(this.modalService, 'Error', [`${error.status} - ${error.statusText}`]);
        });
    }

    cancel() {
        this.route.navigate(['/login']);
    }
}
