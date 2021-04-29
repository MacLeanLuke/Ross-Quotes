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

namespace RossQuotes.Pages.Authors
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
    
        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
    
        public PaginatedList<Author> Authors { get; set; }
    
        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            // using System;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;
    
            IQueryable<Author> authorsIQ = from s in _context.Authors
                                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                authorsIQ = authorsIQ.Where(s => s.AuthorLastName.ToUpper().Contains(searchString.ToUpper())
                                       || s.AuthorFirstName.ToUpper().Contains(searchString.ToUpper())
                                       || s.AuthorMiddleName.ToUpper().Contains(searchString.ToUpper()));
            }
    
            switch (sortOrder)
            {
                case "name_desc":
                    authorsIQ = authorsIQ.OrderByDescending(s => s.AuthorLastName);
                    break;
                default:
                    authorsIQ = authorsIQ.OrderBy(s => s.AuthorLastName);
                    break;
            }
    
            var pageSize = Configuration.GetValue("PageSize", 4);
            Authors = await PaginatedList<Author>.CreateAsync(
                authorsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}