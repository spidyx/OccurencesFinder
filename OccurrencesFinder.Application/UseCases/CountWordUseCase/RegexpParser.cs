using System.Text.RegularExpressions;

namespace OccurrencesFinder.Application.UseCases.CountWordUseCase;

public class RegexpParser
{
    public int CountWordOccurrences(string word, string text) =>
        Regex.Count(text, $"\\b{word}\\b", RegexOptions.IgnoreCase);
}