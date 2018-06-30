﻿// <auto-generated />
using LibraryRestApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LibraryRestApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LibraryRestApi.Models.BookCopy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("BookTitleId");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("BookTitleId");

                    b.ToTable("BookCopys");
                });

            modelBuilder.Entity("LibraryRestApi.Models.BookRental", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("BookCopyId");

                    b.Property<long>("ReaderId");

                    b.Property<DateTime>("RentDate");

                    b.Property<DateTime>("ReturnDate");

                    b.HasKey("Id");

                    b.HasIndex("BookCopyId");

                    b.HasIndex("ReaderId");

                    b.ToTable("BookRentals");
                });

            modelBuilder.Entity("LibraryRestApi.Models.BookTitle", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .IsRequired();

                    b.Property<int>("PublicationYear");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("BookTitles");
                });

            modelBuilder.Entity("LibraryRestApi.Models.Reader", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Account");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Readers");
                });

            modelBuilder.Entity("LibraryRestApi.Models.BookCopy", b =>
                {
                    b.HasOne("LibraryRestApi.Models.BookTitle", "BookTitle")
                        .WithMany("BookCopies")
                        .HasForeignKey("BookTitleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LibraryRestApi.Models.BookRental", b =>
                {
                    b.HasOne("LibraryRestApi.Models.BookCopy", "BookCopy")
                        .WithMany("BookRentals")
                        .HasForeignKey("BookCopyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LibraryRestApi.Models.Reader", "Reader")
                        .WithMany("BookRentals")
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
