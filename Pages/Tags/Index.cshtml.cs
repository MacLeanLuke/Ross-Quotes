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

namespace RossQuotes.Pages.Tags
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
    
        public string TagSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
    
        public PaginatedList<Tag> Tags { get; set; }
        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            // using System;
            TagSort = String.IsNullOrEmpty(sortOrder) ? "tag_desc" : "";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;
    
            IQueryable<Tag> tagsIQ = from s in _context.Tags
                                            select s;
    
            if (!String.IsNullOrEmpty(searchString))
            {
                tagsIQ = tagsIQ.Where(s => s.TagName.ToUpper().Contains(searchString.ToUpper()));
            }
            
            switch (sortOrder)
            {
                case "tag_desc":
                    tagsIQ = tagsIQ.OrderByDescending(s => s.TagName);
                    break;
                default:
                    tagsIQ = tagsIQ.OrderBy(s => s.TagName);
                    break;
            }
    
            var pageSize = Configuration.GetValue("PageSize", 4);
            Tags = await PaginatedList<Tag>.CreateAsync(
                tagsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}