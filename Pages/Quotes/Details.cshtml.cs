using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RossQuotes.Data;
using RossQuotes.Models;

namespace RossQuotes.Pages.Quotes
{
    public class DetailsModel : PageModel
    {
        private readonly RossQuotes.Data.QuoteContext _context;

        public DetailsModel(RossQuotes.Data.QuoteContext context)
        {
            _context = context;
        }

        public Quote Quote { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Quote = await _context.Quotes
                .Include(s => s.TagID)
                .AsNoTracking()
                .Include(c => c.Title)
                .Include(c => c.Tag)
                .FirstOrDefaultAsync(m => m.QuoteID == id);

            if (Quote == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
