namespace OccurrencesFinder.Application.Interfaces;

public interface IHttpLoader
{
    Task<string> LoadContentAsString(Uri uri);
}