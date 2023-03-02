using OccurrencesFinder.Core;

namespace OccurrencesFinder.Console;

public class Application
{
    private readonly ICountWordOccurrences countWordOccurrences;
    private readonly TextReader input;
    private readonly TextWriter output;

    public Application(TextReader input, TextWriter output, ICountWordOccurrences countWordOccurrences)
    {
        this.input = input;
        this.output = output;
        this.countWordOccurrences = countWordOccurrences;
    }

    public async Task Run()
    {
        await output.WriteLineAsync("Welcome to the Occurrences Finder Program");
        await FindOccurrences();
    }

    private async Task FindOccurrences()
    {
        await output.WriteLineAsync("Which web page should we analyze ?");
        Uri uri = await GetInputForUri();

        await output.WriteLineAsync("Which word should we count ?");
        string word = await GetInputForWord();

        int count = await countWordOccurrences.Execute(word, uri);

        await output.WriteAsync(count.ToString());
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
}