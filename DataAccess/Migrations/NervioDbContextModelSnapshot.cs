﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(NervioDbContext))]
    partial class NervioDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Entity.Concretes.OperationClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OperationClaims");
                });

            modelBuilder.Entity("Core.Entity.Concretes.UserOperationClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OperationClaimId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserOperationClaims1");
                });

            modelBuilder.Entity("Core.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Entity.Concretes.AdoptionNoticeImage", b =>
                {
                    b.Property<int>("AdoptionNoticeImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdoptionNoticeId")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdoptionNoticeImageId");

                    b.HasIndex("AdoptionNoticeId");

                    b.ToTable("AdoptionNoticeImages");
                });

            modelBuilder.Entity("Entity.Concretes.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("City")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Country")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceId")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Entity.Concretes.MissingDeclarationImage", b =>
                {
                    b.Property<int>("MissingDeclarationImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MissingDeclarationId")
                        .HasColumnType("int");

                    b.HasKey("MissingDeclarationImageId");

                    b.HasIndex("MissingDeclarationId");

                    b.ToTable("MissingDeclarationImages");
                });

            modelBuilder.Entity("Entity.MissingDeclaration", b =>
                {
                    b.Property<int>("MissingDeclarationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnimalSpeciesId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MissingDeclarationId");

                    b.HasIndex("AnimalSpeciesId");

                    b.HasIndex("LocationId");

                    b.HasIndex("UserId");

                    b.ToTable("MissingDeclarations");
                });

            modelBuilder.Entity("Entity.concretes.AdoptionNotice", b =>
                {
                    b.Property<int>("AdoptionNoticeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnimalSpeciesId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AdoptionNoticeId");

                    b.HasIndex("AnimalSpeciesId");

                    b.HasIndex("LocationId");

                    b.HasIndex("UserId");

                    b.ToTable("AdoptionNotices");
                });

            modelBuilder.Entity("Entity.concretes.AnimalCategory", b =>
                {
                    b.Property<int>("AnimalCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnimalCategoryName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AnimalCategoryId");

                    b.ToTable("AnimalCategories");
                });

            modelBuilder.Entity("Entity.concretes.AnimalSpecies", b =>
                {
                    b.Property<int>("AnimalSpeciesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnimalCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Kind")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AnimalSpeciesId");

                    b.HasIndex("AnimalCategoryId");

                    b.ToTable("AnimalSpecies");
                });

            modelBuilder.Entity("Entity.Concretes.AdoptionNoticeImage", b =>
                {
                    b.HasOne("Entity.concretes.AdoptionNotice", "AdoptionNotice")
                        .WithMany("AdoptionNoticeImage")
                        .HasForeignKey("AdoptionNoticeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdoptionNotice");
                });

            modelBuilder.Entity("Entity.Concretes.MissingDeclarationImage", b =>
                {
                    b.HasOne("Entity.MissingDeclaration", "MissingDeclaration")
                        .WithMany("MissingDeclarationImages")
                        .HasForeignKey("MissingDeclarationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MissingDeclaration");
                });

            modelBuilder.Entity("Entity.MissingDeclaration", b =>
                {
                    b.HasOne("Entity.concretes.AnimalSpecies", "AnimalSpecies")
                        .WithMany("MissingDeclarations")
                        .HasForeignKey("AnimalSpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity.Concretes.Location", "Location")
                        .WithMany("MissingDeclarations")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnimalSpecies");

                    b.Navigation("Location");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entity.concretes.AdoptionNotice", b =>
                {
                    b.HasOne("Entity.concretes.AnimalSpecies", "AnimalSpecies")
                        .WithMany()
                        .HasForeignKey("AnimalSpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity.Concretes.Location", "Location")
                        .WithMany("AdoptionNotices")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnimalSpecies");

                    b.Navigation("Location");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entity.concretes.AnimalSpecies", b =>
                {
                    b.HasOne("Entity.concretes.AnimalCategory", "AnimalCategory")
                        .WithMany("AnimalSpecies")
                        .HasForeignKey("AnimalCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnimalCategory");
                });

            modelBuilder.Entity("Entity.Concretes.Location", b =>
                {
                    b.Navigation("AdoptionNotices");

                    b.Navigation("MissingDeclarations");
                });

            modelBuilder.Entity("Entity.MissingDeclaration", b =>
                {
                    b.Navigation("MissingDeclarationImages");
                });

            modelBuilder.Entity("Entity.concretes.AdoptionNotice", b =>
                {
                    b.Navigation("AdoptionNoticeImage");
                });

            modelBuilder.Entity("Entity.concretes.AnimalCategory", b =>
                {
                    b.Navigation("AnimalSpecies");
                });

            modelBuilder.Entity("Entity.concretes.AnimalSpecies", b =>
                {
                    b.Navigation("MissingDeclarations");
                });
#pragma warning restore 612, 618
        }
    }
}
