﻿using Core.Entity;
using Core.Entity.Concretes;
using Entity;
using Entity.concretes;
using Entity.Concretes;
using Entity.Concretes.Translations;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(false).UseNpgsql(ConnectionString.DataBaseConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<ColorTranslation>().HasOne(c => c.Color).WithMany(t => t.ColorTranslations).HasForeignKey(c => c.ColorId);
            modelBuilder.Entity<Color>().Navigation(t => t.ColorTranslations).HasField("_ColorTranslations").UsePropertyAccessMode(PropertyAccessMode.Property);*/

        }

        public DbSet<AnimalCategory> AnimalCategories { get; set; }
        public DbSet<AnimalSpecies> AnimalSpecies { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<AdvertCategory> AdvertCategories { get; set; }
        public DbSet<AdvertImage> AdvertImages { get; set; }
        public DbSet<FavoriteAdvert> FavoriteAdverts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Age> Ages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Log> logs { get; set; }
        public DbSet<UserLocation> UserLocations { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Translation> Translations { get; set; }    
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketFile> TicketFiles { get; set; }
        public DbSet<Filter> Filters { get; set; }

        public DbSet<ColorTranslation> ColorTranslations { get; set; }



    }
}