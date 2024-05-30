using Microsoft.EntityFrameworkCore;
using Subscribers.Data.Entities;
using System.Collections.Generic;
using Subscribers.Data;
using System.ComponentModel.DataAnnotations;

namespace Subscribers.Data
{
    public class SubDbContext : DbContext
    {
        public SubDbContext(DbContextOptions<SubDbContext> options) : base(options)
        {

        }
        public DbSet<Sub> tbl_subscribers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sub>().HasData(
               new Sub
               {
                   sub_id = 1,
                   sub_FirstName = "Maja",
                   sub_LastName = "Svensson",
                   sub_deliveryAddress = "fallrundan 11",
                   sub_PhoneNumber = "0768234535",
                   sub_postalCode = "82830",
                   sub_socialSecurityNumber = "930412",
                   sub_subscriptionNumber = 100
               },
                new Sub
                {
                    sub_id = 2,
                    sub_FirstName = "Kalle",
                    sub_LastName = "Andersson",
                    sub_deliveryAddress = "dalrundan 27",
                    sub_PhoneNumber = "0738234536",
                    sub_postalCode = "82832",
                    sub_socialSecurityNumber = "930418",
                    sub_subscriptionNumber = 337

    }
            );
        }
    }
    }
