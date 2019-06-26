using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegaDeskRP.Models;

namespace MegaDeskRP.Pages.Quotes
{
    public class EditModel : PageModel
    {
        private readonly MegaDeskRP.Models.MegaDeskRPContext _context;

        public EditModel(MegaDeskRP.Models.MegaDeskRPContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DeskQuote DeskQuote { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DeskQuote = await _context.DeskQuote
                .Include(d => d.Desk).FirstOrDefaultAsync(m => m.DeskQuoteId == id);

            if (DeskQuote == null)
            {
                return NotFound();
            }
           ViewData["DeskId"] = new SelectList(_context.Desk, "DeskId", "DeskId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DeskQuote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeskQuoteExists(DeskQuote.DeskQuoteId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DeskQuoteExists(int id)
        {
            return _context.DeskQuote.Any(e => e.DeskQuoteId == id);
        }
    }
}
