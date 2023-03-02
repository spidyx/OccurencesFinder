using Microsoft.Extensions.DependencyInjection;
using OccurrencesFinder.Console;
using OccurrencesFinder.Application.Interfaces;
using OccurrencesFinder.Application.UseCases.CountWordUseCase;
using OccurrencesFinder.Infrastructure;

await using ServiceProvider serviceProvider = new ServiceCollection()
    .AddHttpClient()
    .AddTransient<IHttpLoader, HttpLoader>()
    .AddTransient<ICountWordOccurrences, CountWordOccurrences>()
    .AddTransient<TextReader>(_ => Console.In)
    .AddTransient<TextWriter>(_ => Console.Out)
    .AddTransient<Application>()
    .BuildServiceProvider();

var app = serviceProvider.GetService<Application>();

await app.Run();