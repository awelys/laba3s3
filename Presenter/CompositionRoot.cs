using Model;
using Model.Contracts;
using Model.Services;
using Shared;
using View;

namespace Presenter;

public static class CompositionRoot
{
    public static LibraryPresenter BuildLibrary()
    {
        IBookRepository repository = new InMemoryBookRepository();
        IBookValidator validator = new BookValidator();
        IModel model = new LibraryModel(repository, validator);
        IView view = new PassiveView();

        return new LibraryPresenter(model, view);
    }
}
