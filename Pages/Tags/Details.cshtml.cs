using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RossQuotes.Data;
using RossQuotes.Models;

namespace RossQuotes.Pages.Tags
{
    public class DetailsModel : PageModel
    {
        private readonly RossQuotes.Data.QuoteContext _context;

        public DetailsModel(RossQuotes.Data.QuoteContext context)
        {
            _context = context;
        }

        public Tag Tag { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tag = await _context.Tags.FirstOrDefaultAsync(m => m.TagID == id);

            if (Tag == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
