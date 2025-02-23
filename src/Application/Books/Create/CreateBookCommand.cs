using Application.Abstractions.Messaging;

namespace Application.Books.Create;

public sealed class CreateBookCommand : ICommand<Guid>
{
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public string AuthorName { get; set; }
}
