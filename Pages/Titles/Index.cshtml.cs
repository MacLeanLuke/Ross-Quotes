using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RossQuotes.Data;
using RossQuotes.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace RossQuotes.Pages.Titles
{

    public class IndexModel : PageModel
    {
        private readonly QuoteContext _context;
        private readonly IConfiguration Configuration;
        public IndexModel(QuoteContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
    
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string PublisherSort { get; set; }
        public string PublishDateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
    
        public PaginatedList<Title> Titles { get; set; }
        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            // using System;
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "Author" ? "author_desc" : "Author";
            PublisherSort = sortOrder == "Publisher" ? "publisher_desc" : "Publisher";
            PublishDateSort = sortOrder == "PublishDate" ? "publish_date_desc" : "PublishDate";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;
    
            IQueryable<Title> titlesIQ = from s in _context.Titles
                                            select s;
    
            if (!String.IsNullOrEmpty(searchString))
            {
                titlesIQ = titlesIQ.Where(s => s.TitleName.ToUpper().Contains(searchString.ToUpper()));
            }
            
            switch (sortOrder)
            {
                case "title_desc":
                    titlesIQ = titlesIQ.OrderByDescending(s => s.TitleName);
                    break;
                case "Author":
                titlesIQ = titlesIQ.OrderBy(s => s.AuthorID);
                break;
                case "author_desc":
                titlesIQ = titlesIQ.OrderByDescending(s => s.AuthorID);
                break;
                case "Publisher":
                titlesIQ = titlesIQ.OrderBy(s => s.PublisherID);
                break;
                case "publisher_desc":
                titlesIQ = titlesIQ.OrderByDescending(s => s.PublisherID);
                break;
                case "PublishDate":
                titlesIQ = titlesIQ.OrderBy(s => s.PublishDate);
                break;
                case "publish_date_desc":
                titlesIQ = titlesIQ.OrderByDescending(s => s.PublishDate);
                break;
                default:
                    titlesIQ = titlesIQ.OrderBy(s => s.TitleName);
                    break;
            }
    
            var pageSize = Configuration.GetValue("PageSize", 4);
            Titles = await PaginatedList<Title>.CreateAsync(
                titlesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}