using Application.UseCases.DeleteMotorcycle.Inputs;
using Application.UseCases.ListMotorcycles.Inputs;
using Application.UseCases.ListMotorcycles.Mapping;
using System.Threading;

namespace UnitTests.Application.UseCases.ListMotorcycles.Mapping
{
    public class ListMotorcyclesInputMapperTests
    {
        private readonly IFixture _fixture;

        public ListMotorcyclesInputMapperTests()
        {
            _fixture = new Fixture();
        }

        [Fact(DisplayName = nameof(ShouldNotMapWhenInputIsNull))]
        [Trait("Application", "ListMotorcyclesInputMapper")]
        public async Task ShouldNotMapWhenInputIsNull()
        {
            //arrange
            ListMotorcyclesInput input = null;

            //act
            var output = input.MapToSearchInput();

            //assert
            output.Should().BeNull();
        }

        [Fact(DisplayName = nameof(ShouldMapWhenInputIsValid))]
        [Trait("Application", "ListMotorcyclesInputMapper")]
        public async Task ShouldMapWhenInputIsValid()
        {
            //arrange
            var input = _fixture.Create<ListMotorcyclesInput>();

            //act
            var output = input.MapToSearchInput();

            //assert
            output.Should().NotBeNull();
            output.Should().BeEquivalentTo(input, opt => opt.ExcludingMissingMembers());
        }

    }
}
