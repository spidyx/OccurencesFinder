namespace OccurrencesFinder.Core;

public class CountWordOccurrences
{
    private readonly IHttpLoader loader;
    private readonly RegexpParser parser = new();
    
    public CountWordOccurrences(IHttpLoader loader)
    {
        this.loader = loader;
    }

    public async Task<int> Execute(string word, Uri uri)
    {
        string content = await loader.LoadContentAsString(uri);
        return parser.CountWordOccurrences(word, content);
    }
}