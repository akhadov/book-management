using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Models;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Books.Get;

internal sealed class GetBooksQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetBooksQuery, PaginatedList<BooksResponse>>
{
    public async Task<Result<PaginatedList<BooksResponse>>> Handle(GetBooksQuery query, CancellationToken cancellationToken)
    {
        return Result.Success(await PaginatedList<BooksResponse>.CreateAsync(
            context.Books.Select(book => new BooksResponse
            {
                Title = book.Title
            }),
            query.PageNumber,
            query.PageSize
        ));
    }
}
