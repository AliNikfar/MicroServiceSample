﻿using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=OrderDb;User Id=sa;Password=SwN12345678");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Order> Orders { get; set; }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        {
                            entry.Entity.CreateDate = DateTime.Now;
                            entry.Entity.ModifiedDate = DateTime.Now;
                            entry.Entity.LastModifiedBy = "Ali Nikfar";
                            entry.Entity.CreateBy = "Ali Nikfar";
                            break;
                        }
                    case EntityState.Modified:
                        {
                            entry.Entity.CreateDate = DateTime.Now;
                            entry.Entity.ModifiedDate = DateTime.Now;
                            entry.Entity.LastModifiedBy = "Ali Nikfar";
                            entry.Entity.CreateBy = "Ali Nikfar";
                            break;
                        }
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}