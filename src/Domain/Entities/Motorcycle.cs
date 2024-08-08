using Domain.Exceptions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Motorcycle
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; private set; }
        public int Year { get; private set; }
        public string Model { get; private set; }
        public string Plate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public Motorcycle() { }
        public Motorcycle(Guid id,
            int year,
            string model,
            string plate,
            DateTime? createdAt = null,
            DateTime? updatedAt = null,
            bool? isActive = null)
        {
            Id = id;
            Year = year;
            Model = model;
            Plate = plate;
            CreatedAt = createdAt ?? DateTime.Now;
            UpdatedAt = updatedAt;
            IsActive = isActive ?? true;

            Validate();
        }

        public void Validate()
        {
            if (Year <= 1900 || Year >= 2100)
                throw new EntityValidationException($"{nameof(Year)} should be a valid year");

            if (Plate.Length != 7)
                throw new EntityValidationException($"{nameof(Plate)} should have 7 characters");
        }

        public void UpdatePlate(string newPlate)
        {
            Plate = newPlate;
            UpdatedAt = DateTime.Now;

            Validate();
        }

        public void Delete()
        {
            IsActive = false;
            UpdatedAt = DateTime.Now;

            Validate();
        }
    }
}
