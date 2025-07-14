namespace AuthorDemo;

public class Mutation
{
    public Author AddAuthor(string id, string name, string country, [Service] AuthorRepository repository)
    {
        var author = new Author(id, name, country);
        return repository.Add(author);
    }
}
