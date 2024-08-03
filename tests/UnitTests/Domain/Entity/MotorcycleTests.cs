using Domain.Entities;
using Domain.Exceptions;

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
        [Trait("Domain", "Motorcycle")]
        public void ShouldInstantiate()
        {
            //arrange
            var id = Guid.NewGuid();
            var year = 2000;
            var model = _fixture.Create<string>();
            var plate = _fixture.Create<string>()[..7];

            //act
            var motorcycle = new Motorcycle(id, year, model, plate);

            //assert
            motorcycle.Id.Should().Be(id);
            motorcycle.Year.Should().Be(year);
            motorcycle.Model.Should().Be(model);
            motorcycle.Plate.Should().Be(plate);
            motorcycle.CreatedAt.Date.Should().Be(DateTime.Now.Date);
            motorcycle.UpdatedAt.Should().Be(null);
        }

        [Fact(DisplayName = nameof(ShouldThrowExceptionWhenYearIsInvalid))]
        [Trait("Domain", "Motorcycle")]
        public void ShouldThrowExceptionWhenYearIsInvalid()
        {
            //arrange
            _fixture.Customizations.Add(new RandomNumericSequenceGenerator(3000, int.MaxValue));

            var id = Guid.NewGuid();
            var year = _fixture.Create<int>();
            var model = _fixture.Create<string>();
            var plate = _fixture.Create<string>();

            //act
            Action action = () => _ = new Motorcycle(id, year, model, plate);

            //assert
            action.Should().Throw<EntityValidationException>().WithMessage($"{nameof(Motorcycle.Year)} should be a valid year");
        }

        [Fact(DisplayName = nameof(ShouldThrowExceptionWhenPlateIsInvalid))]
        [Trait("Domain", "Motorcycle")]
        public void ShouldThrowExceptionWhenPlateIsInvalid()
        {
            //arrange
            var id = Guid.NewGuid();
            var year = 2014;
            var model = _fixture.Create<string>();
            var plate = _fixture.Create<string>();

            //act
            Action action = () => _ = new Motorcycle(id, year, model, plate);

            //assert
            action.Should().Throw<EntityValidationException>().WithMessage($"{nameof(Motorcycle.Plate)} should have 7 characters");
        }

        [Fact(DisplayName = nameof(ShouldUpdatePlate))]
        [Trait("Domain", "Motorcycle")]
        public void ShouldUpdatePlate()
        {
            //arrange
            var id = Guid.NewGuid();
            var year = 2000;
            var model = _fixture.Create<string>();
            var plate = _fixture.Create<string>()[..7];
            var newPlate = _fixture.Create<string>()[..7];

            //act
            var motorcycle = new Motorcycle(id, year, model, plate);
            motorcycle.UpdatePlate(newPlate);

            //assert
            motorcycle.Plate.Should().Be(newPlate);
        }
    }
}
