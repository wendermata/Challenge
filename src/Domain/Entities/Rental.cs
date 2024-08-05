using Domain.Enums;

namespace Domain.Entities
{
    public class Rental
    {
        public Guid Id { get; private set; }
        public Guid RenterId { get; private set; }
        public Guid MotorcycleId { get; private set; }
        public PlanType PlanType { get; private set; }
        public DateTime InitialDate { get; private set; }

        public DateTime ExpectedDevolutionDate { get; private set; }
        public double ExpectedTotalValue { get; private set; }

        public DateTime? DevolutionDate { get; private set; }
        public double? TotalValue { get; private set; }
        public bool IsFinished { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }


        public Rental(Guid id,
            Guid renterId,
            Guid motorcycleId,
            PlanType planType,
            DateTime initialDate,
            DateTime? devolutionDate = null,
            double? totalValue = null,
            bool? isFinished = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null)
        {
            Id = id;
            RenterId = renterId;
            MotorcycleId = motorcycleId;
            PlanType = planType;
            InitialDate = initialDate.AddDays(1);

            ExpectedTotalValue = CalculateExpectedValue(planType);
            ExpectedDevolutionDate = CalculateExpectedDevolutionDate(planType, initialDate);

            DevolutionDate = devolutionDate;
            TotalValue = totalValue;
            IsFinished = isFinished ?? false;

            CreatedAt = createdAt ?? DateTime.Now;
            UpdatedAt = updatedAt;
        }

        public void FinishRental(DateTime date)
        {
            IsFinished = true;
            UpdatedAt = DateTime.Now;
            DevolutionDate = date;
            TotalValue = CalculateTotalValue();
        }

        private DateTime CalculateExpectedDevolutionDate(PlanType type, DateTime initialDate)
        {
            return type switch
            {
                PlanType.SevenDays => initialDate.Date.AddDays(7),
                PlanType.FifteenDays => initialDate.Date.AddDays(15),
                PlanType.ThirtyDays => initialDate.Date.AddDays(30),
                PlanType.FortyFiveDays => initialDate.Date.AddDays(45),
                PlanType.FiftyDays => initialDate.Date.AddDays(50),
                _ => initialDate,
            };
        }

        private double CalculateExpectedValue(PlanType planType)
        {
            return planType switch
            {
                PlanType.SevenDays => 30.00 * 7,
                PlanType.FifteenDays => 28.00 * 15,
                PlanType.ThirtyDays => 22.00 * 30,
                PlanType.FortyFiveDays => 20.00 * 45,
                PlanType.FiftyDays => 18.00 * 50,
                _ => 0
            };
        }

        public double CalculateTotalValue()
        {
            if (DevolutionDate.Value.Date < ExpectedDevolutionDate.Date)
                return CalculateEarlyReturnFine();

            if (DevolutionDate.Value.Date > ExpectedDevolutionDate.Date)
                return CalculateLateReturnFine();

            return ExpectedTotalValue;
        }

        private double CalculateEarlyReturnFine()
        {
            var daysDiff = (ExpectedDevolutionDate.Date - DevolutionDate.Value.Date).Days;
            var usedDaysValue = CalculateUsedDaysValue();

            return PlanType switch
            {
                PlanType.SevenDays => usedDaysValue + (daysDiff * 0.2 * 30.00),
                PlanType.FifteenDays => usedDaysValue + (daysDiff * 0.4 * 28.00),
                PlanType.ThirtyDays => ExpectedTotalValue,
                PlanType.FortyFiveDays => ExpectedTotalValue,
                PlanType.FiftyDays => ExpectedTotalValue,
                _ => 0
            };
        }

        private double CalculateLateReturnFine()
        {
            var daysDiff = (DevolutionDate.Value.Date - ExpectedDevolutionDate.Date).Days;
            return PlanType switch
            {
                PlanType.SevenDays => ExpectedTotalValue + (daysDiff * 50.00),
                PlanType.FifteenDays => ExpectedTotalValue + (daysDiff * 50.00),
                PlanType.ThirtyDays => ExpectedTotalValue + (daysDiff * 50.00),
                PlanType.FortyFiveDays => ExpectedTotalValue + (daysDiff * 50.00),
                PlanType.FiftyDays => ExpectedTotalValue + (daysDiff * 50.00),
                _ => 0
            };
        }

        private double CalculateUsedDaysValue()
        {
            var usedDays = (DevolutionDate.Value.Date - InitialDate.Date).Days + 1;
            var dailyValue = PlanType switch
            {
                PlanType.SevenDays => 30.00,
                PlanType.FifteenDays => 28.00,
                PlanType.ThirtyDays => 22.00,
                PlanType.FortyFiveDays => 20.00,
                PlanType.FiftyDays => 18.00,
                _ => 0
            };

            return dailyValue * usedDays;
        }
    }
}
