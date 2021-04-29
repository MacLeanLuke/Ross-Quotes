using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RossQuotes.Data;
using RossQuotes.Models;

namespace RossQuotes.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly RossQuotes.Data.QuoteContext _context;

        public EditModel(RossQuotes.Data.QuoteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Author Author { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Author = await _context.Authors.FindAsync(id);

            if (Author == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var authorToUpdate = await _context.Authors.FindAsync(id);

            if (authorToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Author>(
                authorToUpdate,
                "author",
                s => s.AuthorLastName, s => s.AuthorFirstName, s => s.AuthorMiddleName, s => s.AuthorPrefix, s => s.AuthorSuffix))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.AuthorID == id);
        }
    }
}
