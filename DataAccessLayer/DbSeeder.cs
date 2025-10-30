using laba1s3core;
using System.Linq;
namespace DataAccessLayer
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Books.Any()) return;
            context.Books.AddRange(
                new Book { Title = "Мастер и Маргарита", Author = "М. Булгаков", Genre = "Роман", Year = 1967 },
                new Book { Title = "Война и мир", Author = "Л. Толстой", Genre = "Роман", Year = 1869 },
                new Book { Title = "Метро 2033", Author = "Д. Глуховский", Genre = "Научная фантастика", Year = 2005 },
                new Book { Title = "Clean Code", Author = "Robert C. Martin", Genre = "Programming", Year = 2008 }
            );
            context.SaveChanges();
        }
    }
}
