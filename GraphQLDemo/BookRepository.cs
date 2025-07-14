using System.Collections.Concurrent;

namespace GraphQLDemo;

public class BookRepository
{
    private readonly ConcurrentDictionary<string, Book> _books = new();

    public IEnumerable<Book> GetAll() => _books.Values;

    public Book? GetById(string id)
    {
        _books.TryGetValue(id, out var book);
        return book;
    }

    public Book Add(Book book)
    {
        _books[book.Id] = book;
        return book;
    }
}
