using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Books.Get;

internal sealed class GetBooksQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetBooksQuery, List<BooksResponse>>
{
    public async Task<Result<List<BooksResponse>>> Handle(GetBooksQuery query, CancellationToken cancellationToken)
    {

        List<BooksResponse> books = await context.Books
            .Select(book => new BooksResponse
            {
                Id = book.Id,
                Title = book.Title,
                PublicationYear = book.PublicationYear,
                AuthorName = book.AuthorName,
                ViewsCount = book.ViewsCount,
                CreatedAt = book.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return books;
    }
}
