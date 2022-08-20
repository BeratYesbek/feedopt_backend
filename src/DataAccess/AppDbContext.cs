using System.Configuration;
using Core.Entity.Concretes;
using Core.Utilities.IoC;
using Entity.concretes;
using Entity.Concretes;
using Entity.Concretes.Translations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration["ConnectionStrings:DB_CONNECTION_STRING"]);
            //.UseLazyLoadingProxies(false)
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Advert>().HasOne<AnimalCategory>(a => a.AnimalCategory).WithMany(t => t.Adverts);
            modelBuilder.Entity<Advert>().HasOne<AnimalSpecies>(a => a.AnimalSpecies).WithMany(t => t.Adverts);
            modelBuilder.Entity<Advert>().HasOne<AdvertCategory>(a => a.AdvertCategory).WithMany(t => t.Adverts);
            modelBuilder.Entity<Advert>().HasOne<Age>(a => a.Age).WithMany(t => t.Adverts);
            modelBuilder.Entity<Advert>().HasOne<Color>(a => a.Color).WithMany(t => t.Adverts);
            modelBuilder.Entity<Advert>().HasOne<Location>(t => t.Location).WithMany(t => t.Adverts);
            modelBuilder.Entity<FavoriteAdvert>().HasOne(t => t.Advert).WithMany(t => t.FavoriteAdverts);
            modelBuilder.Entity<AdvertImage>().HasOne(t => t.Advert).WithMany(t => t.AdvertImages);

            /*  modelBuilder.Entity<ColorTranslation>().HasOne(c => c.Color).WithMany(t => t.ColorTranslations).HasForeignKey(c => c.ColorId);
              modelBuilder.Entity<Color>().Navigation(t => t.ColorTranslations).HasField("_colorTranslations").UsePropertyAccessMode(PropertyAccessMode.Property);*/

            /*  modelBuilder.Entity<AnimalCategoryTranslation>().HasOne(c => c.AnimalCategory).WithMany(t => t.AnimalCategoryTranslations).HasForeignKey(t => t.AnimalCategoryId);
              modelBuilder.Entity<AnimalCategory>().Navigation(t => t.AnimalCategoryTranslations).HasField("_AnimalCategoryTranslations").UsePropertyAccessMode(PropertyAccessMode.Property);*/

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
        public DbSet<Token> Tokens { get; set; }
        public DbSet<UserLocation> UserLocations { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<ColorTranslation> ColorTranslations { get; set; }
        public DbSet<AnimalCategoryTranslation> AnimalCategoryTranslations { get; set; }



    }
}