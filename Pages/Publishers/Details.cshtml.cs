using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RossQuotes.Data;
using RossQuotes.Models;

namespace RossQuotes.Pages.Publishers
{
    public class DetailsModel : PageModel
    {
        private readonly RossQuotes.Data.QuoteContext _context;

        public DetailsModel(RossQuotes.Data.QuoteContext context)
        {
            _context = context;
        }

        public Publisher Publisher { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Publisher = await _context.Publishers.FirstOrDefaultAsync(m => m.PublisherID == id);

            if (Publisher == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
