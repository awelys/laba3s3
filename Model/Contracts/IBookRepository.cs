using Model.Domain;

namespace Model.Contracts;

public interface IBookRepository
{
    IReadOnlyCollection<Book> GetAll();
    void Add(Book book);
    void Remove(Guid id);
}
