namespace AuthorDemo;

public class Mutation
{
    public Author AddAuthor(string id, string name, string country, [Service] AuthorRepository repository)
    {
        var author = new Author(id, name, country);
        return repository.Add(author);
    }

    public async Task<bool> AddAuthorField(string name, string type, [Service] JsonTypeModule module)
    {
        await module.AddFieldAsync("AuthorInfo", name, type);
        return true;
    }
}
