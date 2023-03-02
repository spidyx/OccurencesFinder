using Microsoft.Extensions.DependencyInjection;
using OccurrencesFinder.Application;
using OccurrencesFinder.Console;
using OccurrencesFinder.Application.Interfaces;
using OccurrencesFinder.Application.UseCases.CountWordUseCase;
using OccurrencesFinder.Application.UseCases.ListCountingRecordsUseCase;
using OccurrencesFinder.Application.UseCases.SaveCountingRecords;
using OccurrencesFinder.Infrastructure;

const string SAVE_FILE = "data.json";
string filePath = Path.Combine(Directory.GetCurrentDirectory(), SAVE_FILE);

await using ServiceProvider serviceProvider = new ServiceCollection()
    .AddHttpClient()
    .AddTransient<IDateTimeProvider, DateTimeProvider>()
    .AddTransient<IHttpLoader, HttpLoader>()
    .AddTransient<IRecordsRepository>(_ => new LocalFileRecordsRepository(filePath))
    .AddTransient<ICountWordOccurrences, CountWordOccurrences>()
    .AddTransient<IListCountingRecords, ListCountingRecords>()
    .AddTransient<ISaveCountingRecord, SaveCountingRecord>()
    .AddTransient<TextReader>(_ => Console.In)
    .AddTransient<TextWriter>(_ => Console.Out)
    .AddTransient<Application>()
    .BuildServiceProvider();


var app = serviceProvider.GetService<Application>();

await app!.Run();