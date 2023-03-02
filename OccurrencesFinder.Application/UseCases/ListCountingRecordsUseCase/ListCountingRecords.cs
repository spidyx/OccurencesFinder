using OccurrencesFinder.Application.Interfaces;
using OccurrencesFinder.Domain;

namespace OccurrencesFinder.Application.UseCases.ListCountingRecordsUseCase;

public class ListCountingRecords
{
    private readonly IRecordsRepository repository;

    public ListCountingRecords(IRecordsRepository repository)
    {
        this.repository = repository;
    }

    public Task<IEnumerable<CountWordRecord>> Execute()
    {
        return repository.GetRecords();
    }
}