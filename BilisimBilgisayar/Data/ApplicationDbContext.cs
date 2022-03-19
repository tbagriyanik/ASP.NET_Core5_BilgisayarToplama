using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilisimBilgisayar.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BilisimBilgisayar.Models.Kategori> Kategori { get; set; }
        public DbSet<BilisimBilgisayar.Models.Urun> Urun { get; set; }
    }
}
