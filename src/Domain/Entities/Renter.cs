using Domain.Enums;
using Domain.Exceptions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Renter
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Document { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string LicenseNumber { get; private set; }
        public LicenseType LicenseType { get; private set; }
        public string LicenseImageFileName { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Renter() { }
        public Renter(Guid id,
            string name,
            string document,
            DateTime birthDate,
            string licenseNumber,
            LicenseType licenseType,
            string licenseImageFileName = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null)
        {
            Id = id;
            Name = name;
            Document = document;
            BirthDate = birthDate;
            LicenseNumber = licenseNumber;
            LicenseType = licenseType;
            LicenseImageFileName = licenseImageFileName;

            CreatedAt = createdAt ?? DateTime.Now;
            UpdatedAt = updatedAt;

            Validate();
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new EntityValidationException($"{nameof(Name)} should not be null or empty");

            if (string.IsNullOrEmpty(Document))
                throw new EntityValidationException($"{nameof(Document)} should not be null or empty");

            if (string.IsNullOrEmpty(LicenseNumber))
                throw new EntityValidationException($"{nameof(LicenseNumber)} should not be null or empty");
        }

        public bool CanRent()
        {
            return LicenseType == LicenseType.A || LicenseType == LicenseType.AB;
        }

        public void GetFriendlyLicenseImage(string extension)
        {
            var fileKeyName = string.Concat(Name.Split(' ')[0], "-", LicenseNumber.Substring(LicenseNumber.Length - 5, 5), extension);
            LicenseImageFileName = fileKeyName;
            UpdatedAt = DateTime.Now;
        }

    }
}
