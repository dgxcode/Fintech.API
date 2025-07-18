﻿// <auto-generated />
using System;
using Fintech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fintech.Infrastructure.Migrations
{
    [DbContext(typeof(FintechDbContext))]
    partial class FintechDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Fintech.Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("FromWalletId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FromWalletId1")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ToWalletId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FromWalletId");

                    b.HasIndex("FromWalletId1");

                    b.HasIndex("ToWalletId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Fintech.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Fintech.Domain.Entities.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("Fintech.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Fintech.Domain.Entities.Wallet", null)
                        .WithMany("Transactions")
                        .HasForeignKey("FromWalletId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Fintech.Domain.Entities.Wallet", "FromWallet")
                        .WithMany()
                        .HasForeignKey("FromWalletId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fintech.Domain.Entities.Wallet", "ToWallet")
                        .WithMany()
                        .HasForeignKey("ToWalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromWallet");

                    b.Navigation("ToWallet");
                });

            modelBuilder.Entity("Fintech.Domain.Entities.Wallet", b =>
                {
                    b.HasOne("Fintech.Domain.Entities.User", "User")
                        .WithOne("Wallet")
                        .HasForeignKey("Fintech.Domain.Entities.Wallet", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fintech.Domain.Entities.User", b =>
                {
                    b.Navigation("Wallet")
                        .IsRequired();
                });

            modelBuilder.Entity("Fintech.Domain.Entities.Wallet", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
