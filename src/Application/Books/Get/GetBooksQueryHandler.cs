using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Models;
using SharedKernel;

namespace Application.Books.Get;

internal sealed class GetBooksQueryHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
    : IQueryHandler<GetBooksQuery, PaginatedList<BooksResponse>>
{
    public async Task<Result<PaginatedList<BooksResponse>>> Handle(GetBooksQuery query, CancellationToken cancellationToken)
    {
        int currentYear = dateTimeProvider.UtcNow.Year;

        IQueryable<BooksResponse> booksQuery = context.Books
            .Select(book => new
            {
                book.Title,
                PopularityScore = book.ViewsCount * 0.5 + (currentYear - book.PublicationYear) * 2
            })
            .OrderByDescending(book => book.PopularityScore)
            .Select(book => new BooksResponse
            {
                Title = book.Title
            });

        PaginatedList<BooksResponse> paginatedBooks = await PaginatedList<BooksResponse>.CreateAsync(
            booksQuery,
            query.PageNumber,
            query.PageSize
        );

        return Result.Success(paginatedBooks);
    }
}
