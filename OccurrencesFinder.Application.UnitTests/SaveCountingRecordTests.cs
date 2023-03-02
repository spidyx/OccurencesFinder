using OccurrencesFinder.Application.Interfaces;
using OccurrencesFinder.Application.UseCases;
using OccurrencesFinder.Domain;

namespace OccurrencesFinder.Application.UnitTests;

public class SaveCountingRecordTests
{
    [Fact]
    public async Task SaveCountRecord_Should_SaveNewRecord()
    {
        // Arrange
        const string WORD = "word";
        const int WORD_COUNT = 42;
        DateTime date = new DateTime(2023, 03, 02);

        Mock<IDateTimeProvider> dateTimeProviderMock = new();
        dateTimeProviderMock.Setup(provider => provider.GetUTCNow())
            .Returns(date);

        Mock<IRecordsRepository> repositoryMock = new();

        SaveCountingRecord saveCountingRecord = new(dateTimeProviderMock.Object, repositoryMock.Object);

        // Act
        await saveCountingRecord.Execute(WORD, WORD_COUNT);

        // Assert
        repositoryMock.Verify(
            r => r.SaveNewRecord(new CountWordRecord(date, WORD, WORD_COUNT)),
            Times.Once);
    }
}