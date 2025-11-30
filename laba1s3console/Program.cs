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

        // Создаем Kernel
        bool useDapper = true; // EF - false

        IKernel kernel = useDapper
            ? new StandardKernel(new DapperModule())
            : new StandardKernel(new EfModule());
        var logic = kernel.Get<ILibraryLogic>();

        Console.WriteLine("=== Библиотека (Console) ===");
        
        while (true)
        {
            try
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
                    case "1":
                        foreach (var b in logic.ReadAllBooks())
                            Console.WriteLine(b);
                        break;

                    case "2":
                        try
                        {
                            var newBook = ReadBookFromConsole();
                            logic.CreateBook(newBook);
                            Console.WriteLine("Книга добавлена: " + newBook.Id);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Ошибка валидации: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при добавлении книги: {ex.Message}");
                        }
                        break;

                    case "3":
                        Console.Write("Введите Id книги для удаления: ");
                        if (int.TryParse(Console.ReadLine(), out var delId))
                        {
                            try
                            {
                                var ok = logic.DeleteBook(delId);
                                Console.WriteLine(ok ? "Удалено" : "Книга не найдена");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Ошибка при удалении: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неверный формат Id");
                        }
                        break;

                    case "4":
                        Console.Write("Введите Id книги для изменения: ");
                        if (int.TryParse(Console.ReadLine(), out var upId))
                        {
                            try
                            {
                                var exist = logic.ReadBook(upId);
                                if (exist == null)
                                {
                                    Console.WriteLine("Книга не найдена");
                                    break;
                                }

                                Console.WriteLine("Текущие данные: " + exist);
                                var updated = ReadBookFromConsole();
                                updated.Id = exist.Id;

                                var res = logic.UpdateBook(updated);
                                Console.WriteLine(res ? "Обновлено" : "Ошибка при обновлении");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"Ошибка валидации: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Ошибка при обновлении: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неверный формат Id");
                        }
                        break;

                    case "5":
                        try
                        {
                            Console.Write("Введите часть имени автора: ");
                            var part = Console.ReadLine() ?? string.Empty;

                            var found = logic.FindBooksByAuthor(part).ToList();
                            if (!found.Any())
                                Console.WriteLine("Ничего не найдено");
                            else
                                found.ForEach(b => Console.WriteLine(b));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при поиске: {ex.Message}");
                        }
                        break;

                    case "6":
                        try
                        {
                            Console.Write("Введите часть названия жанра: ");
                            var genre = Console.ReadLine() ?? string.Empty;

                            var byGenre = logic.FindBooksByGenre(genre).ToList();
                            if (!byGenre.Any())
                                Console.WriteLine("Ничего не найдено");
                            else
                                byGenre.ForEach(b => Console.WriteLine(b));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при поиске: {ex.Message}");
                        }
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критическая ошибка: {ex.Message}");
            }
        }
    }

    static Book ReadBookFromConsole()
    {
        Console.Write("Title: "); 
        var title = Console.ReadLine() ?? string.Empty;
        Console.Write("Author: "); 
        var author = Console.ReadLine() ?? string.Empty;
        Console.Write("Genre: "); 
        var genre = Console.ReadLine() ?? string.Empty;
        Console.Write("Year: "); 
        int.TryParse(Console.ReadLine(), out var year);
        
        return new Book { Title = title, Author = author, Genre = genre, Year = year };
    }
}
