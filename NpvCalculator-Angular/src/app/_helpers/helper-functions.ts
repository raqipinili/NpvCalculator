import { AbstractControl } from '@angular/forms';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { MessageBoxComponent } from '../_components/message-box/message-box.component';

export function getControlError(control: AbstractControl, key: string, displayName): string[] {
    const result: string[] = [];
    const errors = control.get(key).errors;

    if (errors && errors.required) {
        result.push(`${displayName} is required`);
    }

    return result;
}

export function getAllControlErrors(control: AbstractControl, controlNames: string[], displayNames: string[]): string[] {
    const result: string[] = [];

    if (controlNames && displayNames) {
        controlNames.forEach((name, index) => result.push(...getControlError(control, name, displayNames[index])));
    }

    return result;
}

export function showMessageBox(modalService: BsModalService, modalRef: BsModalRef, title: string, message: string[]): BsModalRef {
    return modalService.show(MessageBoxComponent, {
        initialState: {
            list: message,
            title
        }
    });
}
