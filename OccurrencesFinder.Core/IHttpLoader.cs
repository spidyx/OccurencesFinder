namespace OccurrencesFinder.Core;

public interface IHttpLoader
{
    Task<string> LoadContentAsString(Uri uri);
}