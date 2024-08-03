using Domain.Entities;
using Domain.Enums;

namespace UnitTests.Domain.Entity
{
    public class RentalTests
    {
        private IFixture _fixture;

        public RentalTests()
        {
            _fixture = new Fixture();
        }

        [Theory(DisplayName = nameof(ShouldInstantiate))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(PlanType.SevenDays)]
        [InlineData(PlanType.FifteenDays)]
        [InlineData(PlanType.ThirtyDays)]
        [InlineData(PlanType.FortyFiveDays)]
        [InlineData(PlanType.FiftyDays)]
        public void ShouldInstantiate(PlanType planType)
        {
            //arrange
            var id = Guid.NewGuid();
            var driverId = Guid.NewGuid();
            var motorcycleId = Guid.NewGuid();
            var initialDate = _fixture.Create<DateTime>();

            //act
            var rental = new Rental(id, driverId, motorcycleId, planType, initialDate);

            //arrange
            rental.Id.Should().Be(id);
            rental.DriverId.Should().Be(driverId);
            rental.PlanType.Should().Be(planType);
            rental.InitialDate.Date.Should().Be(initialDate.AddDays(1).Date);
            rental.CreatedAt.Date.Should().Be(DateTime.Now.Date);

            rental.DevolutionDate.Should().BeNull();
            rental.TotalValue.Should().BeNull();
            rental.IsFinished.Should().BeFalse();
            rental.UpdatedAt.Should().BeNull();
        }

        [Fact(DisplayName = nameof(ShouldFinishRentalWithEarlyReturn))]
        [Trait("Domain", "Category - Aggregates")]
        public void ShouldFinishRentalWithEarlyReturn()
        {
            //arrange
            var id = Guid.NewGuid();
            var driverId = Guid.NewGuid();
            var motorcycleId = Guid.NewGuid();

            var initialDate = DateTime.Now;
            var rental = new Rental(id, driverId, motorcycleId, PlanType.SevenDays, initialDate);
            var devolutionDate = initialDate.AddDays(6);

            //act
            rental.FinishRental(devolutionDate);

            //arrange
            rental.IsFinished.Should().BeTrue();
            rental.DevolutionDate.Value.Date.Should().Be(devolutionDate.Date);
            rental.UpdatedAt.Value.Date.Should().Be(DateTime.Now.Date);
            rental.TotalValue.Should().Be(186);
        }

        [Fact(DisplayName = nameof(ShouldFinishRentalWithLateReturn))]
        [Trait("Domain", "Category - Aggregates")]
        public void ShouldFinishRentalWithLateReturn()
        {
            //arrange
            var id = Guid.NewGuid();
            var driverId = Guid.NewGuid();
            var motorcycleId = Guid.NewGuid();

            var initialDate = DateTime.Now;
            var rental = new Rental(id, driverId, motorcycleId, PlanType.FortyFiveDays, initialDate);
            var devolutionDate = initialDate.AddDays(46);

            //act
            rental.FinishRental(devolutionDate);

            //arrange
            rental.IsFinished.Should().BeTrue();
            rental.DevolutionDate.Value.Date.Should().Be(devolutionDate.Date);
            rental.UpdatedAt.Value.Date.Should().Be(DateTime.Now.Date);
            rental.TotalValue.Should().Be(950);
        }

        [Fact(DisplayName = nameof(ShouldFinishRentalWithExpectedDate))]
        [Trait("Domain", "Category - Aggregates")]
        public void ShouldFinishRentalWithExpectedDate()
        {
            //arrange
            var id = Guid.NewGuid();
            var driverId = Guid.NewGuid();
            var motorcycleId = Guid.NewGuid();

            var initialDate = DateTime.Now;
            var rental = new Rental(id, driverId, motorcycleId, PlanType.FifteenDays, initialDate);
            var devolutionDate = initialDate.AddDays(15);

            //act
            rental.FinishRental(devolutionDate);

            //arrange
            rental.IsFinished.Should().BeTrue();
            rental.DevolutionDate.Value.Date.Should().Be(devolutionDate.Date);
            rental.DevolutionDate.Value.Date.Should().Be(rental.DevolutionDate.Value.Date);
            rental.UpdatedAt.Value.Date.Should().Be(DateTime.Now.Date);
            rental.TotalValue.Should().Be(rental.ExpectedTotalValue);
        }
    }
}
