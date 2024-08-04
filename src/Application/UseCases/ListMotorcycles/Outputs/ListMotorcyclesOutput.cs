using Application.Common;

namespace Application.UseCases.ListMotorcycles.Outputs
{
    public class ListMotorcyclesOutput : Output
    {
        public List<MotorcycleOutput> Motorcycles { get; set; } = new();
    }
}
