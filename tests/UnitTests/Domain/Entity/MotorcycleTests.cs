using Domain.Entities;

namespace UnitTests.Domain.Entity
{
    public class MotorcycleTests
    {
        private readonly IFixture _fixture;
        public MotorcycleTests()
        {
            _fixture = new Fixture();
        }

        [Fact(DisplayName = nameof(ShouldInstantiate))]
        [Trait("Domain", "Category - Aggregates")]
        public void ShouldInstantiate()
        {
            //arrange
            var id = Guid.NewGuid();
            var year = _fixture.Create<int>();
            var model = _fixture.Create<string>();
            var plate = _fixture.Create<string>();

            //act
            var motorcycle = new Motorcycle(id, year, model, plate);

            //arrange
            motorcycle.Id.Should().Be(id);
            motorcycle.Year.Should().Be(year);
            motorcycle.Model.Should().Be(model);
            motorcycle.Plate.Should().Be(plate);
            motorcycle.CreatedAt.Date.Should().Be(DateTime.Now.Date);
            motorcycle.UpdatedAt.Should().Be(null);
        }
    }
}
