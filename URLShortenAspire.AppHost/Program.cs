var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.URLShortenAspire_ApiService>("apiservice");

builder.AddProject<Projects.URLShortenAspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
