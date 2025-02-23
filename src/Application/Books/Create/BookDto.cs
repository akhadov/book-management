using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Create;
public sealed class BookDto
{
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public string AuthorName { get; set; }
}
