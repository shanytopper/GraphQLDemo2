namespace GraphQLDemo;

public class Query
{
    public IEnumerable<Book> GetBooks([Service] BookRepository repository) => repository.GetAll();

    public Book? GetBook(string id, [Service] BookRepository repository) => repository.GetById(id);
}
