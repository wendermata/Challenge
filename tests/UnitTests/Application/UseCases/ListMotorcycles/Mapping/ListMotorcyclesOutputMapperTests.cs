using Application.UseCases.Motorcycle.ListMotorcycles.Mapping;
using Domain.Entities;
using Domain.Repository.Shared.SearchableRepository;

namespace UnitTests.Application.UseCases.ListMotorcycles.Mapping
{
    public class ListMotorcyclesOutputMapperTests
    {
        private readonly IFixture _fixture;

        public ListMotorcyclesOutputMapperTests()
        {
            _fixture = new Fixture();
        }

        [Fact(DisplayName = nameof(ShouldNotMapWhenSearchOutputIsNull))]
        [Trait("Application", "ListMotorcyclesOutput")]
        public async Task ShouldNotMapWhenSearchOutputIsNull()
        {
            //arrange
            SearchOutput<Motorcycle> searchOutput = null;

            //act
            var output = searchOutput.MapToOutput();

            //assert
            output.Should().NotBeNull();
            output.Items.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldNotMapWhenSearchOutputItensIsNull))]
        [Trait("Application", "ListMotorcyclesOutput")]
        public async Task ShouldNotMapWhenSearchOutputItensIsNull()
        {
            //arrange
            List<Motorcycle> motos = new();
            SearchOutput<Motorcycle> searchOutput = new(_fixture.Create<int>(),
                _fixture.Create<int>(),
                _fixture.Create<int>(),
                motos);

            //act
            var output = searchOutput.MapToOutput();

            //assert
            output.Should().NotBeNull();
            output.Items.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldMapWhenSearchOutputIsValid))]
        [Trait("Application", "ListMotorcyclesOutput")]
        public async Task ShouldMapWhenSearchOutputIsValid()
        {
            //arrange
            var motos = _fixture.CreateMany<Motorcycle>(4).ToList();
            SearchOutput<Motorcycle> searchOutput = new(_fixture.Create<int>(),
                _fixture.Create<int>(),
                _fixture.Create<int>(),
                motos);

            //act
            var output = searchOutput.MapToOutput();

            //assert
            output.Should().NotBeNull();
            output.Should().BeEquivalentTo(searchOutput, opt => opt.ExcludingMissingMembers());
        }
    }
}
