using RiskCalculator.Web.Services;

namespace RiskCalculator.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Calculate_ReturnsExpectedResults()
        {
            var service = new RiskCalculatorService();

            var result = service.Calculate(
                accountSize: 10000m,
                riskPercent: 1m,
                entryPrice: 100m,
                stopPrice: 95m,
                targetPrice: 120m,
                brokerage: 0m);

            // Assert
            Assert.Equal(100m, result.RiskAmount);
            Assert.Equal(5m, result.StopDistance);
            Assert.Equal(20m, result.TargetDistance);
            Assert.Equal(20m, result.PositionSize);
            Assert.Equal(2000m, result.Cost);
            Assert.Equal(1900m, result.SaleAmountIfStopHit);
            Assert.Equal(100m, result.LossIfStopHit);
            Assert.Equal(2400m, result.SaleAmountIfTargetHit);
            Assert.Equal(400m, result.GainIfTargetHit);
            Assert.Equal(0.208m, result.KellyCriterionStrikeRate);
            Assert.Equal(0.20m, result.CapAllocPercent);
            Assert.Equal(4m, result.RiskReward);
            Assert.Equal(0.20m, result.BreakEvenStrikeRate);
        }
    }
}