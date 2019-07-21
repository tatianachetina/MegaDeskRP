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
        public Desk Desk { get; set; }
        public SelectList ShippingTypeList { get; set; }
        public SelectList MaterialTypeList { get; set; }

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

            var shippingQuery = from d in _context.RushOrder
                                orderby d.RushOrderId
                                select d;

            ShippingTypeList = new SelectList(shippingQuery.AsNoTracking(),
            "RushOrderId", "ShippingType");

            var materialQuery = from d in _context.SurfaceMaterial
                                orderby d.SurfaceMaterialId
                                select d;

            MaterialTypeList = new SelectList(materialQuery.AsNoTracking(),
            "SurfaceMaterialId", "MaterialType");

            //ViewData["DeskId"] = new SelectList(_context.Desk, "DeskId", "SurfaceMaterialId");
            //return Page();
            ViewData["DeskId"] = new SelectList(_context.Set<Desk>(), "DeskId", "DeskId");
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
