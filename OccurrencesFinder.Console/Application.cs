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
        string uri = await input.ReadLineAsync();
        
        await output.WriteLineAsync("Which word should we count ?");
        string word = await input.ReadLineAsync();

        int count = await countWordOccurrences.Execute(word, new Uri(uri));
        
        await output.WriteAsync(count.ToString());
    }
}