using OccurrencesFinder.Domain;

namespace OccurrencesFinder.Application.UseCases.ListCountingRecordsUseCase;

public interface IListCountingRecords
{
    Task<IEnumerable<CountWordRecord>> Execute();
}