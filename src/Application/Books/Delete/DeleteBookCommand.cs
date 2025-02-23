using Application.Abstractions.Messaging;

namespace Application.Books.Delete;

public sealed record DeleteBookCommand(Guid BookId) : ICommand;
