using OccurrencesFinder.Domain;

namespace OccurrencesFinder.Application.Interfaces;

public interface IRecordsRepository
{
    Task SaveNewRecord(CountWordRecord newRecord);
    Task<IEnumerable<CountWordRecord>> GetRecords();
}