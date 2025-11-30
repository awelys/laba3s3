using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using laba1s3core;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс для Dapper репозитория книг
    /// </summary>
    public interface IBookDapperRepository : IRepository<Book>
    {
    }

    public class BookDapperRepository : IBookDapperRepository
    {
        private readonly string _connectionString;

        public BookDapperRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Строка подключения не может быть пустой", nameof(connectionString));

            _connectionString = connectionString;
        }

        private IDbConnection Create()
        {
            return new SqlConnection(_connectionString);
        }

        public void Add(Book entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            ValidateBook(entity);

            using var db = Create();
            const string sql = @"
                    INSERT INTO Books (Title, Author, Genre, Year)
                    VALUES (@Title, @Author, @Genre, @Year);
                    SELECT CAST(SCOPE_IDENTITY() as int);";
                
            var id = db.QuerySingle<int>(sql, entity);
            entity.Id = id;
        }

        public bool Remove(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID должен быть больше 0", nameof(id));

            using var db = Create();
            const string sql = "DELETE FROM Books WHERE Id = @Id";
            return db.Execute(sql, new { Id = id }) > 0;
        }

        public Book? Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID должен быть больше 0", nameof(id));

            using var db = Create();
            const string sql = "SELECT Id, Title, Author, Genre, Year FROM Books WHERE Id = @Id";
            return db.QuerySingleOrDefault<Book>(sql, new { Id = id });
        }

        public IEnumerable<Book> GetAll()
        {
            using var db = Create();
            const string sql = "SELECT Id, Title, Author, Genre, Year FROM Books";
            return db.Query<Book>(sql).ToList();
        }

        public bool Update(Book entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id <= 0)
                throw new ArgumentException("ID должен быть больше 0", nameof(entity.Id));

            ValidateBook(entity);

            using var db = Create();
            const string sql = @"
                    UPDATE Books
                    SET Title=@Title, Author=@Author, Genre=@Genre, Year=@Year
                    WHERE Id=@Id";
                
            return db.Execute(sql, entity) > 0;
        }

        private void ValidateBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Название книги не может быть пустым", nameof(book.Title));

            if (string.IsNullOrWhiteSpace(book.Author))
                throw new ArgumentException("Автор не может быть пустым", nameof(book.Author));

            if (string.IsNullOrWhiteSpace(book.Genre))
                throw new ArgumentException("Жанр не может быть пустым", nameof(book.Genre));
        }
    }
}
