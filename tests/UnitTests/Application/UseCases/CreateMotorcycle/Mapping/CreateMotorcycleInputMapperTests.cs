using Application.UseCases.Motorcycle.CreateMotorcycle.Inputs;
using Application.UseCases.Motorcycle.CreateMotorcycle.Mapping;

namespace UnitTests.Application.UseCases.CreateMotorcycle.Mapping
{
    public class CreateMotorcycleInputMapperTests
    {
        private readonly IFixture _fixture;

        public CreateMotorcycleInputMapperTests()
        {
            _fixture = new Fixture();
        }

        [Fact(DisplayName = nameof(ShouldNotMapWhenInputIsNull))]
        [Trait("Application", "CreateMotorcycleInputMapper")]
        public async Task ShouldNotMapWhenInputIsNull()
        {
            //arrange
            CreateMotorcycleInput input = null;

            //act
            var result = input.MapToDomain();

            //assert
            result.Should().BeNull();
        }

        [Fact(DisplayName = nameof(ShouldNotMapWhenInputIsNull))]
        [Trait("Application", "CreateMotorcycleInputMapper")]
        public async Task ShouldMapWhenInputIsValid()
        {
            //arrange
            var input = _fixture.Build<CreateMotorcycleInput>()
                .With(x => x.Year, 2020)
                .With(x => x.Plate, _fixture.Create<string>().Substring(0, 7))
                .Create();

            //act
            var domain = input.MapToDomain();

            //assert
            domain.Should().NotBeNull();
            domain.Year.Should().Be(input.Year);
            domain.Model.Should().Be(input.Model);
            domain.Plate.Should().Be(input.Plate);
        }
    }
}
