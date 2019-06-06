import { NetPresentValuePerRate } from './net-present-value-per-rate';
import { PeriodAmount } from './period-amount';

export interface NetPresentValueResponse {
    netPresentValueId: number;
    name: string;
    initialInvestment: number;
    lowerBoundDiscountRate: number;
    upperBoundDiscountRate: number;
    discountRateIncrement: number;
    cashFlows: PeriodAmount[];
    results: NetPresentValuePerRate[];
}
