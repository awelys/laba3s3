using Shared;
using Shared.DTOs;
using Shared.Events;

namespace Presenter;

public sealed class LibraryPresenter
{
    private readonly IModel _model;
    private readonly IView _view;

    public LibraryPresenter(IModel model, IView view)
    {
        _model = model;
        _view = view;

        _view.AddDataEvent += OnAddRequested;
        _view.DeleteDataEvent += OnDeleteRequested;
        _model.DataChanged += OnDataChanged;
    }

    public IView View => _view;

    private void OnAddRequested(object? sender, AddDataEventArgs e)
    {
        _model.Insert(e.Book);
        _view.Insert(e.Book);
    }

    private void OnDeleteRequested(object? sender, DeleteDataEventArgs e)
    {
        _model.Delete(e.BookId);
        _view.Delete(e.BookId);
    }

    private void OnDataChanged(object? sender, DataChangedEventArgs e)
    {
        _view.RedrawForm(e.Books);
    }
}
