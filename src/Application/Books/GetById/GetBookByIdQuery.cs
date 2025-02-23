using Application.Abstractions.Messaging;

namespace Application.Books.GetById;

public sealed record GetBookByIdQuery(Guid BookId) : IQuery<BookResponse>;
