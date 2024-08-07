using Domain.Enums;

namespace Application.UseCases.Rental.RequestMotorcycleRentalClosure.Outputs
{
    public class FineOutput
    {
        public FineType Type { get; set; }
        public double Value { get; set; }
        public int DaysOverdue { get; set; }
    }
}
