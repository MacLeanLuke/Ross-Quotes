using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RossQuotes.Data;
using RossQuotes.Models;

namespace RossQuotes.Pages.Authors
{
    public class DetailsModel : PageModel
    {
        private readonly RossQuotes.Data.QuoteContext _context;

        public DetailsModel(RossQuotes.Data.QuoteContext context)
        {
            _context = context;
        }

        public Author Author { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Author = await _context.Authors.FirstOrDefaultAsync(m => m.AuthorID == id);
            Author = await _context.Authors
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.AuthorID == id);

            if (Author == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
