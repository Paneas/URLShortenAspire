using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using URLShortenAspire.DB;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddHttpLogging(logging =>
{
	logging.LoggingFields = HttpLoggingFields.All;
	logging.RequestBodyLogLimit = 4096;
	logging.ResponseBodyLogLimit = 4096;
	logging.CombineLogs = true;
});

var app = builder.Build();

app.UseHttpLogging();
app.UseExceptionHandler();

app.MapGet("/{short}", (string url) =>
{
	return url;
});

app.MapGet("/shorten", (string url) =>
{
	return url;
});

app.MapDefaultEndpoints();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
	var context = serviceScope.ServiceProvider.GetService<DBContext>()!;
	context.Database.Migrate();
}

app.Run();
