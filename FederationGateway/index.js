const { ApolloServer } = require('@apollo/server');
const { ApolloGateway } = require('@apollo/gateway');
const fetch = require('cross-fetch');

async function start() {
  const gateway = new ApolloGateway({
    supergraphSdl: null,
    serviceList: [
      { name: 'books', url: 'http://localhost:5283/graphql' },
      { name: 'authors', url: 'http://localhost:5284/graphql' }
    ],
    buildService({ name, url }) {
      const { RemoteGraphQLDataSource } = require('@apollo/gateway');
      return new RemoteGraphQLDataSource({ url, fetch });
    }
  });

  const server = new ApolloServer({
    gateway,
  });

  const { url } = await server.listen({ port: 4000 });
  console.log(`Gateway ready at ${url}`);
}

start().catch(err => {
  console.error(err);
  process.exit(1);
});
