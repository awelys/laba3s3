using System;
using System.Collections.Generic;
using System.Linq;
namespace laba1s3core
{
    public class LibraryLogic
    {
        private readonly IRepository<Book> _repo;
        //public LibraryLogic(IRepository<Book> repo) => _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        public LibraryLogic(IRepository<Book> repo)
        {
            _repo = repo;
        }
        public void CreateBook(Book book) => _repo.Add(book);
        public bool DeleteBook(int id) => _repo.Remove(id);
        public Book? ReadBook(int id) => _repo.Get(id);
        public IEnumerable<Book> ReadAllBooks() => _repo.GetAll();
        public bool UpdateBook(Book book) => _repo.Update(book);
        public IEnumerable<Book> FindBooksByAuthor(string authorPart)
        {
            if (string.IsNullOrWhiteSpace(authorPart)) return Enumerable.Empty<Book>();
            var q = authorPart.Trim().ToLowerInvariant();
            return _repo.GetAll().Where(b => b.Author.ToLowerInvariant().Contains(q));
        }
        public IEnumerable<Book> FindBooksByGenre(string genrePart)
        {
            if (string.IsNullOrWhiteSpace(genrePart)) return Enumerable.Empty<Book>();
            var q = genrePart.Trim().ToLowerInvariant();
            return _repo.GetAll().Where(b => b.Genre.ToLowerInvariant().Contains(q));
        }
    }
}
