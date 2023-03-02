using OccurrencesFinder.Application.Interfaces;

namespace OccurrencesFinder.Infrastructure;

public class HttpLoader : IHttpLoader
{
    private readonly HttpClient client;
    
    public HttpLoader(HttpClient client)
    {
        this.client = client;
    }

    public async Task<string> LoadContentAsString(Uri uri)
    {
        return await client.GetStringAsync(uri);
    }
}