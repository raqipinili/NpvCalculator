import { PeriodAmount } from './period-amount';

export interface NetPresentValueRequest {
    initialInvestment: number;
    lowerBoundDiscountRate: number;
    upperBoundDiscountRate: number;
    discountRateIncrement: number;
    cashFlows: PeriodAmount[];
}
