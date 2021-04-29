using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RossQuotes.Data;
using RossQuotes.Models;

namespace RossQuotes.Pages.Quotes
{
    public class DeleteModel : PageModel
    {
        private readonly RossQuotes.Data.QuoteContext _context;
        private readonly ILogger<DeleteModel> _logger;


        public DeleteModel(RossQuotes.Data.QuoteContext context, 
                            ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Quote Quote { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Quote = await _context.Quotes
                .AsNoTracking()
                .Include(c => c.Title)
                .Include(c => c.Tag)
                .FirstOrDefaultAsync(m => m.QuoteID == id);

            if (Quote == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {QuoteID} failed. Try again", id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quote = await _context.Quotes.FindAsync(id);

            if (quote == null)
            {
                return NotFound();
            }

            try
            {
                _context.Quotes.Remove(quote);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ErrorMessage);

                return RedirectToAction("./Delete",
                                     new { id, saveChangesError = true });
            }
        }
    }
}