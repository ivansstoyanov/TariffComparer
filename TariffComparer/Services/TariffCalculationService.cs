using System;
using TariffComparer.Enumerations;
using TariffComparer.Models;

namespace TariffComparer.Services
{
    public static class TariffCalculationService
    {
        public static decimal CalculateAnnualTariff(decimal consumption, CalculationModel calculationModel)
        {
            return CalculateTariffForYears(1, consumption, calculationModel);
        }

        public static decimal CalculateTariffForYears(int year, decimal consumption, CalculationModel calculationModel)
        {
            switch (calculationModel.Type)
            {
                case CalculationModelEnum.Basic:
                    return CalculateBasic(year, consumption, calculationModel);
                case CalculationModelEnum.Package:
                    return CalculatePackage(year, consumption, calculationModel);
                default:
                    throw new NotImplementedException();
            }
        }

        private static decimal CalculateBasic(int year, decimal consumption, CalculationModel calculationModel)
        {
            decimal baseCost = GetBaseCost(year, calculationModel);

            decimal basicConsumption = GetBasicConsumption(consumption, calculationModel.ConsumptionCost);

            return baseCost + basicConsumption;
        }

        private static decimal CalculatePackage(int year, decimal consumption, CalculationModel calculationModel)
        {
            decimal result = GetBaseCost(year, calculationModel);

            result += GetPackageConsumption(consumption, calculationModel);

            return result;
        }

        private static decimal GetBaseCost(int year, CalculationModel calculationModel)
        {
            decimal yearCost = 0;
            switch (calculationModel.PeriodCost)
            {
                case CalculationModelPeriodCostEnum.Month:
                    yearCost = calculationModel.BaseCost * 12;
                    break;
                case CalculationModelPeriodCostEnum.Year:
                    yearCost = calculationModel.BaseCost;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return yearCost * year;
        }

        private static decimal GetBasicConsumption(decimal consumption, decimal consumptionCost)
        {
            return consumption * consumptionCost;
        }

        private static decimal GetPackageConsumption(decimal consumption, CalculationModel calculationModel)
        {
            decimal result = 0m;

            if (calculationModel.ConsumptionCostStart < consumption)
            {
                decimal difference = consumption - calculationModel.ConsumptionCostStart;

                result = difference * calculationModel.ConsumptionCost;
            }

            return result;
        }
    }
}
