using System;
using System.Linq;
using laba1s3core;
using CompositionRoot;
using Ninject;
class Program
{
    static void Main()
    {
        AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
        //var logic = RepositoryFactory.CreateLibraryLogic(useEf: true);

        /// <summary>
        /// EF
        /// </summary>
        //var kernel = new StandardKernel(new SimpleConfigModule());
        //var logic = kernel.Get<LibraryLogic>();

        /// <summary>
        /// Dapper
        /// </summary>
        var kernel = new StandardKernel(new SimpleConfigModule()).Get<IRepository<Book>>("dapper");
        var logic = new LibraryLogic(kernel);
        Console.WriteLine("=== Библиотека (Console) ===");
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1: Показать все книги");
            Console.WriteLine("2: Добавить книгу");
            Console.WriteLine("3: Удалить книгу по Id");
            Console.WriteLine("4: Изменить книгу по Id");
            Console.WriteLine("5: Поиск по автору");
            Console.WriteLine("6: Поиск по жанру");
            Console.WriteLine("0: Выход");
            Console.Write("Ввод: ");
            var k = Console.ReadLine();
            Console.WriteLine();
            switch (k)
            {
                case "1": foreach (var b in logic.ReadAllBooks()) Console.WriteLine(b); break;
                case "2": var newBook = ReadBookFromConsole(); logic.CreateBook(newBook); Console.WriteLine("Книга добавлена: " + newBook.Id); break;
                case "3": Console.Write("Введите Id книги для удаления: "); if (int.TryParse(Console.ReadLine(), out var delId)) { var ok = logic.DeleteBook(delId); Console.WriteLine(ok ? "Удалено" : "Книга не найдена"); } else Console.WriteLine("Неверный формат Id"); break;
                case "4": Console.Write("Введите Id книги для изменения: "); if (int.TryParse(Console.ReadLine(), out var upId)) { var exist = logic.ReadBook(upId); if (exist == null) { Console.WriteLine("Книга не найдена"); break; } Console.WriteLine("Текущие данные: " + exist); var updated = ReadBookFromConsole(); updated.Id = exist.Id; var res = logic.UpdateBook(updated); Console.WriteLine(res ? "Обновлено" : "Ошибка при обновлении"); } else Console.WriteLine("Неверный формат Id"); break;
                case "5": Console.Write("Введите часть имени автора: "); var part = Console.ReadLine() ?? string.Empty; var found = logic.FindBooksByAuthor(part).ToList(); if (!found.Any()) Console.WriteLine("Ничего не найдено"); else found.ForEach(b => Console.WriteLine(b)); break;
                case "6": Console.Write("Введите часть названия жанра: "); var genre = Console.ReadLine() ?? string.Empty; var byGenre = logic.FindBooksByGenre(genre).ToList(); if (!byGenre.Any()) Console.WriteLine("Ничего не найдено"); else byGenre.ForEach(b => Console.WriteLine(b)); break;
                case "0": return;
                default: Console.WriteLine("Неизвестная команда"); break;
            }
        }
    }
    static Book ReadBookFromConsole()
    {
        Console.Write("Title: "); var title = Console.ReadLine() ?? string.Empty;
        Console.Write("Author: "); var author = Console.ReadLine() ?? string.Empty;
        Console.Write("Genre: "); var genre = Console.ReadLine() ?? string.Empty;
        Console.Write("Year: "); int.TryParse(Console.ReadLine(), out var year);
        return new Book { Title = title, Author = author, Genre = genre, Year = year };
    }
}
