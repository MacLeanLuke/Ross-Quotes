using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RossQuotes.Data;
using RossQuotes.Models;

namespace RossQuotes.Pages.Tags
{
    public class DeleteModel : PageModel
    {
        private readonly RossQuotes.Data.QuoteContext _context;
        private readonly ILogger<DeleteModel> _logger;


        public DeleteModel(RossQuotes.Data.QuoteContext context, 
                            ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Tag Tag { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tag = await _context.Tags
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.TagID == id);

            if (Tag == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {TagID} failed. Try again", id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            try
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ErrorMessage);

                return RedirectToAction("./Delete",
                                     new { id, saveChangesError = true });
            }
        }
    }
}