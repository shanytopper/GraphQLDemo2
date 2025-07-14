namespace AuthorDemo;

public class Query
{
    public IEnumerable<Author> GetAuthors([Service] AuthorRepository repository) => repository.GetAll();

    public Author? GetAuthor(string id, [Service] AuthorRepository repository) => repository.GetById(id);
}
