namespace OccurrencesFinder.Domain;

public record CountWordRecord(DateTime DateTime, string Word, int Count);

class temp
{
    void toto()
    {
        new CountWordRecord(DateTime.Now, null, 0);
    }
}