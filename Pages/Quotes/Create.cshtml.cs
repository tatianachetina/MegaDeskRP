using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MegaDeskRP.Models;

namespace MegaDeskRP.Pages.Quotes
{
    public class CreateModel : PageModel
    {
        private readonly MegaDeskRP.Models.MegaDeskRPContext _context;

        public CreateModel(MegaDeskRP.Models.MegaDeskRPContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["DeskId"] = new SelectList(_context.Desk, "DeskId", "DeskId");
            return Page();
        }

        [BindProperty]
        public DeskQuote DeskQuote { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DeskQuote.Add(DeskQuote);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}