using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw2.Entities
{
    public class CadDbContext :DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Project> Projects { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-O0QCD23\\SQLDEVELOPER;Database=AutoCad;TrustServerCertificate=true;trusted_connection=true");
        }
    }
}
