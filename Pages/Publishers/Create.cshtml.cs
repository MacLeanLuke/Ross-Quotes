using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RossQuotes.Data;
using RossQuotes.Models;

namespace RossQuotes.Pages.Publishers
{
    public class CreateModel : PageModel
    {
        private readonly RossQuotes.Data.QuoteContext _context;

        public CreateModel(RossQuotes.Data.QuoteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Publisher Publisher { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyPublisher = new Publisher();

            if (await TryUpdateModelAsync<Publisher>(
                emptyPublisher,
                "publisher",   // Prefix for form value.
                s => s.PublisherName))
            {
                _context.Publishers.Add(emptyPublisher);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
