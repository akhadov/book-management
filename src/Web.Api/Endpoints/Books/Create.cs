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
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public string AuthorName { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("books", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateBookCommand
            {
                Title = request.Title,
                PublicationYear = request.PublicationYear,
                AuthorName = request.AuthorName
            };

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Books)
        .RequireAuthorization();
    }
}
