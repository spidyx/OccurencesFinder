using OccurrencesFinder.Application.Interfaces;
using OccurrencesFinder.Domain;

namespace OccurrencesFinder.Application.UseCases.SaveCountingRecords;

public class SaveCountingRecord : ISaveCountingRecord
{
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly IRecordsRepository recordsRepository;

    public SaveCountingRecord(IDateTimeProvider dateTimeProvider, IRecordsRepository recordsRepository)
    {
        this.dateTimeProvider = dateTimeProvider;
        this.recordsRepository = recordsRepository;
    }

    public async Task Execute(string word, int count, Uri uri)
    {
        CountWordRecord newRecord = new(dateTimeProvider.GetUTCNow(), word, count, uri);
        await recordsRepository.SaveNewRecord(newRecord);
    }
}