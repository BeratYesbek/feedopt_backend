using Core.Entity;
using Core.Entity.Concretes;
using Entity;
using Entity.concretes;
using Entity.Concretes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString.DataBaseConnectionString);
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
        public DbSet<UserLocation> UserLocations { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketFile> TicketFiles { get; set; }
    }
}