using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;
using Core.Entity.Concretes;
using Entity;
using Entity.concretes;
using Entity.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace DataAccess
{
    public class NervioDbContext : DbContext
    {
    /*    public NervioDbContext(DbContextOptions<NervioDbContext>? dbContextOptions): base(dbContextOptions)
        {
            
        }*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=NervioDb;Integrated Security=True");
            //UseSqlServer(ConnectionString.DataBaseConnectionString);
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
    }
}