using System;

namespace Shared.Events;

public sealed class DeleteDataEventArgs : EventArgs
{
    public DeleteDataEventArgs(Guid bookId)
    {
        BookId = bookId;
    }

    public Guid BookId { get; }
}
