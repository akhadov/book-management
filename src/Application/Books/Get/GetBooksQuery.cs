using Application.Abstractions.Messaging;

namespace Application.Books.Get;

public sealed record GetBooksQuery : IQuery<List<BooksResponse>>;
