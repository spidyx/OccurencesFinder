using System.Text.Json;
using OccurrencesFinder.Application.Interfaces;
using OccurrencesFinder.Domain;

namespace OccurrencesFinder.Infrastructure;

public class LocalFileRecordsRepository : IRecordsRepository
{
    private readonly string localFilePath;

    public LocalFileRecordsRepository(string localFilePath)
    {
        this.localFilePath = localFilePath;
    }

    public async Task SaveNewRecord(CountWordRecord newRecord)
    {
        string json = JsonSerializer.Serialize(newRecord);
        await File.AppendAllLinesAsync(localFilePath, new []{json});
    }
}