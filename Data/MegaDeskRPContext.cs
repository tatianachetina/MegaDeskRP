using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MegaDeskRP.Models;

namespace MegaDeskRP.Models
{
    public class MegaDeskRPContext : DbContext
    {
        public MegaDeskRPContext (DbContextOptions<MegaDeskRPContext> options)
            : base(options)
        {
        }

        public DbSet<MegaDeskRP.Models.Desk> Desk { get; set; }

        public DbSet<MegaDeskRP.Models.DeskQuote> DeskQuote { get; set; }
    }
}
