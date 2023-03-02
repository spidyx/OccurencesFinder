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

    public async Task<IEnumerable<CountWordRecord>> GetRecords()
    {
        List<CountWordRecord> records = new();
        await using FileStream fileStream = File.OpenRead(localFilePath);
        using var streamReader = new StreamReader(fileStream);

        while (await streamReader.ReadLineAsync() is { } line)
        {
            var record = JsonSerializer.Deserialize<CountWordRecord>(line);
            if (record != null) records.Add(record);
        }

        return records;
    }
}