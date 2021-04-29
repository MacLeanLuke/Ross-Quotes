using RossQuotes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace RossQuotes.Pages.Quotes
{
    public class EditModel : TitleTagNamePageModel
    {
        private readonly RossQuotes.Data.QuoteContext _context;

        public EditModel(RossQuotes.Data.QuoteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Quote Quote { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Quote = await _context.Quotes
                .Include(c => c.Title)
                .Include(c => c.Tag)
                .FirstOrDefaultAsync(m => m.QuoteID == id);

            if (Quote == null)
            {
                return NotFound();
            }

            // Select current TagID .
            PopulateTagsDropDownList(_context, Quote.TagID);
            // Select current TitleID .
            PopulateTitlesDropDownList(_context, Quote.TitleID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quoteToUpdate = await _context.Quotes.FindAsync(id);

            if (quoteToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Quote>(
                 quoteToUpdate,
                 "quote",   // Prefix for form value.
                   c => c.Quotation, c => c.TitleID, c => c.Page, c => c.TagID))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select TagID if TryUpdateModelAsync fails.
            PopulateTagsDropDownList(_context, quoteToUpdate.TagID);
            PopulateTitlesDropDownList(_context, quoteToUpdate.TitleID);
            return Page();
        }       
    }
}