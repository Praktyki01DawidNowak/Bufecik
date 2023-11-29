using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bufecik.Models;

namespace Bufecik.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Bufecik.Models.Kanapka>? Kanapka { get; set; }
        public DbSet<Bufecik.Models.Kategoria>? Kategoria { get; set; }
        public DbSet<Bufecik.Models.Klient>? Klient { get; set; }
        public DbSet<Bufecik.Models.Status>? Status { get; set; }
        public DbSet<Bufecik.Models.Szczegoly>? Szczegoly { get; set; }
        public DbSet<Bufecik.Models.Zamowienie>? Zamowienie { get; set; }
    }
}