using HotChocolate.Fusion.Execution.Clients;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpClient("books", c => c.BaseAddress = new Uri("http://localhost:5283/graphql"));

builder.Services
    .AddHttpClient("authors", c => c.BaseAddress = new Uri("http://localhost:5284/graphql"));

builder.Services
    .AddGraphQLGatewayServer()
    .AddHttpClientConfiguration(
        "books",
        new Uri("http://localhost:5283/graphql"),
        SupportedOperationType.Query | SupportedOperationType.Mutation)
    .AddHttpClientConfiguration(
        "authors",
        new Uri("http://localhost:5284/graphql"),
        SupportedOperationType.Query | SupportedOperationType.Mutation);

var app = builder.Build();
app.MapGraphQL();
app.Run();
