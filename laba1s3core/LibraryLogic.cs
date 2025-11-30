using System;
using System.Collections.Generic;
using System.Linq;

namespace laba1s3core
{
    public interface ILibraryLogic
    {
        void CreateBook(Book book);
        bool DeleteBook(int id);
        Book? ReadBook(int id);
        IEnumerable<Book> ReadAllBooks();
        bool UpdateBook(Book book);
        IEnumerable<Book> FindBooksByAuthor(string authorPart);
        IEnumerable<Book> FindBooksByGenre(string genrePart);
    }

    public class LibraryLogic : ILibraryLogic
    {
        private readonly IRepository<Book> _repository;
        private readonly IBookBusinessLogic _business;

        public LibraryLogic(IRepository<Book> repository, IBookBusinessLogic business)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _business = business ?? throw new ArgumentNullException(nameof(business));
        }

        public void CreateBook(Book book)
        {
            _business.ValidateBook(book);
            _repository.Add(book);
        }

        public bool DeleteBook(int id)
        {
            return _repository.Remove(id);
        }

        public Book? ReadBook(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Book> ReadAllBooks()
        {
            return _repository.GetAll();
        }

        public bool UpdateBook(Book book)
        {
            _business.ValidateBook(book);
            return _repository.Update(book);
        }

        public IEnumerable<Book> FindBooksByAuthor(string authorPart)
        {
            return _business.FindByAuthor(authorPart);
        }

        public IEnumerable<Book> FindBooksByGenre(string genrePart)
        {
            return _business.FindByGenre(genrePart);
        }
    }
}
