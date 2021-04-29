using RossQuotes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RossQuotes.Pages.Quotes
{
    public class CreateModel : TitleTagNamePageModel

    {
        private readonly RossQuotes.Data.QuoteContext _context;

        public CreateModel(RossQuotes.Data.QuoteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateTitlesDropDownList(_context);
            PopulateTagsDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Quote Quote { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyQuote = new Quote();

            if (await TryUpdateModelAsync<Quote>(
                 emptyQuote,
                 "quote",   // Prefix for form value.
                 s => s.QuoteID, s => s.Quotation, s => s.TitleID, s => s.Page, s => s.TagID))
            {
                _context.Quotes.Add(emptyQuote);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateTitlesDropDownList(_context, emptyQuote.TitleID);
            PopulateTagsDropDownList(_context, emptyQuote.TagID);
            return Page();
        }
      }
}