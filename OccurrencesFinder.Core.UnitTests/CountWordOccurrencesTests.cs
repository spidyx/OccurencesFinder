namespace OccurrencesFinder.Core.UnitTests;

public class CountWordOccurrencesTests
{
    private static readonly Uri Uri = new("http://non-existent.local");

    [Fact]
    public async Task CountWordsOccurrences_Should_QueryHttpResource()
    {
        // Arrange
        var httpLoaderMock = new Mock<IHttpLoader>();
        httpLoaderMock.Setup(loader => loader.LoadContentAsString(It.Is<Uri>(x => x == Uri)))
            .ReturnsAsync("")
            .Verifiable();

        CountWordOccurrences countWordOccurrences = new(httpLoaderMock.Object);

        // Act
        await countWordOccurrences.Execute("foo", Uri);

        // Assert
        httpLoaderMock.Verify();
    }

    [Fact]
    public async Task CountWordsOccurrences_ShouldReturnsOccurrencesCount()
    {
        // Arrange
        var httpLoaderMock = new Mock<IHttpLoader>();
        httpLoaderMock.Setup(loader => loader.LoadContentAsString(It.IsAny<Uri>()))
            .ReturnsAsync("This sentence is composed of words, but contains only two times the word word");

        CountWordOccurrences countWordOccurrences = new(httpLoaderMock.Object);

        // Act
        int result = await countWordOccurrences.Execute("word", Uri);

        // Assert
        result.Should().Be(2);
    }
}