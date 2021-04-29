using RossQuotes.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RossQuotes.Pages.Quotes
{
    public class IndexModel : PageModel
    {
        private readonly RossQuotes.Data.QuoteContext _context;

        public IndexModel(RossQuotes.Data.QuoteContext context)
        {
            _context = context;
        }

        public IList<Quote> Quotes { get; set; }

        public async Task OnGetAsync()
        {
            Quotes = await _context.Quotes
                .Include(c => c.Title)
                .Include(c => c.Tag)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}