using System.Text;
using FluentAssertions;
using Moq;
using OccurrencesFinder.Application.UseCases.CountWordUseCase;

namespace OccurrencesFinder.Console.UnitTests;

public class ApplicationTest
{
    private const string MenuEntryToStartCountWordOccurrences = "1";
    private const string Word = "foo";
    private const string Uri = "http://non-existent.local";

    [Fact()]
    public async Task Application_FindOccurrencesCount()
    {
        // Arrange
        Mock<ICountWordOccurrences> countWordOccurrencesMock = new();
        countWordOccurrencesMock
            .Setup(c => 
                c.Execute(
                    It.Is<string>(w => w == Word), 
                    It.Is<Uri>(uri => uri == new Uri(Uri))))
            .ReturnsAsync(42);

        await using StringWriter outputWriter = new StringWriter();
        using StringReader input = new StringReader(
            new StringBuilder().AppendJoin(
                    Environment.NewLine,
                    MenuEntryToStartCountWordOccurrences,
                    Uri,
                    Word)
                .ToString());

        Application application = new(input, outputWriter, countWordOccurrencesMock.Object);

        // Act
        await application.Run();

        // Assert
        string[] outputs = outputWriter.GetStringBuilder().ToString().Split(Environment.NewLine);
        outputs[8].Should().Be("42");
        input.Peek().Should().Be(-1, "because all input must have been read");
    }
}