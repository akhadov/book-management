using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Books;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Books.GetById;

internal sealed class GetBookByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetBookByIdQuery, BookResponse>
{
    public async Task<Result<BookResponse>> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
    {
        BookResponse? book = await context.Books
            .Where(book => book.Id == query.BookId)
            .Select(book => new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                PublicationYear = book.PublicationYear,
                AuthorName = book.AuthorName,
                ViewsCount = book.ViewsCount,
                CreatedAt = book.CreatedAt
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (book is null)
        {
            return Result.Failure<BookResponse>(BookErrors.NotFound(query.BookId));
        }

        return book;
    }
}
