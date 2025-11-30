using System.Collections.Generic;

namespace laba1s3core
{
    /// <summary>
    /// Интерфейс для бизнес-логики работы с книгами
    /// </summary>
    public interface IBookBusinessLogic
    {
        IEnumerable<Book> FindByAuthor(string authorPart);
        IEnumerable<Book> FindByGenre(string genrePart);
        void ValidateBook(Book book);
    }
}
