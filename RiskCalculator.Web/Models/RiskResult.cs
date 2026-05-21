namespace RiskCalculator.Web.Models
{
    public class RiskResult
    {
        public decimal RiskAmount { get; set; }

        public decimal StopDistance { get; set; }

        public decimal TargetDistance { get; set; }
        public decimal PositionSize { get; set; }

        public decimal Cost { get; set; }

        public decimal SaleAmountIfStopHit { get; set; }
        public decimal LossIfStopHit { get; set; }
        public decimal SaleAmountIfTargetHit { get; set; }
        public decimal GainIfTargetHit { get; set; }
        public decimal RiskReward { get; set; }
        public decimal BreakEvenStrikeRate { get; set; }
        public decimal CapAllocPercent { get; set; }
        public decimal KellyCriterionStrikeRate { get; set; }
    }
}
