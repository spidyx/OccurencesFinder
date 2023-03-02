using Microsoft.Extensions.DependencyInjection;
using OccurrencesFinder.Console;
using OccurrencesFinder.Core;

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