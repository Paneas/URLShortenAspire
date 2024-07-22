var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
					 .WithPgAdmin();

var postgresdb = postgres.AddDatabase("urlsDB");

var apiService = builder.AddProject<Projects.URLShortenAspire_ApiService>("apiservice")
						.WithReference(postgresdb);

builder.AddProject<Projects.URLShortenAspire_Web>("webfrontend")
	.WithExternalHttpEndpoints()
	.WithReference(apiService);

builder.Build().Run();
