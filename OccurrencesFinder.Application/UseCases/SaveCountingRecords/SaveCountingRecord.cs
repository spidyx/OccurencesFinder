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

    public async Task Execute(string word, int count)
    {
        CountWordRecord newRecord = new(dateTimeProvider.GetUTCNow(), word, count);
        await recordsRepository.SaveNewRecord(newRecord);
    }
}