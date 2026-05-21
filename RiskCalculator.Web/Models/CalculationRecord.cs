namespace RiskCalculator.Web.Models
{
    public class CalculationRecord
    {
        public DateTime Timestamp { get; set; }

        public decimal AccountSize { get; set; }

        public decimal RiskPercent { get; set; }

        public decimal EntryPrice { get; set; }

        public decimal StopPrice { get; set; }
        public decimal TargetPrice { get; set; }
        public decimal Brokerage { get; set; }

        public decimal PositionSize { get; set; }
        public decimal RiskAmount { get; set; }
        public decimal GainIfTargetHit { get; set; }

        public decimal Cost { get; set; }
        public decimal RiskReward { get; set; }
        public decimal BreakEvenStrikeRate { get; set; }
        public decimal KellyCriterionStrikeRate { get; set; }
        public decimal CapAllocPercent { get; set; }

    }
}
