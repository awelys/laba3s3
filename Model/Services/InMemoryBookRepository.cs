using Model.Contracts;
using Model.Domain;

namespace Model.Services;

public sealed class InMemoryBookRepository : IBookRepository
{
    private readonly Dictionary<Guid, Book> _storage = new();

    public IReadOnlyCollection<Book> GetAll() => _storage.Values.ToArray();

    public void Add(Book book)
    {
        _storage[book.Id] = book;
    }

    public void Remove(Guid id)
    {
        _storage.Remove(id);
    }
}
