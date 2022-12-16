﻿// <auto-generated />
using CurrencyProfiler.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CurrencyProfiler.WebApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CurrencyProfiler.Domain.CurrencyList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Eur")
                        .HasColumnType("float");

                    b.Property<double>("Rub")
                        .HasColumnType("float");

                    b.Property<string>("Timestamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Usd")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });
#pragma warning restore 612, 618
        }
    }
}
