using Application.Common;
using Domain.Enums;

namespace Application.UseCases.Rental.RequestMotorcycleRentalClosure.Outputs
{
    public class RequestMotorcycleRentalClosureOutput : Output
    {
        public Guid Id { get; set; }
        public Guid RenterId { get; set; }
        public Guid MotorcycleId { get; set; }
        public string PlanType { get; set; }
        public DateTime ContractDate { get; set; }
        public FinancialOutput FinancialOutput { get; set; }
    }
}
