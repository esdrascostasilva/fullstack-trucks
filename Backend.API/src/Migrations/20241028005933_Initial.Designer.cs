﻿// <auto-generated />
using Backend.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.API.Migrations
{
    [DbContext(typeof(CaminhaoDbContext))]
    [Migration("20241028005933_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Backend.API.Models.Caminhao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnoFabricacao")
                        .HasColumnType("int");

                    b.Property<string>("CodigoChassi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModeloId")
                        .HasColumnType("int");

                    b.Property<int>("PlantaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModeloId");

                    b.HasIndex("PlantaId");

                    b.ToTable("Caminhoes");
                });

            modelBuilder.Entity("Backend.API.Models.Modelo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Modelos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "FH"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "FM"
                        },
                        new
                        {
                            Id = 3,
                            Nome = "VM"
                        });
                });

            modelBuilder.Entity("Backend.API.Models.Planta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Plantas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Brasil"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Suécia"
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Estados Unidos"
                        },
                        new
                        {
                            Id = 4,
                            Nome = "França"
                        });
                });

            modelBuilder.Entity("Backend.API.Models.Caminhao", b =>
                {
                    b.HasOne("Backend.API.Models.Modelo", "Modelo")
                        .WithMany("Caminhoes")
                        .HasForeignKey("ModeloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.API.Models.Planta", "Planta")
                        .WithMany("Caminhoes")
                        .HasForeignKey("PlantaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modelo");

                    b.Navigation("Planta");
                });

            modelBuilder.Entity("Backend.API.Models.Modelo", b =>
                {
                    b.Navigation("Caminhoes");
                });

            modelBuilder.Entity("Backend.API.Models.Planta", b =>
                {
                    b.Navigation("Caminhoes");
                });
#pragma warning restore 612, 618
        }
    }
}
