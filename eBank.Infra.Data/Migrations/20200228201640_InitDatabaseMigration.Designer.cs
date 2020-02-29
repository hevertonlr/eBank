﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using eBank.Infra.Data;

namespace eBank.Infra.Data.Migrations
{
    [DbContext(typeof(BankAccountContext))]
    [Migration("20200228201640_InitDatabaseMigration")]
    partial class InitDatabaseMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("eBank.Infra.Data.Entities.BankAccount", b =>
                {
                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Balance");

                    b.HasKey("Number");

                    b.ToTable("BankAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}