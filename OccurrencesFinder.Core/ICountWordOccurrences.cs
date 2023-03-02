namespace OccurrencesFinder.Core;

public interface ICountWordOccurrences
{
    Task<int> Execute(string word, Uri uri);
}