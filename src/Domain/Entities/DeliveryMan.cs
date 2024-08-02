using Domain.Enums;

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
        }

        public bool CanRental()
        {
            return LicenseType == LicenseType.A || LicenseType == LicenseType.AB;
        }

    }
}
