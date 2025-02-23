using Application.Abstractions.Messaging;
using Application.Abstractions.Models;

namespace Application.Books.Get;

public sealed record GetBooksQuery(int PageNumber, int PageSize) : IQuery<PaginatedList<BooksResponse>>;
