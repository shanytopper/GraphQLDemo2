using System.Collections.Concurrent;

namespace AuthorDemo;

public class AuthorRepository
{
    private readonly ConcurrentDictionary<string, Author> _authors = new();

    public IEnumerable<Author> GetAll() => _authors.Values;

    public Author? GetById(string id)
    {
        _authors.TryGetValue(id, out var author);
        return author;
    }

    public Author Add(Author author)
    {
        _authors[author.Id] = author;
        return author;
    }
}
