# Federation Gateway

This Node.js project composes the schemas from the existing `GraphQLDemo` and `AuthorDemo` projects using Apollo Federation. Once all three projects are running you can query books and authors from a single endpoint.

## Running

1. Start the two subgraph servers from the repository root:
   ```
   dotnet run --project GraphQLDemo
   dotnet run --project AuthorDemo
   ```
2. Start the gateway:
   ```
   cd FederationGateway
   npm install
   npm start
   ```

The gateway exposes GraphQL at `http://localhost:4000/`.
