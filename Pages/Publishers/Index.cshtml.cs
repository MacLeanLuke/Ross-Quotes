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

namespace RossQuotes.Pages.Publishers
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
    
        public PaginatedList<Publisher> Publishers { get; set; }
    
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
    
            IQueryable<Publisher> publishersIQ = from s in _context.Publishers
                                            select s;
    
            if (!String.IsNullOrEmpty(searchString))
            {
                publishersIQ = publishersIQ.Where(s => s.PublisherName.ToUpper().Contains(searchString.ToUpper()));
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    publishersIQ = publishersIQ.OrderByDescending(s => s.PublisherName);
                    break;
                default:
                    publishersIQ = publishersIQ.OrderBy(s => s.PublisherName);
                    break;
            }
    
            var pageSize = Configuration.GetValue("PageSize", 4);
            Publishers = await PaginatedList<Publisher>.CreateAsync(
                publishersIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}