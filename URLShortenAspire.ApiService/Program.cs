using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using URLShortenAspire.DAL.Units;
using URLShortenAspire.DB;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

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

app.MapGet("/{shortUrl}", (UnitOfWork unitOfWork, Guid shortUrl) =>
{
	var ent = unitOfWork.UrlRepository.GetById(shortUrl);

	return ent;
});

app.MapGet("/shorten", (UnitOfWork unitOfWork, string url) =>
{
	unitOfWork.BeginTransaction();

	var upd = unitOfWork.UrlRepository.Add(new URLShortenAspire.Models.Database.URLEntity
	{
		OriginalURL = url,
		Shorten = url
	});

	unitOfWork.Commit();

	return upd;
});

app.MapDefaultEndpoints();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
	var context = serviceScope.ServiceProvider.GetService<DBContext>()!;
	context.Database.Migrate();
}

app.Run();
