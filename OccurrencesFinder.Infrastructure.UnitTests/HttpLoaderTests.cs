using System.Net.Mime;

namespace OccurrencesFinder.Infrastructure.UnitTests;

public class HttpLoaderTests
{
    [Fact]
    public async Task LoadContentAsString()
    {
        // Arrange
        const string EXPECTED_RESULT = "Content from http response";
        var uri = new Uri("http://non-existent.local");

        var mockHttpMessageHandler = new MockHttpMessageHandler();
        mockHttpMessageHandler
            .When(uri.ToString())
            .Respond(MediaTypeNames.Text.Plain, EXPECTED_RESULT);
        HttpLoader loader = new(mockHttpMessageHandler.ToHttpClient());

        // Act
        string result = await loader.LoadContentAsString(uri);

        // Assert
        result.Should().Be(EXPECTED_RESULT);
    }
}