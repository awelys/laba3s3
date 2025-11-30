namespace Model.Domain;

public sealed class Book
{
    public Book(Guid id, string title, string author)
    {
        Id = id;
        Title = title;
        Author = author;
    }

    public Guid Id { get; }
    public string Title { get; }
    public string Author { get; }
}
