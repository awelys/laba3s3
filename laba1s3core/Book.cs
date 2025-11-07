namespace laba1s3core
{
    public interface IDomainObject { int Id { get; set; } }

    public interface IBook
    {
        int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }

    }

    public class Book : IDomainObject, IBook
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Year { get; set; }
        public override string ToString() => $"{Id}: {Title} ({Author}, {Year}) - {Genre}";
    }
}
