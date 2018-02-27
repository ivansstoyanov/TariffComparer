using System;
using System.Collections.Generic;
using System.Linq;
using TariffComparer.Enumerations;
using TariffComparer.Models;
using TariffComparer.Services;

namespace TariffComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tariff> tariffPackages = InputSamplePackages();

            decimal consumption = 0m;
            var input = Console.ReadLine();
            if (decimal.TryParse(input, out consumption))
            {
                List<TariffResult> result = CalculateTariffResult(consumption, tariffPackages);

                PrintResult(result);
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }            
        }


        static List<TariffResult> CalculateTariffResult(decimal consumption, List<Tariff> tariffPackages)
        {
            List<TariffResult> result = new List<TariffResult>();
            tariffPackages.ForEach(
                p => result.Add(new TariffResult()
                {
                    Name = p.Name,
                    AnnualCost = TariffCalculationService.CalculateAnnualTariff(consumption, p.CalculationModel)
                })
            );
            result = result.OrderBy(r => r.AnnualCost).ToList();

            return result;
        }

        static List<Tariff> InputSamplePackages()
        {
            return new List<Tariff>()
            {
                new Tariff()
                {
                    Name = "Basic Tariff",
                    CalculationModel = new CalculationModel()
                    {
                        Type = CalculationModelEnum.Basic,
                        BaseCost = 5m,
                        PeriodCost = CalculationModelPeriodCostEnum.Month,
                        ConsumptionCost = 0.22m,
                        ConsumptionCostStart = 0
                    }
                },
                new Tariff()
                {
                    Name = "Packaged Tariff",
                    CalculationModel = new CalculationModel()
                    {
                        Type = CalculationModelEnum.Package,
                        BaseCost = 800m,
                        PeriodCost = CalculationModelPeriodCostEnum.Year,
                        ConsumptionCost = 0.30m,
                        ConsumptionCostStart = 4000
                    }
                }
            };
        }

        static void PrintResult(List<TariffResult> tariffResult)
        {
            foreach (var tariff in tariffResult)
            {
                Console.WriteLine(tariff.Name);
                Console.WriteLine(tariff.AnnualCost);
                Console.WriteLine();
            }
        }
    }
}
