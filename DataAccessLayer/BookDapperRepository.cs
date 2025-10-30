using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using laba1s3core;

namespace DataAccessLayer
{
    public class BookDapperRepository : IRepository<Book>
    {
        private readonly string _connectionString;
        public BookDapperRepository(string connectionString) => _connectionString = connectionString;
        private IDbConnection Create() => new SqlConnection(_connectionString);

        public void Add(Book entity)
        {
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
            using var db = Create();
            const string sql = "DELETE FROM Books WHERE Id = @Id";
            return db.Execute(sql, new { Id = id }) > 0;
        }

        public Book? Get(int id)
        {
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
            using var db = Create();
            const string sql = @"
            UPDATE Books
            SET Title=@Title, Author=@Author, Genre=@Genre, Year=@Year
            WHERE Id=@Id";
            return db.Execute(sql, entity) > 0;
        }
    }
}
