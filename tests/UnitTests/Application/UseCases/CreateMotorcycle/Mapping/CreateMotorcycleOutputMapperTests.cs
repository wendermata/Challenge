using Application.UseCases.Motorcycle.CreateMotorcycle.Mapping;
using Domain.Entities;

namespace UnitTests.Application.UseCases.CreateMotorcycle.Mapping
{
    public class CreateMotorcycleOutputMapperTests
    {
        private readonly IFixture _fixture;

        public CreateMotorcycleOutputMapperTests()
        {
            _fixture = new Fixture();
        }

        [Fact(DisplayName = nameof(ShouldNotMapWhenDomainIsNull))]
        [Trait("Application", "CreateMotorcycleOutputMapper")]
        public async Task ShouldNotMapWhenDomainIsNull()
        {
            //arrange
            Motorcycle domain = null;

            //act
            var result = domain.MapToOutput();

            //assert
            result.Should().BeNull();
        }

        [Fact(DisplayName = nameof(ShouldMapWhenDomainIsValid))]
        [Trait("Application", "CreateMotorcycleOutputMapper")]
        public async Task ShouldMapWhenDomainIsValid()
        {
            //arrange
            var domain = new Motorcycle(Guid.NewGuid(),
                2020,
                _fixture.Create<string>(),
                _fixture.Create<string>()[..7]);

            //act
            var output = domain.MapToOutput();

            //assert
            output.Should().NotBeNull();
            output.IsValid.Should().BeTrue();
            output.Messages.Should().Contain($"Motorcycle with plate {domain.Plate} created successfully. Id: {domain.Id}");
        }

    }
}
