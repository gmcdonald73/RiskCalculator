using RiskCalculator.Web.Models;

namespace RiskCalculator.Web.Services
{
    public interface IRiskCalculatorService
    {
        
        RiskResult Calculate(decimal accountSize, decimal riskPercent, decimal entryPrice, decimal stopPrice, decimal targetPrice, decimal brokerage);
    }
}
