using OccurrencesFinder.Application.Interfaces;
using OccurrencesFinder.Application.UseCases.ListCountingRecordsUseCase;
using OccurrencesFinder.Domain;

namespace OccurrencesFinder.Application.UnitTests;

public class ListCountingRecordsTests
{
    [Fact]
    public async Task ListCountingRecords_ShouldReturns_RepositoryValues()
    {
        // Arrange
        var expectedRecords = new[]
        {
            new CountWordRecord(new DateTime(2020, 2, 20), "first", 1, new Uri("http://first.com")),
            new CountWordRecord(new DateTime(2010, 10, 10), "second", 2, new Uri("http://second.com")),
        };
        Mock<IRecordsRepository> repositoryMock = new();
        repositoryMock.Setup(r => r.GetRecords())
            .ReturnsAsync(expectedRecords);
        
        ListCountingRecords listCountingRecords = new(repositoryMock.Object);

        // Act
        IEnumerable<CountWordRecord> records = await listCountingRecords.Execute();
        
        // Assert
        records.Should().BeEquivalentTo(expectedRecords);
    }
}