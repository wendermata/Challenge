using Application.UseCases.Motorcycle.ListMotorcycles.Mapping;
using Application.UseCases.Motorcycle.ListMotorcycles.Inputs;
using Application.UseCases.Motorcycle.ListMotorcycles.Outputs;
using Domain.Repository;
using Microsoft.Extensions.Logging;
using Application.Common.Helpers;

namespace Application.UseCases.Motorcycle.ListMotorcycles
{
    public class ListMotorcyclesUseCase : IListMotorcyclesUseCase
    {
        private readonly IMotorcycleRepository _repository;
        private readonly ILogger<ListMotorcyclesUseCase> _logger;

        public ListMotorcyclesUseCase(IMotorcycleRepository repository, ILogger<ListMotorcyclesUseCase> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ListMotorcyclesOutput> Handle(ListMotorcyclesInput request, CancellationToken cancellationToken)
        {
            var output = new ListMotorcyclesOutput();
            try
            {
                if (request is null || string.IsNullOrEmpty(request.Search))
                {
                    _logger.LogError($"Invalid request: {request}");
                    output.ErrorMessages.Add($"Invalid request: {request}");
                    return output;
                }

                var searchInput = request.MapToSearchInput();
                var searchResult = await _repository.Search(searchInput, cancellationToken);
                if (searchResult.Items.Count == 0)
                {
                    _logger.LogWarning($"No plates founded with plate: {request.Search}");
                    output.Messages.Add($"No plates founded with plate: {request.Search}");
                    return output;
                }

                output = searchResult.MapToOutput();
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An error occurred while listing motorcycles: {ex.Message} request: {SerializeHelper.SerializeObjectToJson(request)}");
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}
