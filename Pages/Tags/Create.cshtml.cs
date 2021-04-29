using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RossQuotes.Data;
using RossQuotes.Models;

namespace RossQuotes.Pages.Tags
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
        public Tag Tag { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyTag = new Tag();

            if (await TryUpdateModelAsync<Tag>(
                emptyTag,
                "tag",   // Prefix for form value.
                s => s.TagName))
            {
                _context.Tags.Add(emptyTag);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}