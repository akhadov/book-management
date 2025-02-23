using Application.Abstractions.Messaging;

namespace Application.Books.Update;
public sealed record UpdateBookCommand : ICommand
{
    public Guid BookId { get; set; }
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public string AuthorName { get; set; }
}
