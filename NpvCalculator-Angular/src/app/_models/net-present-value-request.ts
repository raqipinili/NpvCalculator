import { CashFlow } from './cash-flow';

export interface NetPresentValueRequest {
    initialInvestment: number;
    lowerBoundDiscountRate: number;
    upperBoundDiscountRate: number;
    discountRateIncrement: number;
    cashFlows: CashFlow[];
}
