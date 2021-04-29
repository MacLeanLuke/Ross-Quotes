using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RossQuotes.Data;
using RossQuotes.Models;

namespace RossQuotes.Pages.Titles
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
        public Title Title { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyTitle = new Title();

            if (await TryUpdateModelAsync<Title>(
                emptyTitle,
                "title",   // Prefix for form value.
                s => s.TitleName, s => s.AuthorID, s => s.PublisherID, s => s.PublishDate))
            {
                _context.Titles.Add(emptyTitle);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}