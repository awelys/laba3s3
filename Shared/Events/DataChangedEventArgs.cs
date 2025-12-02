using System;
using System.Collections.Generic;
using Shared.DTOs;

namespace Shared.Events;

public sealed class DataChangedEventArgs : EventArgs
{
    public DataChangedEventArgs(IEnumerable<BookDto> books)
    {
        Books = books;
    }

    public IEnumerable<BookDto> Books { get; }
}
