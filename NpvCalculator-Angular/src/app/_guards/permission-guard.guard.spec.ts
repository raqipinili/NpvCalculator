import { TestBed, async, inject } from '@angular/core/testing';

import { PermissionGuard } from './permission.guard';

describe('PermissionGuardGuard', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [PermissionGuard]
        });
    });

    it('should ...', inject([PermissionGuard], (guard: PermissionGuard) => {
        expect(guard).toBeTruthy();
    }));
});
