using Application.Books.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Books;

internal sealed class Delete : IEndpoint
{
    public sealed class Request
    {
        public List<Guid> BookIds { get; set; } = [];
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("books/bulk-delete", async ([FromBody] Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new DeleteBookCommand(request.BookIds);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Books)
        .RequireAuthorization();
    }
}
