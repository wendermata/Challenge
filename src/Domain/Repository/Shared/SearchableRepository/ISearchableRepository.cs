namespace Domain.Repository.Shared.SearchableRepository;
public interface ISearchableRepository<T>
{
    Task<SearchOutput<T>> Search(
        SearchInput input,
        CancellationToken cancellationToken
    );
}
