using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class DeliveryMan
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Document { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string LicenseNumber { get; private set; }
        public LicenseType LicenseType { get; private set; }
        public string LicenseImageUrl { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public DeliveryMan(Guid id,
            string name,
            string document,
            DateTime birthDate,
            string licenseNumber,
            LicenseType licenseType,
            string licenseImage = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null)
        {
            Id = id;
            Name = name;
            Document = document;
            BirthDate = birthDate;
            LicenseNumber = licenseNumber;
            LicenseType = licenseType;
            LicenseImageUrl = licenseImage;

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

        public bool CanRental()
        {
            return LicenseType == LicenseType.A || LicenseType == LicenseType.AB;
        }

    }
}
