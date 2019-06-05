import { NetPresentValuePerRate } from './net-present-value-per-rate';
import { CashFlow } from './cash-flow';

export interface NetPresentValueResponse {
    netPresentValueId: number;
    name: string;
    initialInvestment: number;
    lowerBoundDiscountRate: number;
    upperBoundDiscountRate: number;
    discountRateIncrement: number;
    cashFlows: CashFlow[];
    results: NetPresentValuePerRate[];
}
