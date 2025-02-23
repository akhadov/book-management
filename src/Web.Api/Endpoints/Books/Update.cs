
using Application.Books.Create;
using Application.Books.Update;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Books;

internal sealed class Update : IEndpoint
{
    public sealed class Request
    {
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public string AuthorName { get; set; }
    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("books", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdateBookCommand
            {
                Title = request.Title,
                PublicationYear = request.PublicationYear,
                AuthorName = request.AuthorName
            };

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Books)
        .RequireAuthorization();
    }
}
