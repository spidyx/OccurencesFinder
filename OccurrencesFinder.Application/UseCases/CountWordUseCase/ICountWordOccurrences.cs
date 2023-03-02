namespace OccurrencesFinder.Application.UseCases.CountWordUseCase;

public interface ICountWordOccurrences
{
    Task<int> Execute(string word, Uri uri);
}