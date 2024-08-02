namespace Domain.Entities
{
    public class Motorcycle
    {
        public Guid Id { get; private set; }
        public int Year { get; private set; }
        public string Model { get; private set; }
        public string Plate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Motorcycle(Guid id, 
            int year, 
            string model, 
            string plate, 
            DateTime? createdAt = null, 
            DateTime? updatedAt = null)
        {
            Id = id;
            Year = year;
            Model = model;
            Plate = plate;
            CreatedAt = createdAt ?? DateTime.Now;
            UpdatedAt = updatedAt;
        }

    }
}
