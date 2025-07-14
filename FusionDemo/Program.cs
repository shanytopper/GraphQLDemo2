using HotChocolate.Fusion;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFusionGatewayServer("fusion")
    .ConfigureFromFile("fusiongraph.graphql");

var app = builder.Build();

app.MapGraphQL();

app.Run();
