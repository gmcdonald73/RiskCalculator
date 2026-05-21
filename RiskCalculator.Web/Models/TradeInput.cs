using System.ComponentModel.DataAnnotations;

namespace RiskCalculator.Web.Models
{
    public class TradeInput
    {
        [Display(Name = "Account Size")]
        [Range(1, double.MaxValue, ErrorMessage = "{0} must be be at least {1}")]
        public decimal AccountSize { get; set; }

        [Display(Name = "Risk %")]
        [Range(0.01, 100, ErrorMessage = "{0} must be between {1} and {2}")]
        public decimal RiskPercent { get; set; }

        [Display(Name = "Entry Price")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "{0} must be be at least {1}")]
        public decimal EntryPrice { get; set; }

        [Display(Name = "Stop Price")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "{0} must be be at least {1}")]
        public decimal StopPrice { get; set; }

        [Display(Name = "Target Price")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "{0} must be be at least {1}")]
        public decimal TargetPrice { get; set; }

        [Display(Name = "Brokerage")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} cannot be negative")]
        public decimal Brokerage { get; set; }
    }
}
