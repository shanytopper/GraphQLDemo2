# GraphQL Demo

This repository contains two ASP.NET projects exposing GraphQL endpoints:

- **GraphQLDemo** – provides a simple books API
- **AuthorDemo** – provides an authors API with dynamic fields
- **FederationGateway** – ASP.NET gateway that composes both subgraphs using Apollo Federation

## Running the services

```bash
# from repository root

dotnet run --project GraphQLDemo &
dotnet run --project AuthorDemo &

dotnet run --project FederationGateway
```

The gateway will be available at `http://localhost:5017/graphql`.
