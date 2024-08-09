using Application.UseCases.Motorcycle.ListMotorcycles;
using Application.UseCases.Motorcycle.ListMotorcycles.Inputs;
using Domain.Entities;
using Domain.Repository;
using Domain.Repository.Shared.SearchableRepository;
using Microsoft.Extensions.Logging;

namespace UnitTests.Application.UseCases.ListMotorcycles
{

    public class ListMotorcyclesUseCaseTests
    {
        private readonly IFixture _fixture;
        private readonly CancellationToken _cancellationToken;

        private readonly IMotorcycleRepository _repository;
        private readonly ILogger<ListMotorcyclesUseCase> _logger;
        private readonly ListMotorcyclesUseCase _useCase;

        public ListMotorcyclesUseCaseTests()
        {
            _fixture = new Fixture();
            _cancellationToken = new CancellationToken();

            _repository = Substitute.For<IMotorcycleRepository>();
            _logger = Substitute.For<ILogger<ListMotorcyclesUseCase>>();

            _useCase = new  ListMotorcyclesUseCase(_repository, _logger);
        }

        [Fact(DisplayName = nameof(ShouldFailWhenRequestIsInvalid))]
        [Trait("Application", "ListMotorcyclesUseCase")]
        public async Task ShouldFailWhenRequestIsInvalid()
        {
            //arrange
            ListMotorcyclesInput input = null;

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().Contain(x => x.StartsWith("Invalid request"));
        }

        [Fact(DisplayName = nameof(ShouldFailWhenSearchReturnsNoItens))]
        [Trait("Application", "ListMotorcyclesUseCase")]
        public async Task ShouldFailWhenSearchReturnsNoItens()
        {
            //arrange
            var input = _fixture.Create<ListMotorcyclesInput>();
            List<Motorcycle> motos = new();

            var searchOutput = new SearchOutput<Motorcycle>(input.Page, input.PageSize, motos.Count(), motos);

            _repository.Search(Arg.Is<SearchInput>(x => x.Search == input.Search), _cancellationToken).Returns(searchOutput);
            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.Messages.Should().Contain(x => x.Equals($"No plates founded with plate: {input.Search}"));
        }

        [Fact(DisplayName = nameof(ShouldFailWhenExceptionIsThrown))]
        [Trait("Application", "ListMotorcyclesUseCase")]
        public async Task ShouldFailWhenExceptionIsThrown()
        {
            //arrange
            var input = _fixture.Create<ListMotorcyclesInput>();

            _repository
                .When(x => x.Search(Arg.Any<SearchInput>(), Arg.Any<CancellationToken>()))
                .Do(x =>
                {
                    throw new Exception("fail");
                });

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().NotBeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldSuccess))]
        [Trait("Application", "ListMotorcyclesUseCase")]
        public async Task ShouldSuccess()
        {
            //arrange
            var input = _fixture.Create<ListMotorcyclesInput>();

            var motos = _fixture.CreateMany<Motorcycle>(5).ToList();
            var searchOutput = new SearchOutput<Motorcycle>(input.Page, input.PageSize, motos.Count(), motos);

            _repository.Search(Arg.Is<SearchInput>(x => x.Search == input.Search), _cancellationToken).Returns(searchOutput);

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.Messages.Should().BeNullOrEmpty();
        }
    }
}
