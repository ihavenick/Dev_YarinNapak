using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Dev_YarinNeYapak.Models;
using Dev_YarinNeYapak.Controllers;

namespace Dev_YarinNeYapak.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Yer> Yerler { get; set; }
        public DbSet<Etkinlik> Etkinlikler { get; set; }
        public DbSet<KiralikEvler> KiralikEvler { get; set; }
        public DbSet<KiralananTarih> KiralananTarih { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }

    public class Yer
    {
        public int YerID { get; set; }
        public string YerAdi { get; set; }
        public List<Etkinlik> YerinEtkinliği { get; set; }
    }

    public class Etkinlik
    {
        public int EtkinlikID { get; set; }
        public string EtkinlikAdi { get; set; }
        public List<ApplicationUser> EtkinliğeKatılanKullanıcılar { get; set; }
        public DateTime NeZaman { get; set; }
    }


}
