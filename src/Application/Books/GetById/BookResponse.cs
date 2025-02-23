namespace Application.Books.GetById;

public sealed class BookResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public string AuthorName { get; set; }
    public int ViewsCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
