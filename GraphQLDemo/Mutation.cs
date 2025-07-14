namespace GraphQLDemo;

public class Mutation
{
    public Book AddBook(string id, string name, string author, [Service] BookRepository repository)
    {
        var book = new Book(id, name, author);
        return repository.Add(book);
    }
}
