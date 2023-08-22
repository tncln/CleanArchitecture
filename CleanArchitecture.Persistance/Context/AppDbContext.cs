﻿using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistance.Context
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReferance).Assembly);
        //burada ki assembly referansı sayesinde 100 tane de configuration da eklense otomatik olarak DbContext e bağlar.

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<Entity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                    entry.Property(p => p.CreatedDate).CurrentValue = DateTime.Now;
                if (entry.State == EntityState.Modified)
                    entry.Property(p => p.UpdatedDate).CurrentValue = DateTime.Now;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        //Burada ChangeTracker ile Entity içindeki propertyler alınır kaydetme işlemi öncesinde ve createdDate ve UpdatedDate değerleri burada doldurulur. 
    
    }
}
