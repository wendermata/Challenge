using Application.UseCases.Rental.RequestMotorcycleRentalClosure.Outputs;
using Domain.Enums;
using RentalDomain = Domain.Entities.Rental;

namespace Application.UseCases.Rental.RequestMotorcycleRentalClosure.Mapping
{
    public static class RequestMotorcycleRentalClosureOutputMapper
    {
        public static RequestMotorcycleRentalClosureOutput MapToOutput(this RentalDomain domain)
        {
            if (domain == null)
                return null;

            return new RequestMotorcycleRentalClosureOutput
            {
                Id = domain.Id,
                RenterId = domain.RenterId,
                MotorcycleId = domain.MotorcycleId,
                PlanType = domain.PlanType.ToString(),
                ContractDate = domain.CreatedAt,
                FinancialOutput = domain.MapToFinancialOutput(),
            };
        }

        public static FinancialOutput MapToFinancialOutput(this RentalDomain domain)
        {
            if (domain == null)
                return null;

            bool hasFine = domain.DevolutionDate!.Value != domain.ExpectedDevolutionDate
                || domain.TotalValue!.Value != domain.ExpectedTotalValue;

            return new FinancialOutput
            {
                InitialDate = domain.InitialDate,
                ExpectedDevolutionDate = domain.ExpectedDevolutionDate,
                DevolutionDate = domain.DevolutionDate!.Value,
                ExpectedTotalValue = domain.ExpectedTotalValue,
                TotalValue = domain.TotalValue!.Value,
                Fine = hasFine ? domain.MapToFineOutput() : new FineOutput()
            };
        }

        public static FineOutput MapToFineOutput(this RentalDomain domain)
        {
            if (domain == null)
                return null;

            var type = domain.DevolutionDate!.Value > domain.ExpectedDevolutionDate ? FineType.Late : FineType.Early;

            return new FineOutput
            {
                Type = type,
                DaysOverdue = type is FineType.Late ? (domain.DevolutionDate!.Value - domain.ExpectedDevolutionDate).Days : (domain.ExpectedDevolutionDate - domain.DevolutionDate!.Value).Days,
                Value = domain.TotalValue!.Value - domain.ExpectedTotalValue
            };
        }
    }
}
