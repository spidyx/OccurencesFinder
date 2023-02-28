using System.Net.Mime;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapGet(
    "/",
    () => Results.Content(
        "<html>" +
        "<head>" +
        "<meta charset=\"utf-8\" />" +
        "<title>Hello friend</title>" +
        "</head>" +
        "<body>" +
        "<h1>Hello friend !</h1>" +
        "<p>This is the only page of this site.</p>" +
        "<p>This is the second paragraph.</p>" +
        "</body>" +
        "</html>",
        MediaTypeNames.Text.Html));

app.Run();