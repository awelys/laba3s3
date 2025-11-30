using System;
using Shared.DTOs;

namespace Shared.Events;

public sealed class AddDataEventArgs : EventArgs
{
    public AddDataEventArgs(BookDto book)
    {
        Book = book;
    }

    public BookDto Book { get; }
}
