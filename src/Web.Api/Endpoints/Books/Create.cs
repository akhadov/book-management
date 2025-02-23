using Application.Books.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Books;

internal sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public List<BookDto> Books { get; set; } = [];
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("books", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateBookCommand
            {
                Books = request.Books,
            };

            Result<List<Guid>> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Books)
        .RequireAuthorization();
    }
}
