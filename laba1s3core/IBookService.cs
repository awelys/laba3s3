using System.Collections.Generic;

namespace laba1s3core
{
    public interface IBookService
    {
        void Create(Book book);
        bool Delete(int id);
        Book? Get(int id);
        IEnumerable<Book> GetAll();
        bool Update(Book book);
        IEnumerable<Book> FindByAuthor(string authorPart);
        IEnumerable<Book> FindByGenre(string genrePart);
    }
}
