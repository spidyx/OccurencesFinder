namespace OccurrencesFinder.Application.UseCases.SaveCountingRecords;

public interface ISaveCountingRecord
{
    Task Execute(string word, int count);
}