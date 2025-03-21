﻿// <auto-generated />
using System;
using ApiBotDiscord.Infraestrutura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiBotDiscord.Migrations
{
    [DbContext(typeof(FranquiaContext))]
    partial class FranquiaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ApiBotDiscord.Domain.Models.Franquia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Attachment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Data_Published")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("FranquiaSet");
                });
#pragma warning restore 612, 618
        }
    }
}
