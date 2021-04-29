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

namespace RossQuotes.Pages.Tags
{
    public class EditModel : PageModel
    {
        private readonly RossQuotes.Data.QuoteContext _context;

        public EditModel(RossQuotes.Data.QuoteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tag Tag { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tag = await _context.Tags.FindAsync(id);

            if (Tag == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var tagToUpdate = await _context.Tags.FindAsync(id);

            if (tagToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Tag>(
                tagToUpdate,
                "tag",
                s => s.TagName))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.TagID == id);
        }
    }
}
