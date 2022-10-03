using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualPinballTableReplacer.Models;

namespace VisualPinballTableReplacer
{
    public class PUPDatabaseContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        
        public PUPDatabaseContext(DbContextOptions<PUPDatabaseContext> options) : base(options)
        {
        }
    }
}
