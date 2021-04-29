using RossQuotes.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RossQuotes.Pages.Quotes
{
    public class TitleTagNamePageModel : PageModel
    {
        public SelectList TitleNameSL { get; set; }

        public void PopulateTitlesDropDownList(QuoteContext _context,
            object selectedTitle = null)
        {
            var titlesQuery = from d in _context.Titles
                                   orderby d.TitleName // Sort by name.
                                   select d;

            TitleNameSL = new SelectList(titlesQuery.AsNoTracking(),
                        "TitleID", "TitleName", selectedTitle);
        }
        public SelectList TagNameSL { get; set; }

        public void PopulateTagsDropDownList(QuoteContext _context,
            object selectedTag = null)
        {
            var tagsQuery = from d in _context.Tags
                                   orderby d.TagName // Sort by name.
                                   select d;

            TagNameSL = new SelectList(tagsQuery.AsNoTracking(),
                        "TagID", "TagName", selectedTag);
        }
    }
}