import { TestBed } from '@angular/core/testing';

import { FinancialService } from './financial.service';

describe('FinancialServiceService', () => {
    beforeEach(() => TestBed.configureTestingModule({}));

    it('should be created', () => {
        const service: FinancialService = TestBed.get(FinancialService);
        expect(service).toBeTruthy();
    });
});
