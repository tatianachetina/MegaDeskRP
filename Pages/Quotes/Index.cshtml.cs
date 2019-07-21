using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDeskRP.Models;

namespace MegaDeskRP.Pages.Quotes
{
    public class IndexModel : PageModel
    {
        private readonly MegaDeskRP.Models.MegaDeskRPContext _context;

        public IndexModel(MegaDeskRP.Models.MegaDeskRPContext context)
        {
            _context = context;
        }

        public IList<DeskQuote> DeskQuote { get;set; }
        public string SearchString { get; private set; }

        public async Task OnGetAsync()
        {
            var quotes = from m in _context.DeskQuote
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                quotes = quotes.Where(s => s.CustomerName.Contains(SearchString));
            }

            DeskQuote = await quotes.ToListAsync();
            //DeskQuote = await _context.DeskQuote
            //.Include(d => d.Desk).ToListAsync();
        }
    }
}
