import { PeriodAmount } from './period-amount';

export interface NetPresentValueRequest {
    netPresentValueId: number;
    name: string;
    initialInvestment: number;
    lowerBoundDiscountRate: number;
    upperBoundDiscountRate: number;
    discountRateIncrement: number;
    cashFlows: PeriodAmount[];
}
