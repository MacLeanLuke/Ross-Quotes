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

namespace RossQuotes.Pages.Publishers
{
    public class EditModel : PageModel
    {
        private readonly RossQuotes.Data.QuoteContext _context;

        public EditModel(RossQuotes.Data.QuoteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Publisher Publisher { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Publisher = await _context.Publishers.FindAsync(id);

            if (Publisher == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var publisherToUpdate = await _context.Publishers.FindAsync(id);

            if (publisherToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Publisher>(
                publisherToUpdate,
                "publisher",
                s => s.PublisherName))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool PublisherExists(int id)
        {
            return _context.Publishers.Any(e => e.PublisherID == id);
        }
    }
}
