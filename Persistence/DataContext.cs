﻿using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Value> Values { get; set; }

        protected override void OnModelCreating(ModelBuilder Builder)
        {
            Builder.Entity<Value>()
                .HasData(
                    new Value {Id = 1, Name = "Traveler 101"},
                    new Value {Id = 2, Name = "Traveler 102"},
                    new Value {Id = 3, Name = "Traveler 103"}
                );
        }
    }
}
