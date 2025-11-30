using System;
using Shared.DTOs;
using Shared.Events;

namespace Shared;

public interface IModel
{
    event EventHandler<DataChangedEventArgs>? DataChanged;

    void Insert(BookDto book);
    void Delete(Guid bookId);
}
