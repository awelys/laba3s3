using System;
using System.Collections.Generic;
using Ninject;
using System.Linq;

namespace laba1s3core
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repo;
        private readonly IBookBusinessLogic _businessLogic;

        public BookService(IRepository<Book> repo, IBookBusinessLogic businessLogic)
        {
            _repo = repo;
            _businessLogic = businessLogic;
        }

        public void Create(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            _businessLogic.ValidateBook(book);
            _repo.Add(book);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID должен быть больше 0", nameof(id));

            return _repo.Remove(id);
        }

        public Book? Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID должен быть больше 0", nameof(id));

            return _repo.Get(id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _repo.GetAll();
        }

        public bool Update(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            _businessLogic.ValidateBook(book);
            return _repo.Update(book);
        }

        public IEnumerable<Book> FindByAuthor(string authorPart)
        {
            return _businessLogic.FindByAuthor(authorPart);
        }

        public IEnumerable<Book> FindByGenre(string genrePart)
        {
            return _businessLogic.FindByGenre(genrePart);
        }
    }
}
