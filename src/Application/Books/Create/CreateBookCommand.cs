using Application.Abstractions.Messaging;

namespace Application.Books.Create;

public sealed class CreateBookCommand : ICommand<List<Guid>>
{
    public List<BookDto> Books { get; set; } = [];
}
