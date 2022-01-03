using Core.Entity;
using Core.Entity.Concretes;
using Entity;
using Entity.concretes;
using Entity.Concretes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class NervioDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString.DataBaseConnectionString);
        }


        public DbSet<AnimalCategory> AnimalCategories { get; set; }
        public DbSet<AnimalSpecies> AnimalSpecies { get; set; }
        public DbSet<MissingDeclaration> MissingDeclarations { get; set; }
        public DbSet<AdoptionNotice> AdoptionNotices { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<AdoptionNoticeImage> AdoptionNoticeImages { get; set; }
        public DbSet<MissingDeclarationImage> MissingDeclarationImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketFile> TicketFiles { get; set; }
    }
}