using System;
using System.Collections.Generic;
using System.Linq;

namespace laba1s3core
{
    /// <summary>
    /// Реализация бизнес-логики для работы с книгами
    /// </summary>
    public class BookBusinessLogic : IBookBusinessLogic
    {
        private readonly IRepository<Book> _repository;

        public BookBusinessLogic(IRepository<Book> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Поиск книг по автору
        /// </summary>
        public IEnumerable<Book> FindByAuthor(string authorPart)
        {
            if (string.IsNullOrWhiteSpace(authorPart))
                return Enumerable.Empty<Book>();

            var query = authorPart.Trim().ToLowerInvariant();
            var allBooks = _repository.GetAll();
                
            if (allBooks == null)
                return Enumerable.Empty<Book>();

            return allBooks.Where(b => b != null && 
                                       b.Author != null && 
                                       b.Author.ToLowerInvariant().Contains(query))
                           .ToList();
        }

        /// <summary>
        /// Поиск книг по жанру
        /// </summary>
        public IEnumerable<Book> FindByGenre(string genrePart)
        {
            if (string.IsNullOrWhiteSpace(genrePart))
                return Enumerable.Empty<Book>();

            var query = genrePart.Trim().ToLowerInvariant();
            var allBooks = _repository.GetAll();
                
            if (allBooks == null)
                return Enumerable.Empty<Book>();

            return allBooks.Where(b => b != null && 
                                       b.Genre != null && 
                                       b.Genre.ToLowerInvariant().Contains(query))
                           .ToList();
        }

        /// <summary>
        /// Валидация книги перед операциями
        /// </summary>
        public void ValidateBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book), "Книга не может быть null");

            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Название книги не может быть пустым", nameof(book.Title));

            if (string.IsNullOrWhiteSpace(book.Author))
                throw new ArgumentException("Автор книги не может быть пустым", nameof(book.Author));

            if (string.IsNullOrWhiteSpace(book.Genre))
                throw new ArgumentException("Жанр книги не может быть пустым", nameof(book.Genre));

            if (book.Year < 1000 || book.Year > DateTime.Now.Year + 1)
                throw new ArgumentException($"Год издания должен быть между 1000 и {DateTime.Now.Year + 1}", nameof(book.Year));
        }
    }
}
