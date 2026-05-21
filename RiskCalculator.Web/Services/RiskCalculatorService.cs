using RiskCalculator.Web.Models;
using System;

namespace RiskCalculator.Web.Services
{
    public class RiskCalculatorService : IRiskCalculatorService
    {
        public RiskResult Calculate(decimal accountSize, decimal riskPercent, decimal entryPrice, decimal stopPrice, decimal targetPrice, decimal brokerage)
        {
            RiskResult result = new RiskResult();
            result.RiskAmount = accountSize * (riskPercent / 100);
            var riskPerUnit = Math.Abs(entryPrice - stopPrice);

            if (riskPerUnit > 0)
            {
                result.PositionSize = (result.RiskAmount - 2 * brokerage) / riskPerUnit;
                result.Cost = result.PositionSize * entryPrice + brokerage;
                result.StopDistance = entryPrice - stopPrice;
                result.TargetDistance = targetPrice - entryPrice;
                result.SaleAmountIfStopHit = result.PositionSize * stopPrice - brokerage;
                result.LossIfStopHit = result.Cost - result.SaleAmountIfStopHit;
                result.SaleAmountIfTargetHit = result.PositionSize * targetPrice - brokerage;
                result.GainIfTargetHit = result.SaleAmountIfTargetHit - result.Cost;

                if (result.LossIfStopHit == 0)
                    result.RiskReward = 0;
                else
                    result.RiskReward = result.GainIfTargetHit / result.LossIfStopHit;

                if (result.RiskReward == -1)
                {
                    result.BreakEvenStrikeRate = 0;
                    result.KellyCriterionStrikeRate = 0;
                }
                else
                {
                    result.BreakEvenStrikeRate = 1 / (result.RiskReward + 1);
                    result.KellyCriterionStrikeRate = ((riskPercent / 100) * result.RiskReward + 1) / (result.RiskReward + 1);
                }

                if (accountSize == 0) 
                    result.CapAllocPercent = 0;
                else
                    result.CapAllocPercent = result.Cost / accountSize;


            }

            return result;
        }
    }
}