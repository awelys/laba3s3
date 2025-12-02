using Model.Contracts;
using Shared.DTOs;

namespace Model.Services;

public sealed class BookValidator : IBookValidator
{
    public void Validate(BookDto book)
    {
        if (book is null)
        {
            throw new ArgumentNullException(nameof(book));
        }

        if (string.IsNullOrWhiteSpace(book.Title))
        {
            throw new ArgumentException("Title must be provided", nameof(book));
        }

        if (string.IsNullOrWhiteSpace(book.Author))
        {
            throw new ArgumentException("Author must be provided", nameof(book));
        }
    }
}
