using RossQuotes.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RossQuotes.Pages.Titles
{
    public class AuthorPublisherNamePageModel : PageModel
    {
        public SelectList AuthorNameSL { get; set; }

        public void PopulateAuthorsDropDownList(QuoteContext _context,
            object selectedAuthor = null)
        {
            var authorsQuery = from d in _context.Authors
                                   orderby d.FullName // Sort by name.
                                   select d;

            AuthorNameSL = new SelectList(authorsQuery.AsNoTracking(),
                        "AuthorID", "FullName", selectedAuthor);
        }
        public SelectList PublisherNameSL { get; set; }

        public void PopulatePublishersDropDownList(QuoteContext _context,
            object selectedPublisher = null)
        {
            var publishersQuery = from d in _context.Publishers
                                   orderby d.PublisherName // Sort by name.
                                   select d;

            PublisherNameSL = new SelectList(publishersQuery.AsNoTracking(),
                        "PublisherID", "PublisherName", selectedPublisher);
        }
    }
}