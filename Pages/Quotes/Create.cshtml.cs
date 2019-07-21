using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MegaDeskRP.Models;
using Microsoft.EntityFrameworkCore;

namespace MegaDeskRP.Pages.Quotes
{
    public class CreateModel : PageModel
    {
        private readonly MegaDeskRP.Models.MegaDeskRPContext _context;

        public CreateModel(MegaDeskRP.Models.MegaDeskRPContext context)
        {
            _context = context;
        }


        public SelectList ShippingTypeList { get; set; }
        public SelectList MaterialTypeList { get; set; }

        public IActionResult OnGet()
        {
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

            ViewData["DeskId"] = new SelectList(_context.Desk, "DeskId", "SurfaceMaterialId");
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
            
            DeskQuote newQuote = DeskQuote;
          
            //calculate a surfacearea
            decimal surfaceArea = DeskQuote.Desk.Depth * DeskQuote.Desk.Width;

            //declare a variable
            decimal surfaceAreaPrice = 0;

            if (surfaceArea > 1000)
            {
                surfaceAreaPrice = (surfaceArea - 1000) * 1;
            }

            //new var
            var surfaceMaterialPrice = 0;
            surfaceMaterialPrice = _context.SurfaceMaterial
                .Where(r => r.SurfaceMaterialId == DeskQuote.Desk.SurfaceMaterialId)
                .Select(r => r.SurfaceMaterialId)
                .FirstOrDefault();

            //here I wrote a switch statement, because our price is based on materia
            var DrawersPrice = DeskQuote.Desk.NumberOfDrawers * 50;

            decimal shippingPrice = 0;

            if (surfaceArea < 1000)
            { 
                shippingPrice = _context.RushOrder
                .Where(r => r.RushOrderId == DeskQuote.RushOrderId)
                .Select(r => r.PriceLessThan1000)
                .FirstOrDefault();
            } else if (surfaceArea < 2000)
            {
                shippingPrice = _context.RushOrder
                .Where(r => r.RushOrderId == DeskQuote.RushOrderId)
                .Select(r => r.Price1000To2000)
                .FirstOrDefault();
            } else
            {
                shippingPrice = _context.RushOrder
                .Where(r => r.RushOrderId == DeskQuote.RushOrderId)
                .Select(r => r.PriceGreater2000)
                .FirstOrDefault();
            }

            DeskQuote.QuotePrice = 200 + surfaceAreaPrice + DrawersPrice + surfaceMaterialPrice + shippingPrice;


            _context.DeskQuote.Add(DeskQuote);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
            }
        }
    }