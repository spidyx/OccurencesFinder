using OccurrencesFinder.Application.UseCases.CountWordUseCase;
using OccurrencesFinder.Application.UseCases.ListCountingRecordsUseCase;
using OccurrencesFinder.Application.UseCases.SaveCountingRecords;
using OccurrencesFinder.Domain;

namespace OccurrencesFinder.Console;

public class Application
{
    private readonly ICountWordOccurrences countWordOccurrences;
    private readonly IListCountingRecords listCountingRecords;
    private readonly ISaveCountingRecord saveCountingRecord;
    private readonly TextReader input;
    private readonly TextWriter output;

    public Application(
        TextReader input,
        TextWriter output,
        ICountWordOccurrences countWordOccurrences,
        IListCountingRecords listCountingRecords,
        ISaveCountingRecord saveCountingRecord)
    {
        this.input = input;
        this.output = output;
        this.countWordOccurrences = countWordOccurrences;
        this.listCountingRecords = listCountingRecords;
        this.saveCountingRecord = saveCountingRecord;
    }

    public async Task Run()
    {
        await output.WriteLineAsync("Welcome to the Occurrences Finder Program" + Environment.NewLine);

        Func<Task> action = await RunMenu();
        await action();
    }

    private async Task<Func<Task>> RunMenu()
    {
        Func<Task>? action;

        do
        {
            await output.WriteLineAsync("What do you want to do ?");
            await output.WriteLineAsync("1 - Count Occurrences of a webpage");
            await output.WriteLineAsync("2 - Show saved records");
            await output.WriteLineAsync();
            await output.WriteAsync("Write your choice : ");

            string? userChoice = await input.ReadLineAsync();
            action = userChoice switch
            {
                "1" => FindOccurrences,
                "2" => DisplaySavedResults,
                _ => null
            };

            if (action == null)
                await output.WriteLineAsync("Your choice is invalid." + Environment.NewLine);
        } while (action == null);

        return action;
    }

    private async Task FindOccurrences()
    {
        await output.WriteLineAsync("Which web page should we analyze ?");
        Uri uri = await GetInputForUri();

        await output.WriteLineAsync("Which word should we count ?");
        string word = await GetInputForWord();

        int count = await countWordOccurrences.Execute(word, uri);

        await output.WriteLineAsync($"We found {count} times the word {word}.");

        await output.WriteLineAsync("Do you want to save it ? (y/n)");

        await SaveCount(word, count, uri);
    }

    private async Task<Uri> GetInputForUri()
    {
        Uri? uri = null;

        do
        {
            string? inputUri = await input.ReadLineAsync();
            try
            {
                uri = new Uri(inputUri!);
            }
            catch (Exception)
            {
                await output.WriteLineAsync("The provided uri has not a correct format. Enter a new uri :");
            }
        } while (uri == null);

        return uri;
    }

    private async Task<string> GetInputForWord()
    {
        string? word = null;

        do
        {
            string? inputWord = await input.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(inputWord))
                await output.WriteLineAsync("We did not read a word. Please retry :");
            else
                word = inputWord;
        } while (word == null);

        return word;
    }

    private async Task SaveCount(string word, int count, Uri uri)
    {
        bool shouldSave = await GetUserChoice();

        if (shouldSave)
            await saveCountingRecord.Execute(word, count, uri);
    }

    private async Task<bool> GetUserChoice()
    {
        bool? shouldSave = null;

        do
        {
            string? shouldSaveInput = await input.ReadLineAsync();
            shouldSave = shouldSaveInput switch
            {
                "y" => true,
                "n" => false,
                _ => null
            };
        } while (!shouldSave.HasValue);

        return shouldSave.Value;
    }

    private async Task DisplaySavedResults()
    {
        await output.WriteLineAsync("The following records have been saved :");

        IEnumerable<CountWordRecord> records = await listCountingRecords.Execute();
        foreach (CountWordRecord record in records)
        {
            string date = record.DateTime.ToLocalTime().ToString("f");
            await output.WriteLineAsync(
                $"{date} - The word {record.Word} have been found \"{record.Count}\" at {record.uri}.");
        }
    }
}