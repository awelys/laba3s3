using Shared;
using Shared.DTOs;
using Shared.Events;

namespace View;

public sealed class PassiveView : IView
{
    private readonly List<BookDto> _books = new();

    public event EventHandler<AddDataEventArgs>? AddDataEvent;
    public event EventHandler<DeleteDataEventArgs>? DeleteDataEvent;

    public IReadOnlyCollection<BookDto> Snapshot => _books.AsReadOnly();

    public void RedrawForm(IEnumerable<BookDto> books)
    {
        _books.Clear();
        _books.AddRange(books);
    }

    public void Insert(BookDto book)
    {
        _books.Add(book);
    }

    public void Delete(Guid bookId)
    {
        _books.RemoveAll(b => b.Id == bookId);
    }

    public void RequestAdd(BookDto book)
    {
        AddDataEvent?.Invoke(this, new AddDataEventArgs(book));
    }

    public void RequestDelete(Guid bookId)
    {
        DeleteDataEvent?.Invoke(this, new DeleteDataEventArgs(bookId));
    }
}
