using OccurrencesFinder.Application.UseCases.CountWordUseCase;

namespace OccurrencesFinder.Application.UnitTests;

public class RegexpParserTests
{
    [Fact]
    public void CountWords_CountExactOccurrences()
    {
        RegexpParser regexpParser = new();

        int count = regexpParser.CountWordOccurrences(
            "word", "This sentence is composed of words, but contains only two times the word word");


        count.Should().Be(2);
    }
}