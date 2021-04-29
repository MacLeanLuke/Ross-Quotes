using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RossQuotes.Data;
using RossQuotes.Models;

namespace RossQuotes.Pages.Authors
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
        public Author Author { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyAuthor = new Author();

            if (await TryUpdateModelAsync<Author>(
                emptyAuthor,
                "author",   // Prefix for form value.
                s => s.AuthorLastName, s => s.AuthorFirstName, s => s.AuthorMiddleName, s => s.AuthorPrefix, s => s.AuthorSuffix))
            {
                _context.Authors.Add(emptyAuthor);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
