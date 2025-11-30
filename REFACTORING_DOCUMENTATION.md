# Документация изменений проекта

## Обзор изменений

Проект был полностью рефакторизирован в соответствии с требованиями:

1. ? Добавлены интерфейсы для каждого класса
2. ? Бизнес-логика вынесена из LibraryLogic
3. ? Создан класс с бизнес-функциями в CompositionRoot
4. ? Добавлена полная обработка ошибок
5. ? Проект успешно собирается

## Новые файлы

### 1. `laba1s3core/IBookBusinessLogic.cs`
Интерфейс для бизнес-логики работы с книгами.
- `FindByAuthor(string authorPart)` - поиск книг по автору
- `FindByGenre(string genrePart)` - поиск книг по жанру
- `ValidateBook(Book book)` - валидация книги

### 2. `CompositionRoot/BookBusinessLogic.cs`
Реализация бизнес-логики для работы с книгами (размещена в CompositionRoot как требовалось).
- Содержит всю бизнес-логику поиска и фильтрации книг
- Включает валидацию данных книги
- Полная обработка ошибок с информативными сообщениями

### 3. `DataAccessLayer/IAppDbContext.cs`
Интерфейс для Entity Framework DbContext.

## Модифицированные файлы

### 1. `DataAccessLayer/AppDbContext.cs`
- Добавлена реализация интерфейса `IAppDbContext`

### 2. `DataAccessLayer/EntityRepository.cs`
- Добавлен интерфейс `IEntityRepository<T>`
- Добавлена полная обработка ошибок во всех методах:
  - Валидация входных параметров
  - Обработка `DbUpdateException`
  - Обработка `DbUpdateConcurrencyException`
  - Информативные сообщения об ошибках

### 3. `DataAccessLayer/BookDapperRepository.cs`
- Добавлен интерфейс `IBookDapperRepository`
- Добавлена валидация строки подключения
- Добавлена полная обработка ошибок:
  - Валидация входных параметров
  - Обработка `SqlException`
  - Валидация данных книги перед операциями

### 4. `laba1s3core/Class2.cs` (BookService)
- Изменена зависимость: теперь использует `IBookBusinessLogic`
- Бизнес-логика делегирована в `IBookBusinessLogic`
- Добавлена полная обработка ошибок
- Валидация параметров

### 5. `laba1s3core/LibraryLogic.cs`
- Теперь это чистый слой делегирования
- Вся бизнес-логика удалена (перенесена в `BookBusinessLogic`)
- Добавлена валидация параметров
- Добавлена обработка ошибок

### 6. `CompositionRoot/SimpleConfigModule.cs`
- Добавлена регистрация `BookBusinessLogic`
- Настроены именованные репозитории ("ef" и "dapper")
- Правильная конфигурация зависимостей через WithConstructorArgument

### 7. `laba1s3console/Program.cs`
- Исправлено использование DI контейнера
- Добавлена полная обработка ошибок в каждой операции:
  - Try-catch блоки для всех операций
  - Разделение обработки `ArgumentException` и других исключений
  - Информативные сообщения для пользователя

### 8. `laba1s3winforms/Form1.cs`
- Добавлена валидация зависимостей в конструкторе
- Добавлена полная обработка ошибок во всех обработчиках событий:
  - Try-catch блоки
  - MessageBox с информативными сообщениями
  - Подтверждение удаления
  - Разные типы иконок для разных типов сообщений

## Архитектура решения

### Слои приложения:

1. **Presentation Layer** (laba1s3console, laba1s3winforms)
   - Пользовательский интерфейс
   - Обработка пользовательского ввода
   - Отображение данных

2. **Business Logic Layer** (laba1s3core)
   - `ILibraryLogic` / `LibraryLogic` - фасад для приложения
   - `IBookService` / `BookService` - сервис для работы с книгами
   - `IBookBusinessLogic` / `BookBusinessLogic` - бизнес-правила

3. **Data Access Layer** (DataAccessLayer)
   - `IRepository<T>` - общий интерфейс репозитория
   - `EntityRepository<T>` - реализация через Entity Framework
   - `BookDapperRepository` - реализация через Dapper
   - `AppDbContext` - контекст базы данных

4. **Composition Root** (CompositionRoot)
   - `SimpleConfigModule` - настройка Dependency Injection
   - `BookBusinessLogic` - реализация бизнес-логики

## Обработка ошибок

Во всех слоях приложения добавлена обработка следующих типов ошибок:

1. **Валидация данных**
   - `ArgumentNullException` - для null параметров
   - `ArgumentException` - для некорректных значений

2. **Ошибки базы данных**
   - `DbUpdateException` - ошибки обновления в EF
   - `DbUpdateConcurrencyException` - конфликты конкурентного доступа
   - `SqlException` - ошибки SQL сервера

3. **Общие ошибки**
   - `InvalidOperationException` - для операционных ошибок
   - Обработка всех неожиданных исключений

## Использование

### Консольное приложение:
```csharp
var kernel = new StandardKernel(new SimpleConfigModule());
var logic = kernel.Get<ILibraryLogic>();
```

### WinForms приложение:
```csharp
IKernel kernel = new StandardKernel(new SimpleConfigModule());
var logic = kernel.Get<ILibraryLogic>();
Application.Run(new Form1(logic));
```

## Конфигурация репозиториев

По умолчанию используется Entity Framework репозиторий ("ef").
Для использования Dapper нужно изменить конфигурацию в `SimpleConfigModule.cs`.

## Проверка сборки

? Проект успешно собирается без ошибок и предупреждений.
