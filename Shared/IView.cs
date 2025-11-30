using System;
using System.Collections.Generic;
using Shared.DTOs;
using Shared.Events;

namespace Shared;

public interface IView
{
    event EventHandler<AddDataEventArgs>? AddDataEvent;
    event EventHandler<DeleteDataEventArgs>? DeleteDataEvent;

    void RedrawForm(IEnumerable<BookDto> books);
    void Insert(BookDto book);
    void Delete(Guid bookId);
}
