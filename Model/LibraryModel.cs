using System;
using System.Linq;
using Model.Contracts;
using Model.Domain;
using Shared;
using Shared.DTOs;
using Shared.Events;

namespace Model;

public sealed class LibraryModel : IModel
{
    private readonly IBookRepository _repository;
    private readonly IBookValidator _validator;

    public LibraryModel(IBookRepository repository, IBookValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public event EventHandler<DataChangedEventArgs>? DataChanged;

    public void Insert(BookDto book)
    {
        _validator.Validate(book);
        var domain = new Book(book.Id, book.Title, book.Author);
        _repository.Add(domain);
        NotifyDataChanged();
    }

    public void Delete(Guid bookId)
    {
        _repository.Remove(bookId);
        NotifyDataChanged();
    }

    private void NotifyDataChanged()
    {
        var snapshot = _repository.GetAll()
            .Select(b => new BookDto(b.Id, b.Title, b.Author))
            .ToArray();
        DataChanged?.Invoke(this, new DataChangedEventArgs(snapshot));
    }
}
