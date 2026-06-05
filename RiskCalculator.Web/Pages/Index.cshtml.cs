using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiskCalculator.Web.Models;
using RiskCalculator.Web.Services;
using System.ComponentModel.DataAnnotations;
using RiskCalculator.Web.Extensions;

namespace RiskCalculator.Web.Pages
{
    public class RiskCalculatorModel : PageModel
    {
        [BindProperty]
        public TradeInput Input { get; set; } = new();

        public RiskResult? Result { get; set; }

        public string PositionSizeCssClass
        {
            get
            {
                if (Result == null)
                    return "";

                if (Result.PositionSize < 0)
                    return "text-danger fw-bold";

                return "";
            }
        }
        public string CostCssClass
        {
            get
            {
                if (Result == null)
                    return "";

                if (Result.CapAllocPercent > 1)
                    return "text-danger fw-bold";

                if (Result.CapAllocPercent > 0.5m)
                    return "text-warning";

                return "";
            }
        }

        public string CapAllocPercentCssClass
        {
            get
            {
                if (Result == null)
                    return "";

                if (Result.CapAllocPercent > 1)
                    return "text-danger fw-bold";

                if (Result.CapAllocPercent > 0.5m)
                    return "text-warning";

                return "";
            }
        }
        public string SaleAmountIfStopHitCssClass
        {
            get
            {
                if (Result == null)
                    return "";

                if (Result.SaleAmountIfStopHit < 0)
                    return "text-danger fw-bold";

                return "";
            }
        }
        public string LossIfStopHitCssClass
        {
            get
            {
                return "";
            }
        }
        public string SaleAmountIfTargetHitCssClass
        {
            get
            {
                if (Result == null)
                    return "";

                if (Result.SaleAmountIfTargetHit < 0)
                    return "text-danger fw-bold";

                return "";
            }
        }
        public string GainIfTargetHitCssClass
        {
            get
            {
                if (Result == null)
                    return "";

                if (Result.GainIfTargetHit < 0)
                    return "text-danger fw-bold";

                return "";
            }
        }

        public string RiskRewardCssClass
        {
            get
            {
                if (Result == null)
                    return "";

                if (Result.RiskReward < 1)
                    return "text-danger fw-bold";

                if (Result.RiskReward < 2)
                    return "text-warning";

                return "";
            }
        }
        public string BreakEvenStrikeRateCssClass
        {
            get
            {
                if (Result == null)
                    return "";

                if (Result.BreakEvenStrikeRate < 0)
                    return "text-danger fw-bold";

                if (Result.BreakEvenStrikeRate > 1)
                    return "text-danger fw-bold";

                return "";
            }
        }
        public string KellyCriterionStrikeRateCssClass
        {
            get
            {
                if (Result == null)
                    return "";

                if (Result.KellyCriterionStrikeRate < 0)
                    return "text-danger fw-bold";

                if (Result.KellyCriterionStrikeRate > 1)
                    return "text-danger fw-bold";

                return "";
            }
        }

        public List<CalculationRecord> RecentCalculations { get; set; } = new();

        private readonly ILogger<RiskCalculatorModel> _logger;

        private readonly IRiskCalculatorService _calculator;

        public RiskCalculatorModel(ILogger<RiskCalculatorModel> logger,
                          IRiskCalculatorService calculator)
        {
            _logger = logger;
            _calculator = calculator;
        }

        public void OnGet()
        {
            RecentCalculations = LoadRecentCalculations();

            Input = new TradeInput
            {
                AccountSize = 10000,
                RiskPercent = 1,
                EntryPrice = 0.5M,
                StopPrice = 0.4M,
                TargetPrice = 0.7M,
                Brokerage = 0
            };
        }

        public void OnPost()
        {
            RecentCalculations = LoadRecentCalculations();

            if (!ModelState.IsValid)
                return;

            Result = _calculator.Calculate(
                Input.AccountSize,
                Input.RiskPercent,
                Input.EntryPrice,
                Input.StopPrice,
                Input.TargetPrice,
                Input.Brokerage);

            var record = new CalculationRecord
            {
                Timestamp = DateTime.Now,
                AccountSize = Input.AccountSize,
                RiskPercent = Input.RiskPercent,
                EntryPrice = Input.EntryPrice,
                StopPrice = Input.StopPrice,
                TargetPrice= Input.TargetPrice,
                Brokerage = Input.Brokerage,
                PositionSize = Result.PositionSize,
                RiskAmount = Result.RiskAmount,
                Cost = Result.Cost,
                CapAllocPercent = Result.CapAllocPercent,
                GainIfTargetHit = Result.GainIfTargetHit,
                RiskReward = Result.RiskReward,
                BreakEvenStrikeRate = Result.BreakEvenStrikeRate,
                KellyCriterionStrikeRate = Result.KellyCriterionStrikeRate
            };

            RecentCalculations.Insert(0, record);

            // keep only last 5
            if (RecentCalculations.Count > 5)
            {
                RecentCalculations.RemoveAt(5);
            }

            HttpContext.Session.SetObject(
                "RecentCalculations",
                RecentCalculations);
        }

        private List<CalculationRecord> LoadRecentCalculations()
        {
            return HttpContext.Session.GetObject<List<CalculationRecord>>("RecentCalculations")
                ?? new List<CalculationRecord>();
        }
    }
}
