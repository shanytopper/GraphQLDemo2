using HotChocolate.Fusion;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpClient("books", c => c.BaseAddress = new Uri("http://localhost:5283/graphql"));

builder.Services
    .AddHttpClient("authors", c => c.BaseAddress = new Uri("http://localhost:5284/graphql"));

builder.Services
    .AddFusionGatewayServer()
    .ConfigureFromFile("fusion.graphql");

var app = builder.Build();

app.MapGraphQL();

app.Run();
