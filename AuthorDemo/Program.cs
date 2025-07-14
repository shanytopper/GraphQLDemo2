using AuthorDemo;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// GraphQL services
builder.Services.AddSingleton<AuthorRepository>();
var module = new JsonTypeModule(Path.Combine(builder.Environment.ContentRootPath, "author-types.json"));
builder.Services.AddSingleton(module);
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddTypeModule<JsonTypeModule>(_ => module);

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();

app.Run();
