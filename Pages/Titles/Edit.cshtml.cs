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

namespace RossQuotes.Pages.Titles
{
    public class EditModel : PageModel
    {
        private readonly RossQuotes.Data.QuoteContext _context;

        public EditModel(RossQuotes.Data.QuoteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Title Title { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Title = await _context.Titles.FindAsync(id);

            if (Title == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var titleToUpdate = await _context.Titles.FindAsync(id);

            if (titleToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Title>(
                titleToUpdate,
                "title",
                s => s.TitleName, s => s.AuthorID, s => s.PublisherID, s => s.PublishDate))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool TitleExists(int id)
        {
            return _context.Titles.Any(e => e.TitleID == id);
        }
    }
}
