using System.Text.RegularExpressions;

namespace OccurrencesFinder.Core;

public class RegexpParser
{
    public int CountWordOccurrences(string word, string text) =>
        Regex.Count(text, $"\\b{word}\\b", RegexOptions.IgnoreCase);
}