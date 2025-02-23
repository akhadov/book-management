using Application.Abstractions.Models;
using Application.Books.Get;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Books;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("books", async (int pageNumber, int pageSize, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new GetBooksQuery(pageNumber, pageSize);

            Result<PaginatedList<BooksResponse>> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Books)
        .RequireAuthorization();
    }
}
