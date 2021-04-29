using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RossQuotes.Data;
using RossQuotes.Models;

namespace RossQuotes.Pages.Titles
{
    public class DetailsModel : PageModel
    {
        private readonly RossQuotes.Data.QuoteContext _context;

        public DetailsModel(RossQuotes.Data.QuoteContext context)
        {
            _context = context;
        }

        public Title Title { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Title = await _context.Titles
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.TitleID == id);

            if (Title == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
