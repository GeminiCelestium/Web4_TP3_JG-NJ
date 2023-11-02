﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Web2.API.Data;

#nullable disable

namespace Web2.API.Migrations
{
    [DbContext(typeof(EventPlatformDbContext))]
    [Migration("20230913203818_initialSchemas")]
    partial class initialSchemas
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CategoryEvenement", b =>
                {
                    b.Property<int>("CategoriesID")
                        .HasColumnType("integer");

                    b.Property<int>("EvenementsID")
                        .HasColumnType("integer");

                    b.HasKey("CategoriesID", "EvenementsID");

                    b.HasIndex("EvenementsID");

                    b.ToTable("CategoryEvenement");
                });

            modelBuilder.Entity("Web2.API.Data.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Web2.API.Data.Models.Evenement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateFin")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Organisateur")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VilleID")
                        .HasColumnType("integer");

                    b.Property<double>("prix")
                        .HasColumnType("double precision");

                    b.HasKey("ID");

                    b.HasIndex("VilleID");

                    b.ToTable("Evenements");
                });

            modelBuilder.Entity("Web2.API.Data.Models.Participation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EvenementID")
                        .HasColumnType("integer");

                    b.Property<bool>("IsValid")
                        .HasColumnType("boolean");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NombrePlace")
                        .HasColumnType("integer");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("EvenementID");

                    b.ToTable("Participations");
                });

            modelBuilder.Entity("Web2.API.Data.Models.Ville", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Villes");
                });

            modelBuilder.Entity("CategoryEvenement", b =>
                {
                    b.HasOne("Web2.API.Data.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web2.API.Data.Models.Evenement", null)
                        .WithMany()
                        .HasForeignKey("EvenementsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Web2.API.Data.Models.Evenement", b =>
                {
                    b.HasOne("Web2.API.Data.Models.Ville", "Ville")
                        .WithMany("Evenements")
                        .HasForeignKey("VilleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ville");
                });

            modelBuilder.Entity("Web2.API.Data.Models.Participation", b =>
                {
                    b.HasOne("Web2.API.Data.Models.Evenement", "Evenement")
                        .WithMany("participations")
                        .HasForeignKey("EvenementID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evenement");
                });

            modelBuilder.Entity("Web2.API.Data.Models.Evenement", b =>
                {
                    b.Navigation("participations");
                });

            modelBuilder.Entity("Web2.API.Data.Models.Ville", b =>
                {
                    b.Navigation("Evenements");
                });
#pragma warning restore 612, 618
        }
    }
}