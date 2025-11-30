# MVP reference implementation

## Проектная структура
- **Shared** — общие контракты `IModel`, `IView`, DTO и EventArgs для событий (Add, Delete, DataChanged).
- **Model** — доменные сущности (`Book`), бизнес-правила (`BookValidator`), абстракции для инверсии зависимостей (`IBookRepository`, `IBookValidator`) и реализация модели (`LibraryModel`).
- **View** — пассивная реализация представления (`PassiveView`), генерирующая события и перерисовывающаяся по командам Presenter.
- **Presenter** — `LibraryPresenter` связывает View и Model, а `CompositionRoot` собирает зависимости.

## Точка сборки
```csharp
using Presenter;

var presenter = CompositionRoot.BuildLibrary();
var view = presenter.View;

view.RequestAdd(new BookDto(Guid.NewGuid(), "Title", "Author"));
view.RequestDelete(view.Snapshot.First().Id);
```
`CompositionRoot.BuildLibrary()` создаёт репозиторий, валидатор, модель, пассивный View и связывает их в Presenter.

## SOLID
- **Single Responsibility Principle:** каждая часть отвечает за одну зону — валидация (`BookValidator`), хранение (`InMemoryBookRepository`), бизнес-операции и уведомления (`LibraryModel`), отображение (`PassiveView`), координация (`LibraryPresenter`).
- **Open/Closed Principle:** `IBookRepository` и `IBookValidator` позволяют добавлять новые реализации без изменения остального кода.
- **Liskov Substitution Principle:** зависимости принимаются через интерфейсы (`IModel`, `IView`, `IBookRepository`, `IBookValidator`), поэтому любая совместимая реализация подставляется без нарушения работы.
- **Interface Segregation Principle:** View и Model обмениваются минимальными контрактами с событиями и методами, не требуя лишних зависимостей.
- **Dependency Inversion Principle:** высокоуровневые компоненты (`LibraryPresenter`, `LibraryModel`) зависят от абстракций из `Shared` и `Model.Contracts`, а конкретные реализации передаются через композиционный корень.
