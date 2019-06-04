import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-message-box',
    templateUrl: './message-box.component.html',
    styleUrls: ['./message-box.component.css']
})
export class MessageBoxComponent implements OnInit {
    title: string;
    list: any[] = [];

    constructor(public bsModalRef: BsModalRef) {
    }

    ngOnInit() {
    }
}
