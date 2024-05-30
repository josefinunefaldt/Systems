using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Adverts.Data;
using Adverts.Data.Entities;

namespace Adverts.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Ads> tbl_ads { get; set; }

        public DbSet<Advertisers> tbl_advertisers { get; set; }
    }
}
