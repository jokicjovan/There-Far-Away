﻿using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models;

namespace Three_Far_Away.DbContexts
{
    public class ThereFarAwayDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Attraction> Attractions { get; set; }
        public DbSet<Journey> Journeys { get; set; }
        public DbSet<Arrangement> Arrangements { get; set; }

        public ThereFarAwayDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Credential>()
                .HasIndex(c => new { c.Username })
                .IsUnique(true);
        }
    }
}
