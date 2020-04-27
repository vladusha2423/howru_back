using Howru.Data;
using Howru.Data.Dto;
using Howru.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Howru.Core.EF
{
    public class MyContext : IdentityDbContext<User,
        IdentityRole<Guid>, Guid>
    {
        public DbSet<Friend> Friends { get; set; }
        public MyContext(DbContextOptions<MyContext> opt) :
            base(opt)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>[]
                {
                    new IdentityRole<Guid>
                    {
                        Id = Guid.NewGuid(),
                        Name = "admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole<Guid>
                    {
                        Id = Guid.NewGuid(),
                        Name = "user",
                        NormalizedName = "USER"
                    }
                }
            );


            base.OnModelCreating(builder);
        }
    }
}
