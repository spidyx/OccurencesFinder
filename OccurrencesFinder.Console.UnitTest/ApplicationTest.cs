using System.Text;
using FluentAssertions;
using Moq;
using OccurrencesFinder.Core;

namespace OccurrencesFinder.Console.UnitTest;

public class ApplicationTest
{
    private const string WORD = "foo";
    private const string URI = "http://non-existent.local";
        
    [Fact]
    public async Task Application_FindOccurrencesCount()
    {
        // Arrange
        Mock<ICountWordOccurrences> countWordOccurrencesMock = new();
        countWordOccurrencesMock
            .Setup(c => 
                c.Execute(
                    It.Is<string>(w => w == WORD), 
                    It.Is<Uri>(uri => uri == new Uri(URI))))
            .ReturnsAsync(42);

        await using StringWriter outputWriter = new StringWriter();
        using StringReader input = new StringReader(
            new StringBuilder().AppendJoin(
                    Environment.NewLine,
                    URI,
                    WORD)
                .ToString());

        Application application = new(input, outputWriter, countWordOccurrencesMock.Object);

        // Act
        await application.Run();

        // Assert
        string[] outputs = outputWriter.GetStringBuilder().ToString().Split(Environment.NewLine);
        outputs[3].Should().Be("42");
        input.Peek().Should().Be(-1, "because all input must have been read");
    }
}