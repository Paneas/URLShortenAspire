using Microsoft.AspNetCore.HttpLogging;
using URLShortenAspire.Core;
using URLShortenAspire.DAL.Units;
using URLShortenAspire.DB;
using URLShortenAspire.Models.Database;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddScoped<ShortenService>();
builder.Services.AddScoped<UnitOfWork>();

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

app.MapGet("/{shortUrl}", (ShortenService urlService, string shortUrl) =>
{
	var ent = urlService.GetFullUrl(shortUrl);

	if (ent is null)
		return Results.NotFound();

	return Results.Redirect(ent);
});

app.MapGet("/shorten", (HttpContext context, ShortenService urlService, string url) =>
{
	URLEntity upd = urlService.ShorternUrl(url);

	return Results.Ok("https://" + context.Request.Host + "/" + upd.Shorten);
});

app.MapDefaultEndpoints();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
	var context = serviceScope.ServiceProvider.GetService<DBContext>()!;
	context.Database.EnsureCreated();
}

app.Run();
