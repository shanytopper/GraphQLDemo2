# GraphQL Demo

This repository contains two ASP.NET projects exposing GraphQL endpoints:

- **GraphQLDemo** – provides a simple books API
- **AuthorDemo** – provides an authors API with dynamic fields

A new **FederationGateway** project has been added. It uses Apollo Gateway to combine
both subgraphs into a single federated schema.

## Running the services

```bash
# from repository root

dotnet run --project GraphQLDemo &
dotnet run --project AuthorDemo &

# start the gateway
cd FederationGateway
npm install
npm start
```

The gateway will be available at `http://localhost:4000/`.
