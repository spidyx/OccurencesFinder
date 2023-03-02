using OccurrencesFinder.Application.Interfaces;

namespace OccurrencesFinder.Application;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetUTCNow() => DateTime.UtcNow;
}