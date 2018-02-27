using TariffComparer.Enumerations;

namespace TariffComparer.Models
{
    public class CalculationModel
    {
        public CalculationModelEnum Type { get; set; }

        public decimal BaseCost { get; set; }

        public CalculationModelPeriodCostEnum PeriodCost { get; set; }

        public decimal ConsumptionCost { get; set; }

        public decimal ConsumptionCostStart { get; set; }
    }
}
