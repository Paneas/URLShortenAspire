var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("cache");

var postgres = builder.AddPostgres("postgres")
					 .WithPgAdmin();

var postgresdb = postgres.AddDatabase("urlsDB");

var apiService = builder.AddProject<Projects.URLShortenAspire_ApiService>("apiservice")
						.WithReference(postgresdb)
						.WithReference(redis);

builder.AddProject<Projects.URLShortenAspire_Web>("webfrontend")
	.WithExternalHttpEndpoints()
	.WithReference(apiService);




builder.Build().Run();
