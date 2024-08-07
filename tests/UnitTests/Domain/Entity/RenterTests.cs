using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;

namespace UnitTests.Domain.Entity
{
    public class RenterTests
    {
        private readonly IFixture _fixture;

        public RenterTests()
        {
            _fixture = new Fixture();
        }

        [Fact(DisplayName = nameof(ShouldInstantiate))]
        [Trait("Domain", "Renter")]
        public void ShouldInstantiate()
        {
            //arrange
            var id = Guid.NewGuid();
            var name = _fixture.Create<string>();
            var document = _fixture.Create<string>();
            var licenseNumber = _fixture.Create<string>();
            var birthDate = _fixture.Create<DateTime>();
            var licenseType = _fixture.Create<LicenseType>();

            //act
            var renter = new Renter(id, name, document, birthDate, licenseNumber, licenseType);

            //assert
            renter.Id.Should().Be(id);
            renter.Name.Should().Be(name);
            renter.Document.Should().Be(document);
            renter.BirthDate.Should().Be(birthDate);
            renter.LicenseNumber.Should().Be(licenseNumber);
            renter.LicenseType.Should().Be(licenseType);
            renter.CreatedAt.Date.Should().Be(DateTime.Now.Date);

            renter.LicenseImageUrl.Should().BeNull();
            renter.UpdatedAt.Should().BeNull();
        }

        [Fact(DisplayName = nameof(ShouldCanRentReturnsFalse))]
        [Trait("Domain", "Renter")]
        public void ShouldCanRentReturnsFalse()
        {
            //arrange
            var id = Guid.NewGuid();
            var name = _fixture.Create<string>();
            var document = _fixture.Create<string>();
            var licenseNumber = _fixture.Create<string>();
            var birthDate = _fixture.Create<DateTime>();
            var licenseType = LicenseType.B;
            var renter = new Renter(id, name, document, birthDate, licenseNumber, licenseType);

            //act
            var result = renter.CanRent();

            //assert
            result.Should().BeFalse();
        }

        [Theory(DisplayName = nameof(ShouldCanRentReturnsTrue))]
        [Trait("Domain", "Renter")]
        [InlineData(LicenseType.A)]
        [InlineData(LicenseType.AB)]
        public void ShouldCanRentReturnsTrue(LicenseType licenseType)
        {
            //arrange
            var id = Guid.NewGuid();
            var name = _fixture.Create<string>();
            var document = _fixture.Create<string>();
            var licenseNumber = _fixture.Create<string>();
            var birthDate = _fixture.Create<DateTime>();
            var renter = new Renter(id, name, document, birthDate, licenseNumber, licenseType);

            //act
            var result = renter.CanRent();

            //assert
            result.Should().BeTrue();
        }

        [Theory(DisplayName = nameof(ShouldThrowExceptionWhenNameIsNullOrEmpty))]
        [Trait("Domain", "Renter")]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldThrowExceptionWhenNameIsNullOrEmpty(string name)
        {
            //arrange
            var id = Guid.NewGuid();
            var document = _fixture.Create<string>();
            var licenseNumber = _fixture.Create<string>();
            var birthDate = _fixture.Create<DateTime>();

            //act
            Action action = () => { _ = new Renter(id, name, document, birthDate, licenseNumber, LicenseType.B); };

            //assert
            action.Should().Throw<EntityValidationException>().WithMessage($"{nameof(Renter.Name)} should not be null or empty");
        }


        [Theory(DisplayName = nameof(ShouldThrowExceptionWhenDocumentIsNullOrEmpty))]
        [Trait("Domain", "Renter")]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldThrowExceptionWhenDocumentIsNullOrEmpty(string document)
        {
            //arrange
            var id = Guid.NewGuid();
            var name = _fixture.Create<string>();
            var licenseNumber = _fixture.Create<string>();
            var birthDate = _fixture.Create<DateTime>();

            //act
            Action action = () => { _ = new Renter(id, name, document, birthDate, licenseNumber, LicenseType.B); };

            //assert
            action.Should().Throw<EntityValidationException>().WithMessage($"{nameof(Renter.Document)} should not be null or empty");
        }

        [Theory(DisplayName = nameof(ShouldThrowExceptionWhenLicenseNumbertIsNullOrEmpty))]
        [Trait("Domain", "Renter")]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldThrowExceptionWhenLicenseNumbertIsNullOrEmpty(string licenseNumber)
        {
            //arrange
            var id = Guid.NewGuid();
            var name = _fixture.Create<string>();
            var document = _fixture.Create<string>();
            var birthDate = _fixture.Create<DateTime>();

            //act
            Action action = () => { _ = new Renter(id, name, document, birthDate, licenseNumber, LicenseType.B); };

            //assert
            action.Should().Throw<EntityValidationException>().WithMessage($"{nameof(Renter.LicenseNumber)} should not be null or empty");
        }

        [Fact(DisplayName = nameof(ShouldUploadLicenseImageReturnsTrue))]
        [Trait("Domain", "Renter")]
        public void ShouldUploadLicenseImageReturnsTrue()
        {
            //arrange
            var id = Guid.NewGuid();
            var name = _fixture.Create<string>();
            var document = _fixture.Create<string>();
            var licenseNumber = _fixture.Create<string>();
            var birthDate = _fixture.Create<DateTime>();
            var renter = new Renter(id, name, document, birthDate, licenseNumber, LicenseType.A);

            var url = _fixture.Create<string>();

            //act
            renter.UploadLicenseImage(url);

            //assert
            renter.LicenseImageUrl.Should().Be(url);
        }
    }
}
