namespace Application.UseCases.Rental.RequestMotorcycleRentalClosure.Outputs
{
    public class FinancialOutput
    {
        public DateTime InitialDate { get; set; }
        public DateTime ExpectedDevolutionDate { get; set; }
        public DateTime DevolutionDate { get; set; }
        public double ExpectedTotalValue { get; set; }
        public double TotalValue { get; set; }
        public FineOutput Fine { get; set; }
    }
}
